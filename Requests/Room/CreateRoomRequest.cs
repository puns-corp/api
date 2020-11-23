using System.ComponentModel.DataAnnotations;

namespace PunsApi.Requests.Room
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
