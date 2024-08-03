using StajApp.Models;
using StajApp.Services;
using System.Linq.Expressions;

namespace StajApp.Services.Map
{
    public interface IMapControlService : IService<Coordinate>
    {
        Expression<Func<Coordinate, bool>> CoordinateFilter();
        IQueryable<Coordinate> GetAllCoordinates(int page, int pageSize);
    }
}
