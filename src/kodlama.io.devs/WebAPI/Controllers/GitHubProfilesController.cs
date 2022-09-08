using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class GitHubProfilesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}