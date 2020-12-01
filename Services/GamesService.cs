using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Helpers;
using PunsApi.Hubs;
using PunsApi.Hubs.Interfaces;
using PunsApi.Models;
using PunsApi.Requests.Games;
using PunsApi.Requests.Rooms;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
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

            if(room == null)
                return ServiceResponse<bool>.Error("No room found");

            var isRoomNameValid = NamesValidator.IsNameValid(request.GameName);

            if (!isRoomNameValid)
                return ServiceResponse<bool>.Error("Game name is too short");

            var newGame = new Game
            {
                Id = Guid.NewGuid(),
                Name = request.GameName,
                RoomId = room.Id
            };

            await _context.Games.AddAsync(newGame);
            await _context.SaveChangesAsync();
            player.GameId = newGame.Id;
            player.IsGameMaster = true;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Game created");
        }

        public async Task<ServiceResponse<bool>> JoinGame(string gameId)
        {
            var (response, player) = await ValidateRequest(gameId, true);

            if (!response.Success)
                return response;

            player.GameId = player.Game.Id;
            _context.Update(player);
            await _context.SaveChangesAsync();

            await _gameHubContext.Clients.Group(gameId).PlayerJoined(player.Nick);

            return ServiceResponse<bool>.Ok(true, "Player joined to game");
        }

        public async Task<ServiceResponse<bool>> QuitGame(string gameId)
        {
            var (response, player) = await ValidateRequest(gameId, true);

            if (!response.Success)
                return response;

            if (player.Room.Id != player.Game.RoomId)
                return ServiceResponse<bool>.Error("User isn't in this room");

            player.GameId = null;
            _context.Update(player);
            await _context.SaveChangesAsync();

            await _gameHubContext.Clients.Group(gameId).PlayerQuit(player.Nick);

            return ServiceResponse<bool>.Ok(true, "Player quit game");
        }

        public async Task<ServiceResponse<bool>> StartGame(string gameId)
        {
            var (response, player) = await ValidateRequest(gameId, true);

            if (!response.Success)
                return response;

            if (!player.IsGameMaster)
                return ServiceResponse<bool>.Error("Player isn't game master");

            await _gameHubContext.Clients.Group(gameId).GameStarted();

            return ServiceResponse<bool>.Ok(true, "Game started");
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
    }
}
