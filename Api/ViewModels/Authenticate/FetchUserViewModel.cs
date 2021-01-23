using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Dtos.Authenticate;

namespace PunsApi.ViewModels.Authenticate
{
    public class FetchUserViewModel
    {
        public UserDto User { get; set; }

        public FetchUserViewModel(UserDto user)
        {
            User = user;
        }
    }
}
