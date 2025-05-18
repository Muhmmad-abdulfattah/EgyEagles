using EgyEagles.BLL.Interfaces;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Interfaces;
using EgyEagles.Shared.DTOs.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.BLL.Sevices
{
    public class CompanyService : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<string> CreateCompanyAsync(string name)
        {
            var company = new Company { Name = name };
            
            await _companyRepository.AddAsync(company);
            return company.Id;
        }

        public async Task<List<CompanyDto>> GetAllCompanyNamesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<Company> GetCompanyByIdAsync(string id)
        {
            return await _companyRepository.GetByIdAsync(id);
        }
    }
}
