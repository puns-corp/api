using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Helpers
{
    public static class StringValidator
    {
        public static bool IsRoomNameValid(string RoomName)
        {
            return RoomName.Length > 2 ? true : false;
        }
    }
}
