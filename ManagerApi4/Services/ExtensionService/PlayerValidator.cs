using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ManagerApi4.Models;

namespace ManagerApi4.Services.ExtensionService
{
    public class PlayerValidator:AbstractValidator<PlayerViewModel>
    {
        public PlayerValidator()
        {
            RuleFor(x=>x.Age).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Age).LessThan(45);
            RuleFor(x => x.Name).MaximumLength(200);
            RuleFor(x => x.Name).MinimumLength(2);
        }
    }
}
