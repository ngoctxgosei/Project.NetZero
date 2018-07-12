using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace WS.Web.Controllers
{
    public class HomeController : WSControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
