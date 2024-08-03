namespace StajApp.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        IQueryable<T> GetAll();
        T Get(int id);
        void Update(int id,T entity);
        void Delete(int id);
    }
}
