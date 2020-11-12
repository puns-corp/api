using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Services.ServicesResponses;

namespace PunsApi.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<bool>> Register();
        Task<ServiceResponse<bool>> Login();
        Task<ServiceResponse<bool>> RefreshToken();
        Task<ServiceResponse<bool>> RevokeToken();
    }
}
