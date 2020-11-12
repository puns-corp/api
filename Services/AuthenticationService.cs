using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Data;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;

namespace PunsApi.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticationService(AppDbContext context) : base(context)
        {
        }

        public Task<ServiceResponse<bool>> Register()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> Login()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> RefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> RevokeToken()
        {
            throw new NotImplementedException();
        }
    }
}
