using PunsApi.Controllers;
using PunsApi.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PunsApi.Services.Interfaces;
using Microsoft.Net.Http.Headers;
namespace PUNS.TESTS
{
    public class AuthenticationControllerUnitTest : TestServerDependent
    {
        private AuthenticationController controller;
        private IAuthenticationService _authenticationService;
        private readonly AppDbContext dbContext;

        public AuthenticationControllerUnitTest(TestServerFixture fixture) : base(fixture)
        {
        
        }
        [Fact]
        public async Task RegisterLoginTest()
        {
            _authenticationService = GetService<IAuthenticationService>();
            controller = new AuthenticationController(_authenticationService);
            var response = await controller.Register(new PunsApi.Requests.Authentication.RegisterRequest 
            {  
                Email="p10trek@o2.pl",
                Nick="p10trek",
                Password="pwsz00145tfe_!a"
            });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode == 200);
            var loginResponse = await controller.Login(new PunsApi.Requests.Authentication.LoginRequest { Email = "p10trek@o2.pl", Password = "pwsz00145tfe_!a" });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)loginResponse).StatusCode == 200);
            var refreshTokenResponse = await controller
                .RefreshToken(new PunsApi.Requests.Authentication.RefreshTokenRequest { RefreshToken = ((PunsApi.Services.ServicesResponses.ServiceResponse<PunsApi.ViewModels.Authenticate.AuthenticateViewModel>)((Microsoft.AspNetCore.Mvc.ObjectResult)loginResponse).Value).Data.RefreshToken });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)refreshTokenResponse).StatusCode == 200);
            var RevokeTokenResponse = await controller
                .RevokeToken(new PunsApi.Requests.Authentication.RefreshTokenRequest { RefreshToken = ((PunsApi.Services.ServicesResponses.ServiceResponse<PunsApi.ViewModels.Authenticate.AuthenticateViewModel>)((Microsoft.AspNetCore.Mvc.ObjectResult)refreshTokenResponse).Value).Data.RefreshToken });
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)RevokeTokenResponse).StatusCode == 200);
            //var FetchUserResponse = await controller.FetchUser();
            //Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)FetchUserResponse).StatusCode == 200);
        }
    } 
}
