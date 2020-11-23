using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;
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

        public RoomService(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }


        public async Task<ServiceResponse<CreateRoomViewModel>> CreateRoom(CreateRoomRequest request)
        {
            var player = await GetCurrentPlayer();

            if (player == null)
                return ServiceResponse<CreateRoomViewModel>.Error("No user found");

            var isRoomNameValid = RoomNameValidator.IsRoomNameValid(request.RoomName);

            if (!isRoomNameValid)
                return ServiceResponse<CreateRoomViewModel>.Error("Invalid email");

            var newRoom = new Room
            {
                Id = Guid.NewGuid(),
                RoomName = request.RoomName,
                PlayerMinCount = request.PlayerMinCount == 0 ? 2 : request.PlayerMinCount,
                PlayerMaxCount = request.PlayerMaxCount
            };

            await _context.Rooms.AddAsync(newRoom);
            player.RoomId = newRoom.Id;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<CreateRoomViewModel>.Ok(new CreateRoomViewModel(newRoom));
        }

        public async Task<ServiceResponse<bool>> JoinRoom(string roomId)
        {
            var player = await GetCurrentPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No user found");

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.ToString() == roomId);

            if (room == null)
                return ServiceResponse<bool>.Error("No room found");

            player.RoomId = room.Id;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player joined to room");
        }

        public async Task<ServiceResponse<bool>> QuitRoom(string roomId)
        {
            var player = await GetCurrentPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No user found");

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.ToString() == roomId);

            if (room == null)
                return ServiceResponse<bool>.Error("No room found");

            player.RoomId = null;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player quit room");
        }
    }
}
