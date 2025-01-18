using CarRentalSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Validators
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            var errors = new List<IdentityError>();

            if (password.Length < 8)
            {
                errors.Add(new IdentityError { Description = "Password must be at least 8 characters long." });
            }

            if (!password.Any(char.IsUpper))
            {
                errors.Add(new IdentityError { Description = "Password must contain at least one uppercase letter." });
            }

            if (!password.Any(char.IsDigit))
            {
                errors.Add(new IdentityError { Description = "Password must contain at least one number." });
            }

            if (!password.Any(ch => "!@#$%^&*".Contains(ch)))
            {
                errors.Add(new IdentityError { Description = "Password must contain at least one special character (!@#$%^&*)." });
            }

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}