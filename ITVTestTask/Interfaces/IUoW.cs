using ITVTestTask.Implementations;

namespace ITVTestTask.Interfaces
{
    public interface IUoW
    {
        DeviceRepository DeviceRepository { get; }
        void SaveChanges();
    }
}
