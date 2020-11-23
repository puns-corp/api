using PunsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.ViewModels.CreateRoom
{
    public class CreateRoomViewModel
    {
        public Guid Id { get; set; }

        public string RoomName { get; set; }

        public string RoomLink { get; set; }

        public CreateRoomViewModel(Room room, string roomLink)
        {
            Id = room.Id;
            RoomName = room.RoomName;
            RoomLink = roomLink;
        }
    }
}
