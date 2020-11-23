using PunsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.ViewModels.Room
{
    public class CreateRoomViewModel
    {
        public Guid Id { get; set; }

        public string RoomName { get; set; }

        public CreateRoomViewModel(Models.Room room)
        {
            Id = room.Id;
            RoomName = room.RoomName;
        }
    }
}
