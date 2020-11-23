using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Helpers;
using PunsApi.Helpers.Interfaces;
using PunsApi.Models;
using PunsApi.Requests.CreateRoom;
using PunsApi.Requests.Room;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Authenticate;
using PunsApi.ViewModels.Room;

namespace PunsApi.Services
{
    public class RoomService : BaseService, IRoomService
    {

        public RoomService(AppDbContext context) : base(context)
        {
        }


        public async Task<ServiceResponse<CreateRoomViewModel>> CreateRoom(CreateRoomRequest request)
        {
            var isRoomNameValid = RoomNameValidator.IsRoomNameValid(request.RoomName);

            if(!isRoomNameValid)
                return ServiceResponse<CreateRoomViewModel>.Error("Invalid email");

            var newRoom = new Room
            {
                Id = Guid.NewGuid(),
                RoomName = request.RoomName,
                PlayerMinCount = request.PlayerMinCount == 0 ? 2 : request.PlayerMinCount,
                PlayerMaxCount = request.PlayerMaxCount
            };

            await _context.Rooms.AddAsync(newRoom);
            await _context.SaveChangesAsync();

            return ServiceResponse<CreateRoomViewModel>.Ok(new CreateRoomViewModel(newRoom));
        }

        public Task<ServiceResponse<bool>> JoinRoom(string roomId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> QuitRoom(string roomId)
        {
            throw new NotImplementedException();
        }
    }
}
