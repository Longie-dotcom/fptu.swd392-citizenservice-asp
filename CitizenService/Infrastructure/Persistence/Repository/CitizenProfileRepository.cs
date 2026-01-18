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
                    .ThenInclude(cr => cr.CitizenArea)
                .Include(c => c.RewardHistories)
                    .ThenInclude(cr => cr.CitizenArea)
                .FirstOrDefaultAsync(c => c.CitizenProfileID == citizenProfileId);
        }

        public async Task<CitizenProfile?> GetCitizenProfileByUserId(
            Guid userId)
        {
            return await context.CitizenProfiles.FirstOrDefaultAsync(
                c => c.UserID == userId);
        }

        public async Task<IEnumerable<CollectionReport>> GetCollectionReports(
            string regionCode,
            string wasteType,
            string description)
        {
            IQueryable<CollectionReport> query = context.CollectionReports
                .AsNoTracking()
                .AsQueryable();

            // Apply filters only if they are not null or empty
            if (!string.IsNullOrEmpty(regionCode))
                query = query.Where(c => c.RegionCode == regionCode);

            if (!string.IsNullOrEmpty(wasteType))
                query = query.Where(c => c.WasteType == wasteType);

            if (!string.IsNullOrEmpty(description))
                query = query.Where(c => c.Description.Contains(description));

            // Order and execute query
            return await query
                .OrderByDescending(c => c.ReportAt)
                .ToListAsync();
        }

        public void AddComplaintReport(ComplaintReport complaintReport)
        {
            context.ComplaintReports.Add(complaintReport);
        }

        public void AddCollectionReport(CollectionReport collectionReport)
        {
            context.CollectionReports.Add(collectionReport);
        }

        public async Task<CollectionReport?> GetCollectionReportById(
            Guid collectionReportId)
        {
            return await context.CollectionReports
                .FirstOrDefaultAsync(c => c.CollectionReportID == collectionReportId);
        }

        public void AddRewardHistory(RewardHistory rewardHistory) { 
            context.RewardHistories.Add(rewardHistory);

        }

        public void UpdateCollectionReport(CollectionReport collection)
        {
            context.CollectionReports.Update(collection);
        }   
        #endregion
    }
}
