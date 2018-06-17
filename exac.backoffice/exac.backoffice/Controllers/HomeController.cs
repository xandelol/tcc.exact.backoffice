using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Liga.Backoffice.Lanxess.Api;
using Liga.Backoffice.Lanxess.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Liga.Backoffice.Lanxess.Models;
using Microsoft.AspNetCore.Authorization;

namespace Liga.Backoffice.Lanxess.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        
        private readonly ILanxessApi _api;

        public HomeController(ILanxessApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var user = GetUserInfo();
            
            using (var proxy = await _api.GetDashInfo(user.Token))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return View(proxy.GetContent());
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("~/home/index");
                    default:
                        return Redirect("~/home/error");
                }
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}