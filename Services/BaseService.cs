using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Models;

namespace PunsApi.Services
{
    public class BaseService
    {
        protected readonly AppDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Player> GetCurrentPlayer()
        {
            var playerId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return await _context.Players.FirstOrDefaultAsync(x => x.Id.ToString() == playerId);
        }
    }
}
