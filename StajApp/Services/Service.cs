using StajApp.Repository;
using StajApp.UnitOfWorks;

namespace StajApp.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWorks _unitOfWorks;
        public Service(IGenericRepository<T> genericRepository, IUnitOfWorks unitOfWorks)
        {
            _genericRepository = genericRepository;
            _unitOfWorks = unitOfWorks;

        }
        public void Add(T entity)
        {
            _genericRepository.Add(entity);
            _unitOfWorks.Commit();

        }

        public void Delete(int id)
        {
            _genericRepository.Delete(id);
            _unitOfWorks.Commit();
        }

        public T Get(int id)
        {
            return _genericRepository.Get(id);
        }

        public IQueryable<T> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public void Update(int id, T entity)
        {
            _genericRepository.Update(id, entity);
            _unitOfWorks.Commit();
        }
    }
}
