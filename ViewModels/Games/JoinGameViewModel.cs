using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.ViewModels.Games
{
    public class JoinGameViewModel
    {
        public string GameId { get; set; }

        public string GameMasterId { get; set; }

        public List<Player> Players { get; set; }

        public JoinGameViewModel()
        {

        }

        public JoinGameViewModel(string gameId, string gameMasterId, List<Player> players)
        {
            GameId = gameId;
            GameMasterId = gameMasterId;
            Players = players;
        }
    }
}
