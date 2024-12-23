using System;

namespace CodeQLDemo.Common
{
    public class ConfigurationHelper
    {
        // Security issue: Hardcoded credentials
        private const string AdminUsername = "admin";
        private const string AdminPassword = "SuperSecretPassword123!";

        // Quality issue: Unused private field
        private readonly string unusedConnectionString;

        public static string GetAdminCredentials()
        {
            // Security issue: Returning sensitive data as plain text
            return $"{AdminUsername}:{AdminPassword}";
        }

        // Quality issue: Method that doesn't use its parameter
        public void ProcessConfiguration(string config)
        {
            // Security issue: Writing sensitive information to console
            Console.WriteLine($"Current admin password: {AdminPassword}");
        }
    }
}
