using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.ViewModels.Rooms
{
    public class FetchGamesViewModel
    {
        public List<Game> Games { get; set; }


        public FetchGamesViewModel(List<Game> games)
        {
            Games = games;
        }

    }
}
