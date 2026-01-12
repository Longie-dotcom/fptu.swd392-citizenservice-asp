using Domain.Aggregate;
using Domain.IRepository;

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
        #endregion
    }
}
