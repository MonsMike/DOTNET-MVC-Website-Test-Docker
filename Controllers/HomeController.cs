using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MVCWebsiteTest_Docker.Helpers;
using MVCWebsiteTest_Docker.Models;

namespace MVCWebsiteTest_Docker.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Weather()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var weatherApiBase = Environment.GetEnvironmentVariable("API_URL") ?? config["API_URL"];
        if (string.IsNullOrEmpty(weatherApiBase))
            return NotFound("Missing API URL");
        
        var results = Helper.GetApi(weatherApiBase, "WeatherForecast");
        var weatherList = JsonSerializer.Deserialize<List<WeatherForecast>>(results, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        
        return View(weatherList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}