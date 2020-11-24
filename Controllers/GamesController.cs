using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PunsApi.Requests.Games;
using PunsApi.Services;
using PunsApi.Services.Interfaces;

namespace PunsApi.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
        {
            var result = await _gamesService.CreateGame(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Join(string gameId)
        {
            var result = await _gamesService.JoinGame(gameId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Quit(string gameId)
        {
            var result = await _gamesService.QuitGame(gameId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }


}
