using ITVTestTask.Data;
using ITVTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Devices.ToListAsync();
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> AddDevice(int id, [FromBody] Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevices), new { id = device.Id }, device);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id); if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("type/{type}")]
        public async Task<IActionResult> DeleteDevicesByType(string type)
        {
            var devices = await _context.Devices.Where(d => d.Type == type).ToListAsync();
            if (devices.Count == 0)
            {
                return NotFound();
            }

            _context.Devices.RemoveRange(devices);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
