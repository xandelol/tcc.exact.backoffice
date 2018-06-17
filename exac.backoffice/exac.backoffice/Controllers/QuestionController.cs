using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Liga.Backoffice.Lanxess.Api;
using Liga.Backoffice.Lanxess.Controllers.Base;
using Liga.Backoffice.Lanxess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestEase;

namespace Liga.Backoffice.Lanxess.Controllers
{
    [Authorize]
    public class QuestionController : BaseController
    {
        private readonly ILanxessApi _api;

        public QuestionController(ILanxessApi api)
        {
            _api = api;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? id)
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
                using (var proxy = model.Id == 0 ? await _api.CreateQuestion(user.Token, model) : await _api.UpdateQuestion(user.Token, model))
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