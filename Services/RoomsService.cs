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
using PunsApi.Requests.Rooms;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Authenticate;
using PunsApi.ViewModels.Rooms;

namespace PunsApi.Services
{
    public class RoomsService : BaseService, IRoomsService
    {

        public RoomsService(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public async Task<ServiceResponse<CreateRoomViewModel>> CreateRoom(CreateRoomRequest request)
        {
            var player = await GetPlayer();

            if (player == null)
                return ServiceResponse<CreateRoomViewModel>.Error("No user found");

            var isRoomNameValid = NamesValidator.IsNameValid(request.RoomName);

            if (!isRoomNameValid)
                return ServiceResponse<CreateRoomViewModel>.Error("Room name is too short");

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
            var player = await GetPlayer();

            if (player == null)
                return ServiceResponse<bool>.Error("No user found");

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.ToString() == roomId);

            if (room == null)
                return ServiceResponse<bool>.Error("No room found, roomId: " + roomId);

            player.RoomId = room.Id;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Player joined to room");
        }

        public async Task<ServiceResponse<FetchGamesViewModel>> FetchGames()
        {
            var player = await GetPlayer();

            if (player == null)
                return ServiceResponse<FetchGamesViewModel>.Error("No user found");

            var roomId = player.RoomId.ToString();

            var games = await _context.Games.Where(
                    x => x.RoomId.ToString() == roomId).ToListAsync();

            return ServiceResponse<FetchGamesViewModel>.Ok(
                new FetchGamesViewModel(games));
        }

        public async Task<ServiceResponse<FetchRoomsViewModel>> FetchRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            if(rooms == null)
                return ServiceResponse<FetchRoomsViewModel>.Error("No rooms found");

            return ServiceResponse<FetchRoomsViewModel>.Ok(
                new FetchRoomsViewModel(rooms));

        }
    }
}
