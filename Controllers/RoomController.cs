using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PunsApi.Requests.CreateRoom;
using PunsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PunsApi.Requests.Room;

namespace PunsApi.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            var playerId = User.FindFirst(ClaimTypes.Name)?.Value;

            var result = await _roomService.CreateRoom(request, playerId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Join(string roomId)
        {
            var result = await _roomService.JoinRoom(roomId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Quit(string roomId)
        {
            var result = await _roomService.QuitRoom(roomId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

