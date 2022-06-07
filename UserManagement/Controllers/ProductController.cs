using Microsoft.AspNetCore.Mvc;

namespace InsuranceUserManagement.Controllers
{
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
}