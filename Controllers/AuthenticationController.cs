using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PunsApi.Requests.Authentication;
using PunsApi.Services.Interfaces;

namespace PunsApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthenticateRequest request)
        {
            var result = await _authenticationService.Register(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        public Task<IActionResult> Login()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> RevokeToken()
        {
            throw new NotImplementedException();
        }

    }
}
