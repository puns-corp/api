﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PunsApi.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Nick { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public RefreshToken RefreshToken { get; set; }

        public bool IsGameMaster { get; set; } = false;

        public bool IsPlaying { get; set; } = false;

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public Guid? RoomId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public Guid? GameId { get; set; }
    }
}
