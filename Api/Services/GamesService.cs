using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Dtos.Games;
using PunsApi.Helpers;
using PunsApi.Hubs;
using PunsApi.Hubs.Interfaces;
using PunsApi.Models;
using PunsApi.Requests.Games;
using PunsApi.Requests.Rooms;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Games;
using PunsApi.ViewModels.Rooms;

namespace PunsApi.Services
{
    public class GamesService : BaseService, IGamesService
    {
        private readonly IHubContext<GameHub, IGameHub> _gameHubContext;

        public GamesService(AppDbContext context, IHttpContextAccessor httpContextAccessor, IHubContext<GameHub, IGameHub> gameHubContext) : base(context, httpContextAccessor)
        {
            _gameHubContext = gameHubContext;
        }

        public async Task<ServiceResponse<bool>> CreateGame(CreateGameRequest request)
        {
            var player = await GetPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No player found");

            var room = _context.Rooms.FirstOrDefault(x => x.Id == player.RoomId);

            if (room == null)
                return ServiceResponse<bool>.Error("No room found");

            var isRoomNameValid = NamesValidator.IsNameValid(request.GameName);

            if (!isRoomNameValid)
                return ServiceResponse<bool>.Error("Game name is too short");

            var newGame = new Game
            {
                Name = request.GameName,
                RoomId = room.Id,
                GameMasterId = player.Id
            };

            await _context.Games.AddAsync(newGame);
            await _context.SaveChangesAsync();
            player.GameId = newGame.Id;
            player.IsGameMaster = true;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Game created");
        }

        public async Task<ServiceResponse<FetchGameViewModel>> FetchGame()
        {
            var player = await GetPlayer();

            var game = await _context.Games.Include(x => x.Players).FirstOrDefaultAsync(
                x => x.Id == player.GameId);

            if (game == null)
                return ServiceResponse<FetchGameViewModel>.Error("No game found");

            return ServiceResponse<FetchGameViewModel>.Ok(new FetchGameViewModel(game));
        }


        public async Task<ServiceResponse<bool>> JoinGame(string gameId, string connectionId)
        {
            var (response, player) = await ValidateRequest<bool>(
                gameId, true);

            if (!response.Success)
                return response;

            var game = await _context.Games.FirstOrDefaultAsync(i => i.Id.ToString() == gameId);
            player.IsGameMaster = game.GameMasterId == player.Id;

            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            await _gameHubContext.Groups.AddToGroupAsync(connectionId, gameId);
            await _gameHubContext.Clients.Group(gameId).PlayerJoined(player.Id.ToString());

            return ServiceResponse<bool>.Ok(true, "Player joined to game");
        }

        public async Task<ServiceResponse<bool>> QuitGame(string gameId, string connectionId)
        {
            var (response, player) = await ValidateRequest(gameId, true);

            if (!response.Success)
                return response;

            if (player.Room.Id != player.Game.RoomId)
                return ServiceResponse<bool>.Error("User isn't in this room");
            player.Score = 0;

            player.GameId = null;
            _context.Update(player);
            await _context.SaveChangesAsync();

            await _gameHubContext.Groups.RemoveFromGroupAsync(connectionId, gameId);
            await _gameHubContext.Clients.Group(gameId).PlayerQuit(player.Id.ToString());

            return ServiceResponse<bool>.Ok(true, "Player quit game");
        }


        public async Task<ServiceResponse<bool>> GameStart(string gameId)
        {
            var (response, player) = await ValidateRequest(gameId, true);

            if (!response.Success)
                return response;

            if (!player.IsGameMaster)
                return ServiceResponse<bool>.Error("Player isn't game master");

            var game = _context.Games.FirstOrDefault(x => x.Id == player.GameId);

            if (game == null)
                return ServiceResponse<bool>.Error("No game found");

            game.GameStarted = true;
            game.GameEnded = false;
            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            await _gameHubContext.Clients.Group(gameId).GameStarted();

            return ServiceResponse<bool>.Ok(true, "Game started");
        }

        public async Task<ServiceResponse<bool>> GameEnd(string gameId)
        {
            var (response, player) = await ValidateRequest(gameId, true);

            if (!response.Success)
                return response;

            if (!player.IsGameMaster)
                return ServiceResponse<bool>.Error("Player isn't game master");

            var game = _context.Games.FirstOrDefault(x => x.Id == player.GameId);

            if (game == null)
                return ServiceResponse<bool>.Error("No game found");

            game.GameStarted = false;
            game.GameEnded = true;
            _context.Games.Update(game);
            player.Score = 0;
            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            await _gameHubContext.Clients.Group(gameId).GameEnded(player.Nick);

            return ServiceResponse<bool>.Ok(true, "Game ended");
        }

        public async Task<ServiceResponse<FetchPasswordsViewModel>> FetchPasswords()
        {
            var passwordCategories = await _context.PasswordCategories.Include(
                x => x.Passwords).ToListAsync();

            return ServiceResponse<FetchPasswordsViewModel>.Ok(new FetchPasswordsViewModel(passwordCategories));

        }

        public async Task<ServiceResponse<bool>> PlayerScored(string nextPlayerId)
        {
            var player = _context.Players.FirstOrDefault(x => x.Id.ToString() == nextPlayerId);

            if (player == null)
                return ServiceResponse<bool>.Error("No player found");

            var game = await _context.Games.FirstOrDefaultAsync(
                x => x.Id == player.GameId);
            if (game.ShowingPlayerId != null)
                player.Score++;
            if (player.Score >= 10)
                return await GameEnd(game.Id.ToString());

            _context.Players.Update(player);
            game.ShowingPlayerId = player.Id;
            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player scored and switched");
        }

        public async Task<FetchScoreboardViewModel> GetScoreboard(string gameId)
        {
            var scoreboard = await _context.Players.OrderByDescending(i => i.Score).Where(i => i.GameId == new Guid(gameId)).Select(i => new ScoreboardDto()
            {
                Nickname = i.Nick,
                Score = i.Score
            }).ToListAsync();
            return new FetchScoreboardViewModel(scoreboard);
        }

        public async Task<ServiceResponse<bool>> SwitchPlayer(string gameId)
        {

            var player = await _context.Players.OrderBy(_ => Guid.NewGuid()).FirstOrDefaultAsync(i => i.GameId == new Guid(gameId));

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == player.GameId);

            game.ShowingPlayerId = player.Id;
            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player switched");
        }

        public async Task<ServiceResponse<FetchPlayersViewModel>> FetchPlayers()
        {
            var player = await GetPlayer();

            var players = await _context.Players.Where(x => x.GameId == player.GameId).ToListAsync();

            return ServiceResponse<FetchPlayersViewModel>.Ok(new FetchPlayersViewModel(players));
        }

        private async Task<(ServiceResponse<T>, Player)> ValidateRequest<T>(string gameId, T responseType)
        {
            var player = await GetPlayer();

            if (player == null)
                return (ServiceResponse<T>.Error("No player found"), null);

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id.ToString() == gameId);

            if (game == null)
                return (ServiceResponse<T>.Error("No game found"), null);

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == player.RoomId);

            if (room == null)
                return (ServiceResponse<T>.Error("No room found"), null);

            player.Room = room;
            player.Game = game;

            return (ServiceResponse<T>.Ok(responseType, "Validate "), player);
        }

        public async Task<Game> GetGame(string gameId)
        {
            return await _context.Games.FirstOrDefaultAsync(i => i.Id.ToString() == gameId);
        }
    }
}
