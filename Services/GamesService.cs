using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Helpers;
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
        public GamesService(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<ServiceResponse<bool>> CreateGame(CreateGameRequest request)
        {
            var player = await GetCurrentPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No player found");

            var room = _context.Rooms.FirstOrDefault(x => x.Id == player.RoomId);

            if(room == null)
                return ServiceResponse<bool>.Error("No room found");

            var isRoomNameValid = NamesValidator.IsRoomNameValid(request.GameName);

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
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Game created");
        }

        public async Task<ServiceResponse<bool>> JoinGame(string gameId)
        {
            var player = await GetCurrentPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No player found");

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id.ToString() == gameId);

            if(game == null)
                return ServiceResponse<bool>.Error("No game found");

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == player.RoomId);

            if (room == null)
                return ServiceResponse<bool>.Error("No room found");

            if(room.Id != game.RoomId)
                return ServiceResponse<bool>.Error("User isn't in this room");

            player.GameId = game.Id;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player joined to game");
        }

        public async Task<ServiceResponse<bool>> QuitGame(string gameId)
        {
            var player = await GetCurrentPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No player found");

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id.ToString() == gameId);

            if (game == null)
                return ServiceResponse<bool>.Error("No game found");

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == player.RoomId);

            if (room == null)
                return ServiceResponse<bool>.Error("No room found");

            if (room.Id != game.RoomId)
                return ServiceResponse<bool>.Error("User isn't in this room");

            player.GameId = null;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player quit game");
        }
    }
}
