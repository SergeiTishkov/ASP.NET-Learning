using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Age).GreaterThan(10).WithMessage("Something wrong");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).Must(x => x.StartsWith("a"));
            RuleFor(x => x.FirstName).NotEmpty().When(x => !string.IsNullOrEmpty(x.LastName));
        }
    }
}