using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PunsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PunsApi.Requests.Rooms;

namespace PunsApi.Controllers
{
    public class RoomsController : BaseController
    {
        private readonly IRoomsService _roomsService;

        public RoomsController(IRoomsService roomsService)
        {
            _roomsService = roomsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            var result = await _roomsService.CreateRoom(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> Join(string roomId)
        {
            var result = await _roomsService.JoinRoom(roomId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FetchGames()
        {
            var result = await _roomsService.FetchGames();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FetchRooms()
        {
            var result = await _roomsService.FetchRooms();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

