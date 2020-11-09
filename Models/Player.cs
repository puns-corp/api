using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        public string Nick { get; set; }

        public string PasswordHash { get; set; }

        public bool IsGameMaster { get; set; }

        public bool IsPlaying { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public Guid GameId { get; set; }
    }
}
