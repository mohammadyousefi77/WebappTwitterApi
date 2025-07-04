namespace WebappTwitterApi.Data
{
    public interface IUintOfWork
    {
        IQueryable<Tentity>Get<Tentity>() where Tentity:class;
        Task<Tentity> GetByIdAsync<Tentity>(Object id) where Tentity : class;
       Task<Tentity>InsertAsync<Tentity>(Tentity entity) where Tentity : class;
        void Delete<Tentity>(Tentity entity) where Tentity : class;
        Task<bool> CommitAsync();
    }
}
