using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Requests.CreateRoom
{
    public class CreateRoomRequest
    {
        [Required]
        public string RoomName { get; set; }
        public int PlayerMinCount { get; set; }
        [Required]
        public int PlayerMaxCount { get; set; }
    }
}
