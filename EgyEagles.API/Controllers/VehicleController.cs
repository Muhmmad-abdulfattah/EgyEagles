using EgyEagles.BLL.Interfaces;
using EgyEagles.BLL.Sevices;
using EgyEagles.Shared.DTOs.Companies;
using EgyEagles.Shared.DTOs.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgyEagles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;

        public VehicleController(IVehicleService vehicleService,IUserService userService)
        {
            _vehicleService = vehicleService;
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto dto)
        {
            var id = await _vehicleService.CreateVehicleAsync(dto);
            return Ok(new { VehicleId = id });
        }

        [HttpPut("update-location")]
        [Authorize(Roles = "SuperAdmin,CompanyAdmin")]

        public async Task<IActionResult> UpdateLocation([FromBody] UpdateVehicleLocationDto dto)
        {
            try
            {
                await _vehicleService.UpdateLocationAsync(dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("by-company/{companyId}")]
        [Authorize(Roles = "SuperAdmin,CompanyAdmin")]
        public async Task<IActionResult> GetByCompany(string companyId)
        {
            if (User.IsInRole("CompanyAdmin"))
            {
                var currentCompanyId = User.FindFirst("CompanyId")?.Value;
                if (currentCompanyId != companyId)
                    return Forbid();

                return Ok(await _vehicleService.GetVehiclesByCompanyIdAsync(companyId));
            }

            if (string.IsNullOrEmpty(companyId))
            {
                var allVehicles = await _vehicleService.GetAllAsync(); 
                return Ok(allVehicles);
            }

            return Ok(await _vehicleService.GetVehiclesByCompanyIdAsync(companyId));
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "CompanyAdmin")]
        public async Task<IActionResult> Update(string id, UpdateVehicleDto dto)
        {
            var updated = await _vehicleService.UpdateVehicleAsync(id, dto);
            if (!updated)
                return NotFound();

            return Ok("Updated");
        }

    }
}
