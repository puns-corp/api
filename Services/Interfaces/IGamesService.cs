using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Requests.Games;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Games;
using PunsApi.ViewModels.Rooms;

namespace PunsApi.Services.Interfaces
{
    public interface IGamesService
    {
        Task<ServiceResponse<bool>> CreateGame(CreateGameRequest request);
        
        Task<ServiceResponse<FetchGameViewModel>> FetchGame();

        Task<ServiceResponse<bool>> JoinGame(string gameId, string connectionId);

        Task<ServiceResponse<bool>> QuitGame(string gameId, string connectionId);

        Task<ServiceResponse<bool>> GameStart(string gameId);

        Task<ServiceResponse<bool>> GameEnd(string gameId);

        Task<ServiceResponse<FetchPasswordsViewModel>> FetchPasswords();
        
        Task<ServiceResponse<bool>> PlayerScored(string nextPlayerId);

        //Task<ServiceResponse<bool>> SwitchPlayer(string gameId, string playerId);

        Task<ServiceResponse<FetchPlayersViewModel>> FetchPlayers();
    }
}
