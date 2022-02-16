using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Drivers;
using UI.Pages.PageElements;
namespace UISelenium.Pages
{
    public class LoginPage : LoginPageElements
    {
        IDriver driver;
        public LoginPage(IDriver _driver)
        {
            driver = _driver;
        }

        public static By UsernameTextfield = By.Id("i0116");
        public static By PasswordField = By.Id("i0118");
        public static By LoginHeader = By.Id("loginHeader");
        public static By Next = By.Id("idSIButton9");
        public static By SignIn = By.Id("idSIButton9");
        public static string SignInTitle = "Sign in to your account";
        public static string SignOutTitle = "Sign out";
        public static By ReSignInButton(string username) => By.XPath($"//div[contains(text(), '{username}')]");
        public static By CurrentPassword = By.Id("currentPassword");
        public static By NewPassword = By.Id("newPassword");
        public static By ConfirmNewPassword = By.Id("confirmNewPassword");
        public static By SignInButtonAfterPasswordChange = By.Id("idSIButton9");


        public void Login(string username, string password)
        {
            
            driver.Type(UsernameTextfield, username);
            driver.Click(Next);
            driver.Type(PasswordField, password);
        }
    }
}
