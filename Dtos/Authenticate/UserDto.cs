using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.Dtos.Authenticate
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Nick { get; set; }

        public Guid? RoomId { get; set; } = null;

        public Guid? GameId { get; set; } = null;

        public UserDto()
        {
            
        }

        public UserDto(Player player)
        {
            Id = player.Id;
            Nick = player.Nick;
            RoomId = player.RoomId;
            GameId = player.GameId;
        }
    }
}
