using PunsApi.Requests.CreateRoom;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.CreateRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Services.Interfaces
{
    public interface ICreateRoomService
    {
        Task<ServiceResponse<bool>> Create(CreateRoomRequest request);
        Task<ServiceResponse<CreateRoomViewModel>> Open(CreateRoomRequest request);
        Task<ServiceResponse<CreateRoomViewModel>> Close(CreateRoomRequest request);
    }
}
