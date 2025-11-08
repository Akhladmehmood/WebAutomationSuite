using System;
using System.IO;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using WebAutomationSuite.Drivers;
using WebAutomationSuite.Utilities;

namespace WebAutomationSuite.Tests
{
    public class TestBase
    {
        protected IWebDriver? Driver;
        protected static ExtentReports? Extent;
        protected ExtentTest? Test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Extent = ExtentManager.GetExtent();
        }

        [SetUp]
        public void SetUp()
        {
            var config = ConfigReader.GetConfig();
            Driver = WebDriverFactory.Create(config.Browser);
            Test = Extent?.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            if (Test != null)
            {
                switch (status)
                {
                    case NUnit.Framework.Interfaces.TestStatus.Passed:
                        Test.Pass("✅ Test passed successfully");
                        break;

                    case NUnit.Framework.Interfaces.TestStatus.Failed:
                        Test.Fail($"❌ Test failed: {message}");
                        CaptureScreenshot();
                        break;

                    default:
                        Test.Skip("⚠️ Test skipped or inconclusive");
                        break;
                }
            }

            try
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            catch
            {
                // Ignored — ensure teardown doesn’t block report generation
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Extent?.Flush();
        }

        private void CaptureScreenshot()
        {
            try
            {
                if (Driver == null) return;

                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var config = ConfigReader.GetConfig();

                var reportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.ReportPath, "Screenshots");
                Directory.CreateDirectory(reportDir);

                var fileName = $"screenshot_{TestContext.CurrentContext.Test.ID}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var fullPath = Path.Combine(reportDir, fileName);

                // ✅ Selenium 4.23: only path needed — no ScreenshotImageFormat
                screenshot.SaveAsFile(fullPath);

                Test?.AddScreenCaptureFromPath(fullPath);
            }
            catch (Exception ex)
            {
                Test?.Warning($"⚠️ Failed to capture screenshot: {ex.Message}");
            }
        }
    }
}
