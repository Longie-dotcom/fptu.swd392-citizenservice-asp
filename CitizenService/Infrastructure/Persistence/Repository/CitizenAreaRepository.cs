using Domain.Aggregate;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class CitizenAreaRepository : 
        GenericRepository<CitizenArea>, 
        ICitizenAreaRepository
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        public CitizenAreaRepository(CitizenDBContext context) : base(context) { }

        #region Methods
        public async Task<CitizenArea?> GetCitizenAreaByGPS(double latitude, double longitude)
        {
            IQueryable<CitizenArea> query = context.CitizenAreas
               .AsNoTracking()
               .AsQueryable();

            return await query
                .Where(
                l => l.MinLat <= latitude && l.MaxLat >= longitude
                && l.MinLng <= latitude && l.MaxLng >= longitude)
                .FirstOrDefaultAsync();
        }

        public async Task<CitizenArea?> GetCitizenAreaByRegionCode(string regionCode)
        {
            IQueryable<CitizenArea> query = context.CitizenAreas
               .AsNoTracking()
               .AsQueryable();
            return await query
                .Where(l => l.RegionCode == regionCode)
                .FirstOrDefaultAsync();
        }
        #endregion
    }
}
