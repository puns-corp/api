﻿using PunsApi.Services.ServicesResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Requests.Rooms;
using PunsApi.ViewModels.Rooms;

namespace PunsApi.Services.Interfaces
{
    public interface IRoomsService
    {
        Task<ServiceResponse<CreateRoomViewModel>> CreateRoom(CreateRoomRequest request);
        
        Task<ServiceResponse<bool>> JoinRoom(string roomId);
        
        Task<ServiceResponse<FetchGamesViewModel>> FetchGames();

        Task<ServiceResponse<FetchRoomsViewModel>> FetchRooms();

    }
}
