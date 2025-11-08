using System.IO;
using System.Text.Json;

namespace WebAutomationSuite.Utilities
{
    public class Config
    {
        public string Browser { get; set; } = "chrome";
        public string BaseUrl { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ReportPath { get; set; } = "Reports";
        public string ExcelDataPath { get; set; } = "TestData/TestData.xlsx";
    }

    public static class ConfigReader
    {
        private static readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        private static Config? _config;

        public static Config GetConfig()
        {
            if (_config != null) return _config;

            if (!File.Exists(_path))
                throw new FileNotFoundException($"Config file not found at {_path}");

            var json = File.ReadAllText(_path);
            _config = JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                      ?? new Config();
            return _config;
        }
    }
}

