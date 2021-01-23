using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.ViewModels.Rooms
{
    public class FetchRoomsViewModel
    {
        public List<Room> Rooms { get; set; }

        public FetchRoomsViewModel(List<Room> rooms)
        {
            Rooms = rooms;
        }

    }
}
