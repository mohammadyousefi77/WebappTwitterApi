
namespace WebappTwitterApi.Data
{
    public class UnitOfWork : IUintOfWork
    {

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CommitAsync()
        {
            return await  _context.SaveChangesAsync()>0;
        }

        public void Delete<Tentity>(Tentity entity)
            where Tentity : class
        {
             _context.Set<Tentity>().Remove(entity);
        }

        public IQueryable<Tentity> Get<Tentity>() where Tentity : class
        {
            return _context.Set<Tentity>();
        }

        public async Task<Tentity> GetByIdAsync<Tentity>(object id)
            where Tentity:class
        {
            return await _context.Set<Tentity>().FindAsync(id) ?? throw new EntryPointNotFoundException();
        }

        public async Task<Tentity> InsertAsync<Tentity>(Tentity entity) where Tentity : class
        {
           var result = await _context.Set<Tentity>().AddAsync(entity);
            return  result.Entity;
        }
    }
}
