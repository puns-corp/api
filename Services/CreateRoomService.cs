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
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Authenticate;
using PunsApi.ViewModels.CreateRoom;

namespace PunsApi.Services
{
    public class CreateRoomService : BaseService, ICreateRoomService
    {
        private readonly IJwtHelper _jwtHelper;

        public CreateRoomService(AppDbContext context, IJwtHelper jwtHelper) : base(context)
        {
            _jwtHelper = jwtHelper;
        }

        public Task<ServiceResponse<CreateRoomViewModel>> Close(CreateRoomRequest request)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<ServiceResponse<bool>> Create(CreateRoomRequest request)
        {
            var isRoomNameValid = StringValidator.IsRoomNameValid(request.RoomName);
            if(!isRoomNameValid)
                return ServiceResponse<bool>.Error("Invalid email");
            var newRoom = new Room
            {
                Id = Guid.NewGuid(),
                RoomName = request.RoomName,
                PlayerMinCount = request.PlayerMinCount==0?2:request.PlayerMinCount,
                PlayerMaxCount = request.PlayerMaxCount
            };

            await _context.Rooms.AddAsync(newRoom);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Room created");
        }

        public Task<ServiceResponse<CreateRoomViewModel>> Open(CreateRoomRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
