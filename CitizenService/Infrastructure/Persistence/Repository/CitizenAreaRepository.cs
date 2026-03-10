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
            return await context.CitizenAreas
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.MinLat <= latitude && latitude <= a.MaxLat &&
                    a.MinLng <= longitude && longitude <= a.MaxLng
                );
        }
        #endregion
    }
}
