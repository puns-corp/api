using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Dtos.Games;
using PunsApi.Models;

namespace PunsApi.ViewModels.Games
{
    public class FetchGameViewModel
    {
        public GameDto Game { get; set; }

        public FetchGameViewModel(Game game)
        {
            Game = new GameDto(game);
        }
    }
}
