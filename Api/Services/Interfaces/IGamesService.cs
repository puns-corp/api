using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Dtos.Games;
using PunsApi.Models;
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

        Task<ServiceResponse<bool>> SwitchPlayer(string gameId);

        Task<FetchScoreboardViewModel> GetScoreboard(string gameId);

        Task<ServiceResponse<FetchPlayersViewModel>> FetchPlayers();

        Task<Game> GetGame(string gameId);
    }
}
