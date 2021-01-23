using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Dtos.Games;
using PunsApi.Models;

namespace PunsApi.ViewModels.Games
{
    public class FetchPlayersViewModel
    {
        public List<PlayerDto> Players { get; set; } = new List<PlayerDto>();

        public FetchPlayersViewModel(List<Player> players)
        {
            foreach (var player in players)
            {
                Players.Add(new PlayerDto(player));
            }
        }
    }
}
