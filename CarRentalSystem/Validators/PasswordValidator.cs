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
                errors.Add(new IdentityError { Description = "Fjalëkalimi duhet të jetë të paktën 8 karaktere i gjatë." });
            }

            if (!password.Any(char.IsUpper))
            {
                errors.Add(new IdentityError { Description = "Fjalëkalimi duhet të përmbajë të paktën një shkronjë të madhe." });
            }

            if (!password.Any(char.IsDigit))
            {
                errors.Add(new IdentityError { Description = "Fjalëkalimi duhet të përmbajë të paktën një numër." });
            }

            if (!password.Any(ch => "!@#$%^&*".Contains(ch)))
            {
                errors.Add(new IdentityError { Description = "Fjalëkalimi duhet të përmbajë të paktën një nga këto karaktere speciale: !@#$%^&*." });
            }

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}