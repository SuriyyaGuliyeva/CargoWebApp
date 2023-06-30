using Cargo.Core.Constants;
using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using CargoApi.Exceptions;
using CargoApi.Models.AccountModels;
using CargoApi.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CargoApi.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager = null, IUnitOfWork unitOfWork = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseModel> Authenticate(LoginRequestModel requestModel)
        {
            var user = await _userManager.FindByNameAsync(requestModel.Name);

            if (user == null)
            {
                throw new AppException("Username or Password is incorrect");
            }

            var result = await _signInManager.PasswordSignInAsync(user, requestModel.PasswordHash, false, false);

            if (result.Succeeded == false)
            {
                throw new AppException("Username or Password is incorrect");
            }

            return new LoginResponseModel
            {
                JwtToken = GenerateToken(user)
            };
        }

        public async Task Register(RegisterRequestModel requestModel)
        {
            var user = new User
            {
                Name = requestModel.Name
            };

            var result = await _userManager.CreateAsync(user, requestModel.PasswordHash);

            if (result.Succeeded == false)
            {
                string message = ExtractErrorMessage(result);
                throw new AppException(message);
            }

            await _unitOfWork.SaveAsync();
        }

        // To generate Token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKeyConstant.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber.ToString())
            };

            var token = new JwtSecurityToken(null, null,
                claims,
                expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private string ExtractErrorMessage(IdentityResult result)
        {
            return string.Join('\n', result.Errors.Select(x => x.Description));
        }

    }
}
