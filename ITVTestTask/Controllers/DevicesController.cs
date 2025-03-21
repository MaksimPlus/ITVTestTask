using ITVTestTask.Data;
using ITVTestTask.Implementations;
using ITVTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceContext _context;

        public DevicesController(DeviceContext context)
        {
            _context = context;
        }        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            using (var uow = new UoW(_context))
            {
                return uow.DeviceRepository.GetAll();
            }
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> AddDevice(int id, [FromBody] Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }
            using (var uow = new UoW(_context))
            {
                uow.DeviceRepository.AddNew(device);
                uow.SaveChanges();
            }

            return CreatedAtAction(nameof(GetDevices), new { id = device.Id }, device);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceByType(int id)
        {
            using(var uow = new UoW(_context))
            {
                uow.DeviceRepository.DeleteById(id);
                uow.SaveChanges();
            }
            return NoContent();
        }

        [HttpDelete("type/{type}")]
        public async Task<IActionResult> DeleteDevicesByType(string type)
        {
            using (var uow = new UoW(_context))
            {
                uow.DeviceRepository.DeleteByType(type);
                uow.SaveChanges();
            }
            return NoContent();
        }
    }
}
