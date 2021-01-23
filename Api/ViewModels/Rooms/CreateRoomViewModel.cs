using System;

namespace PunsApi.ViewModels.Rooms
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
