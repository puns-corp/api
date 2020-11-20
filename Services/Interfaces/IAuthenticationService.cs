using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Requests.Authentication;
using PunsApi.Services.ServicesResponses;

namespace PunsApi.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<bool>> Register(AuthenticateRequest request);
        Task<ServiceResponse<bool>> Login();
        Task<ServiceResponse<bool>> RefreshToken();
        Task<ServiceResponse<bool>> RevokeToken();
    }
}
