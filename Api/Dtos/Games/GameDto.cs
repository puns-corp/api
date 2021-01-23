using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.Dtos.Games
{
    public class GameDto
    {
        public Guid Id { get; set; }

        public Guid GameMasterId { get; set; }

        public Guid RoomId { get; set; }

        public string Name { get; set; }

        public Guid? ShowingPlayerId { get; set; }

        public List<PlayerDto> Players { get; set; }

        public bool GameStarted { get; set; }

        public bool GameEnded { get; set; }

        public GameDto(Game game)
        {
            Id = game.Id;
            GameMasterId = game.GameMasterId;
            RoomId = game.RoomId;
            Name = game.Name;
            GameStarted = game.GameStarted;
            GameEnded = game.GameEnded;
            ShowingPlayerId = game.ShowingPlayerId;

            Players = new List<PlayerDto>();
            foreach (var player in game.Players)
            {
                Players.Add(new PlayerDto(player));
            }
        }
    }
}
