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
        public async Task<IActionResult> FetchPasswords()
        {
            var result = await _gamesService.FetchPasswords();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FetchPlayers()
        {
            var result = await _gamesService.FetchPlayers();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FetchGame()
        {
            var result = await _gamesService.FetchGame();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }


}
