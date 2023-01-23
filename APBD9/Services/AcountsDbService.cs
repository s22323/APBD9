using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using APBD9.DTOs;
using APBD9.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace APBD9.Services
{
    public class AcountsDbService : IAccountsDbService
    {

        private readonly Context context;
        private readonly IConfiguration configuration;

        public AcountsDbService(Context context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task AddUser(LoginRequest request)
        {
            string password = request.Password;
            if (password.Length < 8)
            {
                throw new Exception("Password should be longer than 8 characters");
            }

            string user = request.Login;
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            string saltBase64 = Convert.ToBase64String(salt);

            var appuser = new AppUser
            {
                Login = user,
                Password = hashed,
                Salt = saltBase64,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExp = DateTime.Now.AddHours(10)
        };

            return Context.AppUsers.Add(appuser);
        }

        public async Task<String> Login(LoginRequest loginRequest)
        {
            AppUser user = context.AppUsers.FirstOrDefault(e => e.Login == loginRequest.Login);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            byte[] salt = Convert.FromBase64String(user.Salt);
            string currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: loginRequest.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if(user.Password != currentHashedPassword)
            {
                return Unauthorized();
            }
            var token = getJwtToken();

            await context.SaveChangesAsync();

        string AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return AccessToken;

        }

        public Task<String> refreshToken(string token)
    {
        var user = Context.AppUsers.ToList().FirstOrDefault(e => e.RefreshToken == token);
        if (user != null)
        {
            if (user.RefreshTokenExp < DateTime.Now)
            {
                token = user.RefreshToken;
                user.RefreshToken = Guid.NewGuid().ToString();
                user.RefreshTokenExp = DateTime.Now.AddDays(10);

                Context.SaveChanges();
            }
        }
        return Ok(token);
    }
        private JwtSecurityToken getJwtToken()
        {
            Claim[] userClaims = new[]
            {
                new Claim(ClaimTypes.Role,"admin"),
                new Claim(ClaimTypes.Role,"user") };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));

                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost",
            audience: "https://localhost",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
            return token;
        }
        }
    }

