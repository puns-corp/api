using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Requests.Authentication;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Authenticate;

namespace PunsApi.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<bool>> Register(AuthenticateRequest request);

        Task<ServiceResponse<AuthenticateViewModel>> Login(AuthenticateRequest request);

        Task<ServiceResponse<AuthenticateViewModel>> RefreshToken(RefreshTokenRequest request);

        Task<ServiceResponse<bool>> RevokeToken(RefreshTokenRequest request);
    }
}
