using MyWebsite.Application.DTOs;

namespace MyWebsite.Application.Abstracts
{
    public interface IAuthenticationService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool rememberMe);
    }
}
