using StajApp.AppDbContext;
using StajApp.Models;

namespace StajApp.Repository.CoordinateRepository
{
    public class CoordinateRepository : GenericRepository<Coordinate>, ICoordinateRepository
    {
        public CoordinateRepository(ApplicationDbContext context):base(context)
        {
        
        }
    }
}
