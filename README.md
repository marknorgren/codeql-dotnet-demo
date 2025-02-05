# CodeQL .NET Demo

This repository demonstrates the setup and usage of CodeQL for analyzing C# code for security vulnerabilities.

## Environment Setup

- OS: macOS 24.1.0
- CodeQL CLI
- .NET SDK 8.0
- Visual Studio Code with CodeQL extension (recommended)
- just (command runner)

## Project Structure

The solution consists of three projects:
- `CodeQLDemo.Api`: Main ASP.NET Core Web API project
- `CodeQLDemo.Common`: Common library containing shared functionality
- `CodeQLDemo.Security`: Security-related functionality

## Running the Analysis

### Local Analysis

1. Install the required tools:
   ```bash
   brew install codeql just
   ```

2. List available commands:
   ```bash
   just
   ```

3. Run the complete analysis:
   ```bash
   just codeql-all
   ```

   Or run individual steps:
   ```bash
   just clean          # Clean up previous runs
   just codeql-init    # Create CodeQL database
   just codeql-analyze # Run security analysis
   ```

### GitHub Actions Integration

The repository includes GitHub Actions workflow for automated CodeQL analysis:

- Triggers:
  - On push to `main` branch
  - On pull requests to `main` branch
  - Weekly schedule (Sunday 1:30 AM UTC)

- Features:
  - Automated .NET setup
  - CodeQL initialization and analysis
  - Results viewable in GitHub Security tab
  - Configurable via `codeql-config.yml`

To enable:
1. Push the repository to GitHub
2. Enable GitHub Actions in repository settings
3. Enable GitHub Code Scanning alerts

## Detected Security Issues

The CodeQL analysis has identified several security vulnerabilities:

1. **SQL Injection** (`src/CodeQLDemo.Security/Class1.cs`)
   - Issue: User input is directly concatenated into SQL queries
   - Fix: Use parameterized queries or an ORM like Entity Framework
   ```csharp
   // Bad
   string query = "SELECT * FROM Users WHERE Username = '" + userInput + "'";
   
   // Good
   string query = "SELECT * FROM Users WHERE Username = @username";
   command.Parameters.AddWithValue("@username", userInput);
   ```

2. **Cleartext Storage of Sensitive Information** (`src/CodeQLDemo.Api/Controllers/WeatherForecastController.cs`)
   - Issue: Admin credentials are logged in cleartext
   - Fix: Never log sensitive information, use secure logging practices
   ```csharp
   // Bad
   _logger.LogInformation("Admin credentials: " + ConfigurationHelper.GetAdminCredentials());
   
   // Good
   _logger.LogInformation("Admin authentication attempt");
   ```

3. **Insecure SQL Connection** (`src/CodeQLDemo.Security/Class1.cs`)
   - Issue: SQL connection string doesn't specify encryption
   - Fix: Add `Encrypt=True` to the connection string
   ```csharp
   // Bad
   "Server=myserver;Database=mydb;User Id=sa;Password=123456;"
   
   // Good
   "Server=myserver;Database=mydb;User Id=sa;Password=123456;Encrypt=True;"
   ```

4. **Hardcoded Credentials** (`src/CodeQLDemo.Security/Class1.cs`)
   - Issue: Database credentials are hardcoded in the connection string
   - Fix: Use configuration management and secure secrets storage
   ```csharp
   // Bad
   var connectionString = "Server=myserver;Database=mydb;User Id=sa;Password=123456;";
   
   // Good
   var connectionString = configuration.GetConnectionString("DefaultConnection");
   // Store in Azure Key Vault, AWS Secrets Manager, or similar service
   ```

## Best Practices

1. Always use parameterized queries for database operations
2. Never store or log sensitive information in cleartext
3. Use secure connection strings with encryption enabled
4. Store credentials in secure configuration management systems
5. Regularly run CodeQL analysis to catch security issues early

## Next Steps

1. Fix the identified security vulnerabilities
2. Add more security test cases
3. Integrate CodeQL analysis into the CI/CD pipeline
4. Set up automated security scanning with GitHub Code Scanning

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

MIT 