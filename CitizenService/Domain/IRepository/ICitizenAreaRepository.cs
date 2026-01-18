using Domain.Aggregate;
namespace Domain.IRepository
{
    public interface ICitizenAreaRepository : 
        IGenericRepository<CitizenArea>, 
        IRepositoryBase
    {
         Task<CitizenArea?> GetCitizenAreaByGPS(double latitude, double longitude);
    }
}
