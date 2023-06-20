using Cargo.Core.Constants;
using Cargo.Core.Domain.Entities;
using CargoApi.Exceptions;
using CargoApi.Models.AccountModels;
using CargoApi.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CargoApi.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResponseModel> Authenticate(LoginRequestModel requestModel)
        {
            var user = await _userManager.FindByNameAsync(requestModel.Name);

            if (user == null)
            {
                throw new AppException("Username or Password is incorrect.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, requestModel.PasswordHash, false, false);

            if (result.Succeeded == false)
            {
                throw new AppException("Username or Password is incorrect.");
            }

            return new LoginResponseModel
            {
                JwtToken = GenerateToken(user)
            };
        }

        // To generate token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKeyConstant.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(null, null,
                claims,
                expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
