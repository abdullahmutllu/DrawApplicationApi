using StajApp.Models;
using StajApp.Repository;
using StajApp.Repository.CoordinateRepository;
using StajApp.Services;
using StajApp.UnitOfWorks;
using System.Linq.Expressions;

namespace StajApp.Services.Map
{
    public class MapControlService : Service<Coordinate>, IMapControlService
    {
        private readonly ICoordinateRepository _coordinateRepository;
        public MapControlService(IGenericRepository<Coordinate> repository, ICoordinateRepository coordinateRepository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
            _coordinateRepository = coordinateRepository;
        }

        public Expression<Func<Coordinate, bool>> CoordinateFilter()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Coordinate> GetAllCoordinates(int page, int pageSize)
        {
            //int pageSize = 5;
            IQueryable<Coordinate> coordinateList = _coordinateRepository.GetAll().Skip((page - 1) * pageSize).Take(pageSize);
            return coordinateList;
        }
    }
}
