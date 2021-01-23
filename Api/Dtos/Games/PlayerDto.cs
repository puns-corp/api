using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using PunsApi.Models;

namespace PunsApi.Dtos.Games
{
    public class PlayerDto
    {
        public Guid Id { get; set; }

        public string Nick { get; set; }

        public int Score { get; set; }

        public bool IsGameMaster { get; set; }

        public PlayerDto(Player player)
        {
            Id = player.Id;
            Nick = player.Nick;
            Score = player.Score;
            IsGameMaster = player.IsGameMaster;
        }
    }
}
