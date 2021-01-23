using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PunsApi.Hubs.Interfaces;
using PunsApi.Requests.Games;
using PunsApi.Services.Interfaces;

namespace PunsApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Hub<IGameHub>
    {
        private readonly IGamesService _gamesService;

        public GameHub(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }


        public async Task JoinGame(string gameId)
        {
            var result = await
                _gamesService.JoinGame(gameId, Context.ConnectionId);

            if (result.Success)
            {
                var scoreboard = await _gamesService.GetScoreboard(gameId);
                await Clients.Groups(gameId).Scoreboard(scoreboard);
            }

            if (!result.Success)
                await Clients.Caller.SendErrorMessage(result.Message);
        }

        public async Task QuitGame(string gameId)
        {
            var result = await
                _gamesService.QuitGame(gameId, Context.ConnectionId);

            if (!result.Success)
                await Clients.Caller.SendErrorMessage(result.Message);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task GameStart(string gameId)
        {
            var result = await
                _gamesService.GameStart(gameId);

            if (!result.Success)
                await Clients.Caller.SendErrorMessage(result.Message);
        }

        public async Task GameEnd(string gameId)
        {
            var result = await
                _gamesService.GameEnd(gameId);

            if (!result.Success)
                await Clients.Caller.SendErrorMessage(result.Message);
            if (result.Success)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
                await _gamesService.QuitGame(gameId, Context.ConnectionId);
            }
        }

        public async Task PlayerGuessed(string gameId, string nextPlayerId)
        {

            var result =
                await _gamesService.PlayerScored(nextPlayerId);

            if (result.Success)
            {
                var game = await _gamesService.GetGame(gameId);
                if (!game.GameEnded)
                {
                    await Clients.Groups(gameId).PlayerGuessed(nextPlayerId);
                    await Clients.Groups(gameId).PlayerScored(nextPlayerId);
                }
                var scoreboard = await _gamesService.GetScoreboard(gameId);
                await Clients.Groups(gameId).Scoreboard(scoreboard);

            }
        }

        public async Task NewShowingPlayer(string gameId, string playerId)
        {
            await _gamesService.SwitchPlayer(gameId);
        }

        public async Task RemoveFromGameGroup(string gameId, string playerId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
            await _gamesService.QuitGame(gameId, Context.ConnectionId);

            await Clients.Group(gameId).PlayerQuit(playerId);

            await _gamesService.SwitchPlayer(gameId);
        }
    }
}
