using System;
using System.Net;
using System.Threading.Tasks;
using exac.backoffice.Api;
using exac.backoffice.Controllers.Base;
using exac.backoffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exac.backoffice.Controllers
{
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly IExactApi _api;

        public QuestionController(IExactApi api)
        {
            _api = api;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(Guid? id)
        {
            var user = GetUserInfo();

            if (!id.HasValue) return View(QuestionViewModel.New());
            
            using (var proxy = await _api.GetQuestion( user.Token, id.Value))
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

        [HttpPost]
        public async Task<IActionResult> Create(QuestionViewModel model)
        { 
            var user = GetUserInfo();

            if (ModelState.IsValid)
            {
                using (var proxy = model.Id == Guid.Empty ? await _api.CreateQuestion(user.Token, model) 
                    : await _api.UpdateQuestion(user.Token, model))
                {
                    switch (proxy.ResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return Redirect("Index");
                        case HttpStatusCode.Unauthorized:
                            await Logout();
                            return Redirect("~/home/index");
                        default:
                            return Redirect("~/home/error");
                    }
                }
            }
            
            ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor, contate os responsáveis.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            var user = GetUserInfo();

            var payload = GetDataTablePayload();

            using (var proxy = await _api.GetQuestions(user.Token, payload))
            {
                switch (proxy.ResponseMessage.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var list = proxy.GetContent();
                        return Json(list);
                    case HttpStatusCode.Unauthorized:
                        await Logout();
                        return Redirect("~/home/index");
                    default:
                        return Redirect("~/home/error");
                }
            }
        }
    }
}