﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PunsApi.Data;
using PunsApi.Helpers;
using PunsApi.Helpers.Interfaces;
using PunsApi.Models;
using PunsApi.Requests.CreateRoom;
using PunsApi.Services.Interfaces;
using PunsApi.Services.ServicesResponses;
using PunsApi.ViewModels.Authenticate;

namespace PunsApi.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly PasswordHasher<Player> _passwordHasher;
        private readonly PlayerPasswordValidator _passwordValidator;
        private readonly IJwtHelper _jwtHelper;


        public AuthenticationService(AppDbContext context, PasswordHasher<Player> passwordHasher,
            PlayerPasswordValidator passwordValidator, IJwtHelper jwtHelper) : base(context)
        {
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _jwtHelper = jwtHelper;
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

        public async Task<ServiceResponse<AuthenticateViewModel>> Login(AuthenticateRequest request)
        {
            var player = await _context.Players.Include(
                x => x.RefreshToken).FirstOrDefaultAsync(
                x => x.Email == request.Email);

            if (player == null)
                return ServiceResponse<AuthenticateViewModel>.Error("User for this email does not exist");

            var verifyPasswordResult = _passwordHasher.VerifyHashedPassword(player, player.PasswordHash, request.Password);

            if (verifyPasswordResult != PasswordVerificationResult.Success)
                return ServiceResponse<AuthenticateViewModel>.Error("Invalid password");

            var jwtToken = _jwtHelper.GenerateJwtToken(player);

            var refreshToken = _jwtHelper.GenerateRefreshToken();

            player.RefreshToken = refreshToken;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<AuthenticateViewModel>.Ok(
                new AuthenticateViewModel(player, jwtToken, refreshToken.Token));
        }

        public async Task<ServiceResponse<AuthenticateViewModel>> RefreshToken(RefreshTokenRequest request)
        {
            var user = await _context.Players.Include(
                x => x.RefreshToken).FirstOrDefaultAsync(
                u => u.RefreshToken.Token == request.RefreshToken);

            if (user == null)
                return ServiceResponse<AuthenticateViewModel>.Error("Invalid refresh token");

            if (!user.RefreshToken.IsActive)
                return ServiceResponse<AuthenticateViewModel>.Error("Refresh token is not active");

            var newRefreshToken = _jwtHelper.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _context.Update(user);
            await _context.SaveChangesAsync();

            var jwtToken = _jwtHelper.GenerateJwtToken(user);

            return ServiceResponse<AuthenticateViewModel>.Ok(new AuthenticateViewModel(user, jwtToken, newRefreshToken.Token));
        }

        public async Task<ServiceResponse<bool>> RevokeToken(RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return ServiceResponse<bool>.Error("Refresh token is required");

            var player = 
                await _context.Players.Include(
                    x => x.RefreshToken).FirstOrDefaultAsync(
                    u => u.RefreshToken.Token == request.RefreshToken);

            if (player == null)
                return ServiceResponse<bool>.Error("Invalid refresh token");

            var refreshToken = player.RefreshToken;

            if (!refreshToken.IsActive)
                return ServiceResponse<bool>.Error("Refresh token is not active");

            refreshToken.Revoked = true;
            _context.Update(player);
            await _context.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true, "Token revoked");
        }
    }
}
