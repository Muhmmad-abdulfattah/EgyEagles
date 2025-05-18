using EgyEagles.MVC.Models;

public interface ICompanyAppService
{
    Task<List<CompanyViewModel>> GetAllCompanyNamesAsync(string token);
    Task<string> CreateCompanyAsync(CreateCompanyViewModel model, string token);
    Task<CompanyViewModel> GetCompanyByIdAsync(string id, string token);
}