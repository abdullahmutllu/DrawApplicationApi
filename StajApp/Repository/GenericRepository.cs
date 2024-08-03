using Microsoft.EntityFrameworkCore;
using StajApp.AppDbContext;
using StajApp.Models;

namespace StajApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
          return  _dbSet.AsQueryable();
        }

        public void Update(int id, T entity)
        {
            if (typeof(T) == typeof(Coordinate))
            {
                var data = _dbSet.Find(id) as Coordinate;
                if (data == null)
                {
                    throw new Exception("Entity not found");
                }

                var coordinateEntity = entity as Coordinate;
                if (coordinateEntity != null)
                {
                    data.Name = coordinateEntity.Name;
                    data.X = coordinateEntity.X;
                    data.Y = coordinateEntity.Y;

                    _context.Coordinates.Update(data);
                }
                else
                {
                    throw new InvalidOperationException("Invalid entity type");
                }
            }
            else
            {
                throw new InvalidOperationException("Update method supports only Coordinate type");
            }
        }


        public void Delete(int id)
        {
          var data =  _dbSet.Find(id) as Coordinate;
            if (data == null)
            {
                throw new Exception("data is not found");
            }
            _context.Coordinates.Remove(data);
        }

      
    }
}
