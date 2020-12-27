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

            if (!result.Success)
                await Clients.Caller.SendErrorMessage(result.Message);
        }

        public async Task QuitGame(string gameId)
        {
            var result = await
                _gamesService.QuitGame(gameId, Context.ConnectionId);

            if (!result.Success)
                await Clients.Caller.SendErrorMessage(result.Message);
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
        }
        //do wszystkich pójdzie, że ktoś zgadł, ale tylko ten z własciwym id
        //wyśle requesta, że on ma być teraz pokazującym
        public async Task PlayerGuessed(string gameId, string nextPlayerId)
        {
            await Clients.Groups(gameId).PlayerGuessed(nextPlayerId);
            
            var result =
                await _gamesService.PlayerScored(nextPlayerId);

            if (result.Success)
                await Clients.Groups(gameId).PlayerScored(nextPlayerId);
        }

        public async Task NewShowingPlayer(string gameId, string playerId)
        {
            //var result = await _gamesService.SwitchPlayer(gameId, playerId);
            //var result = await _gamesService.NewShowingPlayer(playerId);
            //if (result.Success)
            //{
                //await Clients.Caller.SwitchPlayer();
                await Clients.Groups(gameId).NewShowingPlayer(playerId);
            //}
        }

        public async Task RemoveFromGameGroup(string gameId, string playerId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).PlayerQuit(playerId);
        }
    }
}
