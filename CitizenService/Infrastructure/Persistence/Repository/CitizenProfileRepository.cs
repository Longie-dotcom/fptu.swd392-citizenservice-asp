using Domain.Aggregate;
using Domain.Entity;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class CitizenProfileRepository :
        GenericRepository<CitizenProfile>,
        ICitizenProfileRepository
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        public CitizenProfileRepository(CitizenDBContext context) : base(context) { }

        #region Methods
        public async Task<IEnumerable<CitizenProfile>> GetCitizenProfiles(
            string displayName,
            int pageIndex,
            int pageSize)
        {
            IQueryable<CitizenProfile> query = context.CitizenProfiles
                .AsNoTracking()
                .AsQueryable();

            // Filter by display name (optional)
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                query = query.Where(c =>
                    EF.Functions.Like(
                        c.DisplayName,
                        $"%{displayName}%"));
            }

            return await query
                .OrderByDescending(c => c.JoinedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<CitizenProfile?> GetCitizenProfileDetailById(Guid citizenProfileId)
        {
            return await context.CitizenProfiles
                .Include(c => c.CollectionReports)
                .Include(c => c.ComplaintReports)
                .Include(c => c.RewardHistories)
                .FirstOrDefaultAsync(c => c.CitizenProfileID == citizenProfileId);
        }

        public async Task<CitizenProfile?> GetCitizenProfileByUserId(
            Guid userId)
        {
            return await context.CitizenProfiles.FirstOrDefaultAsync(
                c => c.UserID == userId);
        }

        public void AddComplaintReport(ComplaintReport complaintReport)
        {
            context.ComplaintReports.Add(complaintReport);
        }

        public void AddCollectionReport(CollectionReport collectionReport)
        {
            context.CollectionReports.Add(collectionReport);
        }
        #endregion
    }
}
