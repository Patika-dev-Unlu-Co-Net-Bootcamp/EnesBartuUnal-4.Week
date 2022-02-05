using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ManagerApi4.Entities;
using ManagerApi4.Models;
using AutoMapper;
using ManagerApi4.Services;

namespace ManagerApi4.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignupAsync(model);
                if (result == null)
                {
                    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model);
                return Ok(result);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPost]
        [Route("admin-signup")]
        public async Task<IActionResult> RegisterAdmin([FromBody] SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AdminRegister(model);
                if (result == null)
                {
                    return Ok(new Response { Status = "Success", Message = "User(Admin) created successfully!" });
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest("Some properties are not valid!");
        }


    }



}
