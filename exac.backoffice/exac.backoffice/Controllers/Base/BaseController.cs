using System;
using System.Linq;
using System.Threading.Tasks;
using Liga.Backoffice.Lanxess.Models;
using Liga.Backoffice.Lanxess.Models.Payload;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Liga.Backoffice.Lanxess.Controllers.Base
{
    public class BaseController : Controller
    {
        
        public UserInfoViewModel GetUserInfo()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var loggedInUser = HttpContext.User;
                return JsonConvert.DeserializeObject<UserInfoViewModel>(loggedInUser.Identity.Name);
            }
            return null;
        }
        
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userInfo = GetUserInfo();
            if(userInfo != null)
                ViewData["UserInfo"] = userInfo;
        }
        
        public DataTablePayload GetDataTablePayload()
        {
            var payload = new DataTablePayload();
            
            payload.Draw = HttpContext.Request.Form["draw"].FirstOrDefault();  
  
            // Skip number of Rows count  
            payload.Skip  = Request.Form["start"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["start"].FirstOrDefault()) : 0;  
  
            // Paging Length 10,20  
            payload.PageSize = Request.Form["length"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["length"].FirstOrDefault()) : 0;  
  
            // Sort Column Name  
            payload.SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();  
  
            // Sort Column Direction (asc, desc)  
            payload.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();  
  
            // Search Value from (Search box)  
            payload.SearchValue = Request.Form["search[value]"].FirstOrDefault();

            return payload;

        }
        
        public DataTablePayload<T> GetDataTablePayload<T>()
        {
            var payload = new DataTablePayload<T>();
            
            payload.Draw = HttpContext.Request.Form["draw"].FirstOrDefault();  
  
            // Skip number of Rows count  
            payload.Skip  = Request.Form["start"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["start"].FirstOrDefault()) : 0;  
  
            // Paging Length 10,20  
            payload.PageSize = Request.Form["length"].FirstOrDefault() != null ? Convert.ToInt32(Request.Form["length"].FirstOrDefault()) : 0;  
  
            // Sort Column Name  
            payload.SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();  
  
            // Sort Column Direction (asc, desc)  
            payload.SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();  
  
            // Search Value from (Search box)  
            payload.SearchValue = Request.Form["search[value]"].FirstOrDefault();

            return payload;

        }
        
        
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
         /// <summary>
        ///     Run <paramref name="predicate" /> under default statement.
        /// </summary>
        /// <param name="predicate">Function to be ran.</param>
        /// <returns><paramref name="predicate" /> return or default return in case of an error has been thrown.</returns>
        protected async Task<IActionResult> RunDefaultAsync(Func<Task<IActionResult>> predicate)
        {
            try
            {
                return await predicate();
            }
            catch (System.Exception exception)
            {
                return Redirect("~/home/error");
            }
        }

        /// <summary>
        ///     Run <paramref name="predicate" /> under default statement.
        /// </summary>
        /// <param name="predicate">Function to be ran.</param>
        /// <returns><paramref name="predicate" /> return or default return in case of an error has been thrown.</returns>
        protected IActionResult RunDefault(Func<IActionResult> predicate)
        {
            try
            {
                return predicate();
            }
            
            catch (System.Exception exception)
            {
                return Redirect("~/home/error");
            }
        }
    }
}