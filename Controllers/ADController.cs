using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;

using Dksh.ePOD.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dksh.ePOD.Controllers
{
    [Authorize(AuthenticationSchemes = AzureADDefaults.AuthenticationScheme)]
    public class ADController : Controller
    {        
        public IActionResult Index()
        {
            bool a = User.Identity.IsAuthenticated;

            return View();
        }
        
        public IActionResult Test1()
        {
            return View("Test1");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
