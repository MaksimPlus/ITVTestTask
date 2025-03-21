namespace ITVTestTask.Interfaces
{
    public interface IRepository<T> where T : BaseClass
    {
        List<T> GetAll();
        bool AddNew(T entity);
        T GetById(int id);
        void DeleteById(int id);
    }
}
