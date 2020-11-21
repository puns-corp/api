using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.ViewModels.Authenticate
{
    public class AuthenticateViewModel
    {
        public Guid Id { get; set; }

        public string Nick { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public AuthenticateViewModel(Player player, string jwtToken, string refreshToken)
        {
            Id = player.Id;
            Nick = player.Nick;
            AccessToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
