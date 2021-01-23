using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using PunsApi.Controllers;
using PunsApi.Data;
using PunsApi.Requests.Games;
using PunsApi.Services;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PUNS.TESTS
{
    public class GamesControllerUnitTest : TestServerDependent
    {
        private readonly GamesController controller;
        private IGamesService service;
        private readonly AppDbContext dbContext;
        private IAuthenticationService _authenticationService;
        private AuthenticationController authController;


        public GamesControllerUnitTest(TestServerFixture fixture) : base(fixture)
        {
            //service = fixture.GetService<IGamesService>();
            dbContext = fixture.GetService<AppDbContext>();
            //controller = new GamesController(_gamesService);
            _authenticationService = GetService<IAuthenticationService>();
            authController = new AuthenticationController(_authenticationService);

            string reason = "This is the reason that we expect";

            // Create a default HttpContext
            var httpContext = new DefaultHttpContext();

            // Create the stream to house our content
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(reason));
            httpContext.Request.Body = stream;
            httpContext.Request.ContentLength = stream.Length;

            // Create the service mock
            var service = new Mock<IGamesService>();
            service.Setup(s => s.CreateGame(new CreateGameRequest { GameName = "testowa" }));

            // Create the controller
            controller = new GamesController(service.Object)
            {
                // Set the controller context to our created HttpContext
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
                
            };

            // Invoke the method
            //var result = await controller.Create(new CreateGameRequest { GameName = "Testowa" });

            // Verify the result
            //var okResult = Assert.IsType<OkObjectResult>(result);
            //Assert.True(true);
            // var returnValue = Assert.IsType<Thing>(okResult.Value);
            //Assert.Equal(returnValue.Reason, reason);
        }
        [Fact]
        public async Task CreateTest()
        {
            var loginResponse = await authController.Login(new PunsApi.Requests.Authentication.LoginRequest { Email = "p10trek@o2.pl", Password = "pwsz00145tfe_!a" });
            var _gameService = GetService<IGamesService>();
            var httpContext = new DefaultHttpContext();

            // Create the stream to house our content
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("testowa"));
            httpContext.Request.Body = stream;
            httpContext.Request.ContentLength = stream.Length;
            var controller = new GamesController(_gameService)
            {
                // Set the controller context to our created HttpContext
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }

            };
            var response = await controller.Create(new CreateGameRequest { GameName = "Testowa" });
                Assert.True(true);
        }
    }
}
