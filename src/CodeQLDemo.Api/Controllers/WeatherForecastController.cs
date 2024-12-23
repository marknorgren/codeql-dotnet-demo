using Microsoft.AspNetCore.Mvc;
using CodeQLDemo.Common;
using CodeQLDemo.Security;

namespace CodeQLDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        // Security issue: Logging sensitive information
        _logger.LogInformation("Admin credentials: " + ConfigurationHelper.GetAdminCredentials());

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("user/{username}")]
    public IActionResult GetUserWeather(string username)
    {
        // Security issue: Using vulnerable SQL query
        SecurityUtils.ExecuteQuery(username);

        // Security issue: XSS vulnerability
        var formattedUsername = SecurityUtils.FormatUserInput(username);
        
        // Quality issue: Unused variable
        var unusedVariable = DateTime.Now;

        return Ok($"Weather for user: {formattedUsername}");
    }
} 