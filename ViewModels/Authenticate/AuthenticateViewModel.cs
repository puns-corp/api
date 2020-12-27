using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.ViewModels.Authenticate
{
    public class AuthenticateViewModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }


        public AuthenticateViewModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
