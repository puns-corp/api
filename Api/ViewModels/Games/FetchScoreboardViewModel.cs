using PunsApi.Dtos.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.ViewModels.Games
{
    public class FetchScoreboardViewModel
    {
        public List<ScoreboardDto> Scores { get; set; }

        public FetchScoreboardViewModel(List<ScoreboardDto> entries)
        {
            Scores = new List<ScoreboardDto>();

            foreach (var entry in entries)
            {
                Scores.Add(entry);
            }
        }
    }
}
