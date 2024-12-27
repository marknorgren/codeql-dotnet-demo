using Microsoft.AspNetCore.Mvc;
using CodeQLDemo.Security;
using CodeQLDemo.Common;

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
    public IActionResult GetUser(string username)
    {
        // Security issue: SQL Injection
        SecurityUtils.ExecuteQuery(username);

        // Security issue: XSS
        var formattedUsername = SecurityUtils.FormatUserInput(username);

        // Security issue: Empty catch block
        SecurityUtils.ValidateUser(username);

        return Ok(new { username = formattedUsername });
    }
} 