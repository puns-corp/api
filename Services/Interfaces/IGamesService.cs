using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Requests.Games;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Games;

namespace PunsApi.Services.Interfaces
{
    public interface IGamesService
    {
        Task<ServiceResponse<bool>> CreateGame(CreateGameRequest request);
        Task<ServiceResponse<JoinGameViewModel>> JoinGame(string gameId);
        Task<ServiceResponse<bool>> QuitGame(string gameId);
    }
}
