using StajApp.AppDbContext;

namespace StajApp.UnitOfWorks
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWorks(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
