using Microsoft.AspNetCore.Identity;

namespace SecurityRegLog.CustomValidations
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        //public override IdentityError DuplicateEmail(string email) => new IdentityError
        //{
        //    Code = "DuplicateEmail",
        //    Description = $"\"{email}\" başka bir kullanıcı tarafından kullanılmaktadır!"
        //};
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{userName}\" kullanıcı adı kullanılmaktadır." };
        public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{email}\" başka bir kullanıcı tarafından kullanılmaktadır." };
        public override IdentityError InvalidEmail(string email) => new IdentityError { Code = "InvalidEmail", Description = "Geçersiz email." };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = "PasswordRequiresDigit", Description = "Lütfen en az 1 sayı ekleyiniz!" };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = "PasswordRequiresLower", Description = "Lütfen en az 1 küçük harf ekleyiniz!" };
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = "PasswordRequiresUpper", Description = "Lütfen en az 1 büyük harf ekleyiniz!" };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = "PasswordRequiresNonAlphanumeric", Description = "Lütfen en az Alfanümerik  harf ekleyiniz!" };
    }
}
