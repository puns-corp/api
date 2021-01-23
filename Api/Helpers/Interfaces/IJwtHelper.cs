using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PunsApi.Models;

namespace PunsApi.Helpers.Interfaces
{
    public interface IJwtHelper
    {
        string GenerateJwtToken(Player player);

        RefreshToken GenerateRefreshToken();
    }
}
