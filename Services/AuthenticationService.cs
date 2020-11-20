using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Helpers;
using PunsApi.Models;
using PunsApi.Requests.Authentication;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;

namespace PunsApi.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly PasswordHasher<Player> _passwordHasher;
        private readonly PlayerPasswordValidator _passwordValidator;


        public AuthenticationService(AppDbContext context, PasswordHasher<Player> passwordHasher,
            PlayerPasswordValidator passwordValidator) : base(context)
        {
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }

        public async Task<ServiceResponse<bool>> Register(AuthenticateRequest request)
        {
            var isEmailValid = EmailValidator.IsEmailValid(request.Email);

            if(!isEmailValid)
                return ServiceResponse<bool>.Error("Invalid email");

            var player = await _context.Players.FirstOrDefaultAsync(
                x => x.Email == request.Email);

            if (player != null)
                return ServiceResponse<bool>.Error("Email already exist");

            var (isPasswordValid, passwordMessage) = _passwordValidator.ValidateAsync(request.Password);

            if(!isPasswordValid)
                return ServiceResponse<bool>.Error(passwordMessage);

            var hashedPassword = 
                _passwordHasher.HashPassword(player, request.Password);

            var newPlayer = new Player
            {
                Email = request.Email,
                PasswordHash = hashedPassword,
            };

            await _context.Players.AddAsync(newPlayer);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "User created");
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
