using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; }

        public string RoomName { get; set; }

        public int PlayerMinCount { get; set; }

        public int PlayerMaxCount { get; set; }

        public bool HasPassword { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Game> Games { get; set; }

    }
}
