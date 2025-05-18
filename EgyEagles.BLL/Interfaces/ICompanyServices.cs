using EgyEagles.Domain.Enitites;
using EgyEagles.Shared.DTOs.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.BLL.Interfaces
{
    public interface ICompanyServices
    {
        Task<string> CreateCompanyAsync(string name);
        Task<List<CompanyDto>> GetAllCompanyNamesAsync();

        Task<Company> GetCompanyByIdAsync(string id);
    }
}
