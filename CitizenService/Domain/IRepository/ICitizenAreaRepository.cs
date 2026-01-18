using Domain.Aggregate;
namespace Domain.IRepository
{
    public interface ICitizenAreaRepository : 
        IGenericRepository<CitizenArea>, 
        IRepositoryBase
    {
        Task<CitizenArea?> GetCitizenAreaByGPS(GPS gps);
        Task<CitizenArea?> GetCitizenAreaByRegionCode(string regionCode);
    }
}
