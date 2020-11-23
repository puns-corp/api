using PunsApi.Requests.CreateRoom;
using PunsApi.Services.ServicesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Requests.Room;
using PunsApi.ViewModels.Room;

namespace PunsApi.Services.Interfaces
{
    public interface IRoomService
    {
        Task<ServiceResponse<CreateRoomViewModel>> CreateRoom(CreateRoomRequest request, string playerId);
        Task<ServiceResponse<bool>> JoinRoom(string roomId);
        Task<ServiceResponse<bool>> QuitRoom(string roomId);
    }
}
