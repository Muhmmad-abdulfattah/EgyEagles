using EgyEagles.MVC.Models;

namespace EgyEagles.MVC.Services
{
    public interface IUserAppService
    {
        Task<TokenResponseModel> LoginAsync(LoginViewModel model);
        Task<string> CreateUserAsync(CreateUserViewModel model, string token);
        Task<List<UserViewModel>> GetUsersByCompanyAsync(string companyId, string token);
    }
}
