using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Pages.PageElements
{
    public class LoginPageElements
    {
        public string UsernameTextfield => "i016";
        public string PasswordField => "i0118";
        public string LoginHeader => "loginHeader";
        public string Next => "idSIButton9";
        public string SignIn => "idSIButton9";
        public string SignInTitle => "Sign in to your account";
        public string SignOutTitle => "Sign out";
        public string ReSignInButton(string username) => $"//div[contains(text(), '{username}')]";
        public string CurrentPassword => "currentPassword";
        public string NewPassword => "newPassword";
        public string ConfirmNewPassword => "confirmNewPassword";
        public string SignInButtonAfterPasswordChange => "idSIButton9";
    }
}
