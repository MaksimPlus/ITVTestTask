using ITVTestTask.Data;
using ITVTestTask.Interfaces;
using ITVTestTask.Models;

namespace ITVTestTask.Implementations
{
    public class DeviceRepository : IRepository<Device>
    {
        private DeviceContext _context;
        public DeviceRepository(DeviceContext deviceContext)
        {
            _context = deviceContext;
        }
        public bool AddNew(Device device)
        {
            if (device != null)
            {
                if (GetById(device.Id) != null)
                {
                    return false;
                }
                else
                {
                    _context.Devices.Add(device);
                    return true;
                }
            }
            return false;
        }
        public void DeleteById(int id)
        {
            var device = GetById(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
            }
        }
        public Device GetById(int id)
        {
            return _context.Devices.FirstOrDefault(x => x.Id == id);
        }
        public void DeleteByType(string type)
        {
            var devices = _context.Devices.Where(x => x.Type == type);
            foreach (var device in devices)
            {
                _context.Devices.Remove(device);
            }
        }
        public List<Device> GetAll()
        {
            return _context.Devices.ToList();
        }
    }
}
