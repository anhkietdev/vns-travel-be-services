using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices([FromQuery] ServiceFilterDto filterDto)
        {
            try
            {
                var services = await _serviceManager.GetServices(filterDto);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            try
            {
                var service = await _serviceManager.GetServiceById(id);
                return Ok(service);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null)
                return BadRequest("Service data is null");

            try
            {
                await _serviceManager.CreateService(serviceDto);
                return StatusCode(201, "Service created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null || id != serviceDto.ServiceId)
                return BadRequest("Service data is invalid");

            try
            {
                await _serviceManager.UpdateService(serviceDto);
                return Ok("Service updated successfully");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            try
            {
                var result = await _serviceManager.DeleteService(id);
                return result ? Ok("Service deleted successfully") : NotFound("Service not found");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
