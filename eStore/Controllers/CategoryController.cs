using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers;

[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit(int id)
    {
        return View();
    }
}
