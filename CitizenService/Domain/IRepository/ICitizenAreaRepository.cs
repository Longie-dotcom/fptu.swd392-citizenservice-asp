using Domain.Aggregate;
using Domain.ValueObject;
namespace Domain.IRepository
{
    public interface ICitizenAreaRepository : 
        IGenericRepository<CitizenArea>, 
        IRepositoryBase
    {
         Task<CitizenArea?> GetCitizenAreaByGPS(GPS gps);
    }
}
