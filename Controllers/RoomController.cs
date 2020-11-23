using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PunsApi.Requests.CreateRoom;
using PunsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Controllers
{
    public class RoomController : BaseController
    {
        private readonly ICreateRoomService _createRoomService;

        public RoomController(ICreateRoomService createRoomService)
        {
            _createRoomService = createRoomService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            var result = await _createRoomService.Create(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Test([FromBody] CreateRoomRequest request)
        {
            var result = await _createRoomService.Create(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
