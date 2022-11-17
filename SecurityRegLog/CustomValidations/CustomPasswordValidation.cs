using Microsoft.AspNetCore.Identity;

namespace SecurityRegLog.CustomValidations
{
    public class CustomPasswordValidation : IPasswordValidator<IdentityUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            IdentityOptions options = new IdentityOptions();
            
            
            List<IdentityError> errors = new List<IdentityError>();
            //if (password.Length<6)
            //{
            //    errors.Add(new IdentityError { Code = "PasswordLength", Description = "Lütfen şifreyi en az 5 haneli giriniz.." });
            //}

            //if (password.Length > 100)
            //{
            //    errors.Add(new IdentityError { Code = "PasswordLength", Description = "Lütfen şifreyi en fazla 100 haneli giriniz.." });
            //}
            if (!errors.Any())
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
