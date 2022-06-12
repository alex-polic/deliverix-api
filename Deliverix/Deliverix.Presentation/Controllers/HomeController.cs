using Microsoft.AspNetCore.Mvc;

namespace Deliverix.Presentation.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Index()
    {
        return Json(new { Message = "Welcome to Deliverix API" });
    }
}