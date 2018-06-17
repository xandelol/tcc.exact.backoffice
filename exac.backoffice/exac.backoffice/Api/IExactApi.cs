using System;
using System.Threading.Tasks;
using exac.backoffice.Models;
using exac.backoffice.Models.Payload;
using exac.backoffice.Models.Proxy;
using RestEase;

namespace exac.backoffice.Api
{
    public interface IExactApi
    {
        #region Login 

        [Post("auth/login")]
        Task<TokenProxy> Authenticate([Body] LoginPayload payload);

        [Get("user/me")]
        Task<MeProxy> GetUser([Header("Authorization")] string authorization);

        #endregion
        
        #region Question
        
        [Get("question")]
        Task<Response<QuestionViewModel>> GetQuestion([Header("Authorization")] string authorization, [Query]Guid id);
        
        [Post("question/create")]
        Task<Response<bool>> CreateQuestion([Header("Authorization")] string authorization, [Body]QuestionViewModel payload);
        
        [Put("question/update")]
        Task<Response<bool>> UpdateQuestion([Header("Authorization")] string authorization, [Body]QuestionViewModel payload);
        
        [Get("question/list")]
        Task<Response<DataTableProxy<QuestionViewModel>>> GetQuestions([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        #endregion
        
        
        #region Settings
        
        [Get("setting")]
        Task<Response<SettingViewModel>> GetSetting([Header("Authorization")] string authorization, [Query]int id);
        
        [Post("setting/create")]
        Task<Response<bool>> CreateSetting([Header("Authorization")] string authorization, [Body]SettingViewModel payload);
        
        [Put("setting/update")]
        Task<Response<bool>> UpdateSetting([Header("Authorization")] string authorization, [Body]SettingViewModel payload);
        
        [Get("setting/list")]
        Task<Response<DataTableProxy<SettingViewModel>>> GetSettings([Header("Authorization")] string authorization, [Body]DataTablePayload payload);
        
        #endregion
    }
}