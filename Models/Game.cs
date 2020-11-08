using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Game
    {
        
        public Guid Id { get; set; }
        [ForeignKey("Rooms")]
        public Guid RoomId { get; set; }
        public Rooms Rooms { get; set; }
        public ICollection<Player> Players { get; set; } 

    }
}
