using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Liga.Backoffice.Lanstringxess.Models.Payload;
using Liga.Backoffice.Lanxess.Api;
using Liga.Backoffice.Lanxess.Controllers.Base;
using Liga.Backoffice.Lanxess.Models;
using Liga.Backoffice.Lanxess.Models.Payload;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Liga.Backoffice.Lanxess.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly ILanxessApi _api;

        public AccountController(ILanxessApi api)
        {
            _api = api;
        }


        // GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginViewModel loginModel)
        {
            try
            {
                var token = await _api.Authenticate(new LoginPayload
                {
                    Password = loginModel.Password,
                    Username = loginModel.Username,
                    Type = ClientAccessType.Backoffice
                });

                var userProxy = await _api.GetUser(token.Token);

                var user = new UserInfoViewModel()
                {
                    Name = userProxy.Name,
                    Email = userProxy.Email,
                    Token = token.Token
                };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(user)),
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                var principal = new ClaimsPrincipal(userIdentity);
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    principal);
                
                return Ok(new {accessGranted = true});

            }
            catch (RestEase.ApiException exception)
            {
                var result = JsonConvert.DeserializeObject<AuthFailedViewModel>(exception.Content);
                result.AccessGranted = false;

                return Ok(result);
            }
            catch (Exception e)
            {
                var result = new AuthFailedViewModel();
                result.Message = "Usuário e/u senha inválido(s)!";
                result.AccessGranted = false;

                return Ok(result);
            }
          
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            return Ok(new
            {
                Ok = true
            });
        }
    }
    
    
}