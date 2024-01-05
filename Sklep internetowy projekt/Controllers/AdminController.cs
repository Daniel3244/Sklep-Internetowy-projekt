using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    [Authorize(Roles = "Admin")]
    public IActionResult AdminDashboard()
    {
        // Ta akcja jest dostępna tylko dla użytkowników z rolą "Admin"
        return View();
    }
}
