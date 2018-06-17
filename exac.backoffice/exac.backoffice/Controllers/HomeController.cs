using System.Net;
using System.Threading.Tasks;
using exac.backoffice.Api;
using exac.backoffice.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exac.backoffice.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        
        private readonly IExactApi _api;

        public HomeController(IExactApi api)
        {
            _api = api;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}