using DocumentFormat.OpenXml.Bibliography;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAutomationSuite.Pages;

namespace WebAutomationSuite.Tests
{
    internal class LoginWithVisibleCreds : TestBase
    {
        [Test]
        [Category("Regression")]
        public void Login_UsingVisibleCredentials()
        {
            // Create login page object using the Driver from BaseTest
            var loginPage = new LoginPage(Driver, "https://www.saucedemo.com/");

            // Navigate to the page
            loginPage.GoTo();

            // Read visible credentials shown on the screen
            string rawUserBlock = loginPage.GetStandardUser();
            string rawPasswordBlock = loginPage.GetVisiblePassword();

            // Extract actual username and password
            string username = rawUserBlock
                                .Split('\n')
                                .FirstOrDefault(line => line.Contains("standard_user"))
                                .Trim();

            string password = rawPasswordBlock
                                .Split('\n')
                                .FirstOrDefault(line => line.Contains("secret_sauce"))
                                .Trim();

            // Perform login
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLogin();

            // Assert login
            Assert.IsTrue(loginPage.IsLoggedIn(), "Login failed using visible credentials.");
        }
    }
}
