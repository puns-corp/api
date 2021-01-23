using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Dtos.Games;
using PunsApi.ViewModels.Games;

namespace PunsApi.Hubs.Interfaces
{
    public interface IGameHub
    {
        Task PlayerJoined(string playerId);

        Task PlayerQuit(string playerId);

        Task GameStarted();

        Task GameEnded(string nickname);

        Task PlayerGuessed(string nextPlayerId);

        Task SwitchPlayer();

        Task NewShowingPlayer(string playerId);

        Task PlayerScored(string nextPlayerId);

        Task SendErrorMessage(string errorMessage);
        Task Scoreboard(FetchScoreboardViewModel scoreboard);

    }
}
