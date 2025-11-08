using OpenQA.Selenium;

namespace WebAutomationSuite.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url;

        // Locators - update to match your app
        private By UsernameInput => By.Id("user-name");
        private By PasswordInput => By.Id("password");
        private By LoginButton => By.Id("login-button");
        private By LoggedInIndicator => By.CssSelector(".app_logo"); // example

        public LoginPage(IWebDriver driver, string baseUrl)
        {
            _driver = driver;
            _url = baseUrl;
        }

        public void GoTo() => _driver.Navigate().GoToUrl(_url);

        public void EnterUsername(string username) => _driver.FindElement(UsernameInput).ClearThenSendKeys(username);

        public void EnterPassword(string password) => _driver.FindElement(PasswordInput).ClearThenSendKeys(password);

        public void ClickLogin() => _driver.FindElement(LoginButton).Click();

        public bool IsLoggedIn()
        {
            try
            {
                return _driver.FindElements(LoggedInIndicator).Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }

    internal static class WebElementExtensions
    {
        public static void ClearThenSendKeys(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
