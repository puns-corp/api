using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public Guid RoomId { get; set; }

        [ForeignKey("GameMasterId")]
        public Player GameMaster { get; set; }

        public Guid GameMasterId { get; set; }

        [ForeignKey("ShowingPlayerId")]
        public Player ShowingPlayer { get; set; }

        public Guid? ShowingPlayerId { get; set; }

        public ICollection<Player> Players { get; set; }

        public bool GameStarted { get; set; } = false;

        public bool GameEnded { get; set; } = false;
    }
}
