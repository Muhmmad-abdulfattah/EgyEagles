using EgyEagles.BLL.Interfaces;
using EgyEagles.BLL.Sevices;
using EgyEagles.Shared.DTOs.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyEagles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyServices _companyServices;

        public CompanyController(ICompanyServices companyServices)
        {
            _companyServices = companyServices;
        }

        [HttpPost("create")]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> CreateCompany(CreateCompanyDto Dto)
        {
            var companyId = await _companyServices.CreateCompanyAsync(Dto.Name);
            return Ok(new { companyId }); 

        }

        [HttpGet("names")]
        //[Authorize(Roles = "SuperAdmin,CompanyAdmin")]

        public async Task<IActionResult> GetCompanyNames()
        {
            var names = await _companyServices.GetAllCompanyNamesAsync();
            return Ok(names);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,CompanyAdmin")]
        public async Task<IActionResult> GetById(string id)
        {
            var company = await _companyServices.GetCompanyByIdAsync(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }
    }
        
}
