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

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public Guid RoomId { get; set; }

        public ICollection<Player> Players { get; set; } 

    }
}
