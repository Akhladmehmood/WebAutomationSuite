using NUnit.Framework;
using WebAutomationSuite.Pages;
using WebAutomationSuite.Utilities;
using System.Threading;

namespace WebAutomationSuite.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void SuccessfulLoginTest()
        {
            var config = ConfigReader.GetConfig();
            var login = new LoginPage(Driver!, config.BaseUrl);

            Test!.Info($"Navigating to {config.BaseUrl}");
            login.GoTo();

            Test.Info("Entering username");
            login.EnterUsername(config.Username);

            Test.Info("Entering password");
            login.EnterPassword(config.Password);

            Test.Info("Click login");
            login.ClickLogin();

            // Next line is a placeholder wait - replace with proper wait logic
            Thread.Sleep(2000);

            Assert.IsTrue(login.IsLoggedIn(), "User should be logged in - update locators/assertion as required");
            Test.Pass("Login test passed");
        }
    }
}

