using ITVTestTask.Data;
using ITVTestTask.Interfaces;
using ITVTestTask.Models;

namespace ITVTestTask.Implementations
{
    public class UoW : IUoW, IDisposable
    {
        private DeviceContext _context;
        public UoW(DeviceContext context)
        {
            _context = context;
            DeviceRepository = new DeviceRepository(_context);
        }
        public DeviceRepository DeviceRepository { get; }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
