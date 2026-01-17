using Domain.Aggregate;
using Domain.IRepository;
using Domain.ValueObject;
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
        public async Task<CitizenArea?> GetCitizenAreaByGPS(GPS gps)
        {
            IQueryable<CitizenArea> query = context.CitizenAreas
               .AsNoTracking()
               .AsQueryable();

            return await query
                .Where(l => l.MinLat <= (double)gps.Latitude && l.MaxLat >= (double)gps.Latitude && l.MinLng <= (double)gps.Longitude && l.MaxLng >= (double)gps.Longitude)
                .FirstOrDefaultAsync();
        }
        #endregion
    }
}
