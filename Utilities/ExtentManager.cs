using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace WebAutomationSuite.Utilities
{
    public class ExtentManager
    {
        private static ExtentReports? _extent;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                Directory.CreateDirectory(reportPath);

                string reportFile = Path.Combine(reportPath, $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");
                Console.WriteLine("Extent Report Path: " + reportFile);
                // For older versions, use ExtentHtmlReporter
                var htmlReporter = new ExtentHtmlReporter(reportFile);

                htmlReporter.Config.DocumentTitle = "Automation Report";
                htmlReporter.Config.ReportName = "WebAutomationSuite Execution Report";
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
            }

            return _extent;
        }
    }
}
