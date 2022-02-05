using Microsoft.AspNetCore.Identity;
using ManagerApi4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApi4.Services
{
    public interface IUserService
    {
        Task<string> SignupAsync(SignUpModel model);

        Task<Token> LoginAsync(LoginModel model);

        Task<string> AdminRegister(SignUpModel model);
    }
}
