using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ManagerApi4.Entities;
using ManagerApi4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApi4.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;

        public UserService(UserManager<User> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }


        public async Task<string> SignupAsync(SignUpModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return "User already exists!";
            var user = new User();
            user.UserName = model.Username;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.Email = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return "User creation failed! Please check user details and try again.";

            return null;
        }

        public async Task<Token> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.CreateToken(user, userRoles);
                return token;
            }
            throw new InvalidOperationException("There is no user with that name.Please check your name or Invalid Password");

        }

        public async Task<string> AdminRegister(SignUpModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return "User already exists!";
            var user = new User();
            user.UserName = model.Username;
            user.SecurityStamp = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return "User creation failed! Please check user details and try again.";
            await _userManager.AddToRoleAsync(user,Role.Admin);
            return null;
        }
    }
}
