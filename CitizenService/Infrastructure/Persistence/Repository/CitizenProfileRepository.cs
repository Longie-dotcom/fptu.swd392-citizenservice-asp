using Domain.Aggregate;
using Domain.Entity;
using Domain.Enum;
using Domain.IRepository;
using Domain.DTO;
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
            string? displayName,
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
            return await context.CitizenProfiles
                .Include(c => c.CollectionReports)
                .Include(c => c.ComplaintReports)
                    .ThenInclude(cr => cr.CitizenArea)
                .Include(c => c.RewardHistories)
                    .ThenInclude(cr => cr.CitizenArea)
                .FirstOrDefaultAsync(c => c.UserID == userId);
        }

        public async Task<IEnumerable<CollectionReportDTO>> GetCollectionReports(
            string? regionCode,
            string? wasteType,
            string? description)
        {
            var query = context.CollectionReports
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(wasteType))
                query = query.Where(c => c.WasteType == wasteType);

            if (!string.IsNullOrEmpty(description))
                query = query.Where(c => c.Description.Contains(description));

            if (!string.IsNullOrEmpty(regionCode))
                query = query.Where(c => c.CitizenArea.RegionCode == regionCode);

            var result = await query
                .Join(context.CitizenProfiles,
                    report => report.CitizenProfileID,
                    citizen => citizen.CitizenProfileID,
                    (report, citizen) => new CollectionReportDTO
                    {
                        CollectionReport = report,
                        CitizenName = citizen.DisplayName
                    })
                .OrderByDescending(x => x.CollectionReport.ReportAt)
                .ToListAsync();

            return result;
        }

        public async Task<CollectionReportDTO?> GetCollectionReportDetailById(
            Guid collectionReportId)
        {
            var result = await context.CollectionReports
                .AsNoTracking()
                .Where(r => r.CollectionReportID == collectionReportId)
                .Join(context.CitizenProfiles,
                    report => report.CitizenProfileID,
                    citizen => citizen.CitizenProfileID,
                    (report, citizen) => new CollectionReportDTO
                    {
                        CollectionReport = report,
                        CitizenName = citizen.DisplayName
                    })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<CollectionReport?> GetCollectionReportById(
            Guid collectionReportId)
        {
            return await context.CollectionReports
                .FirstOrDefaultAsync(c => c.CollectionReportID == collectionReportId);
        }

        public void AddComplaintReport(
            ComplaintReport complaintReport)
        {
            context.ComplaintReports.Add(complaintReport);
        }

        public void AddCollectionReport(
            CollectionReport collectionReport)
        {
            context.CollectionReports.Add(collectionReport);
        }

        public void AddRewardHistory(
            RewardHistory rewardHistory)
        {
            context.RewardHistories.Add(rewardHistory);
        }

        public void UpdateCollectionReport(
            CollectionReport collection)
        {
            context.CollectionReports.Update(collection);
        }

        public async Task<IEnumerable<ComplaintReport>> GetComplaintReports(
            ComplaintReportStatus? status)
        {
            IQueryable<ComplaintReport> query = context.ComplaintReports
                .Include(r => r.CitizenArea)
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            return await query
                .OrderByDescending(r => r.ReportAt)
                .ToListAsync();
        }

        public async Task<ComplaintReport?> GetComplaintReportById(
            Guid complaintReportId)
        {
            return await context.ComplaintReports
                .Include(r => r.CitizenArea)
                .FirstOrDefaultAsync(r => r.ComplaintReportID == complaintReportId);
        }

        public void UpdateComplaintReport(
            ComplaintReport complaintReport)
        {
            context.ComplaintReports.Update(complaintReport);
        }

        public async Task<IEnumerable<(Guid CitizenProfileID, string DisplayName, string AvatarName, int TotalPoints)>> GetLeaderboard(
            Guid citizenAreaId)
        {
            var result = await context.RewardHistories
                .AsNoTracking()
                .Where(r => r.CitizenAreaID == citizenAreaId)
                .GroupBy(r => r.CitizenProfileID)
                .Select(g => new
                {
                    CitizenProfileID = g.Key,
                    TotalPoints = g.Sum(r => r.Point)
                })
                .OrderByDescending(x => x.TotalPoints)
                .Join(
                    context.CitizenProfiles.AsNoTracking(),
                    leaderItem => leaderItem.CitizenProfileID,
                    profile => profile.CitizenProfileID,
                    (leaderItem, profile) => new
                    {
                        leaderItem.CitizenProfileID,
                        profile.DisplayName,
                        profile.AvatarName,
                        leaderItem.TotalPoints
                    }
                )
                .ToListAsync();

            return result.Select(x => (x.CitizenProfileID, x.DisplayName, x.AvatarName, x.TotalPoints));
        }
        #endregion
    }
}
