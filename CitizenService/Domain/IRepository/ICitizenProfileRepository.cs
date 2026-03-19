using Domain.Aggregate;
using Domain.DTO;
using Domain.Entity;
using Domain.Enum;
namespace Domain.IRepository
{
    public interface ICitizenProfileRepository : 
        IGenericRepository<CitizenProfile>, 
        IRepositoryBase
    {
        Task<IEnumerable<CitizenProfile>> GetCitizenProfiles(
            string? displayName,
            int pageIndex,
            int pageSize);

        Task<CitizenProfile?> GetCitizenProfileDetailById(
            Guid citizenProfileId);

        Task<CitizenProfile?> GetCitizenProfileByUserId(
            Guid userId);

        Task<IEnumerable<CollectionReportDTO>> GetCollectionReports(
            string? regionCode,
            string? wasteType,
            string? description);

        Task<CollectionReportDTO?> GetCollectionReportDetailById(
            Guid collectionReportId);

        Task<CollectionReport?> GetCollectionReportById(
            Guid collectionReportId);

        void AddComplaintReport(
            ComplaintReport complaintReport);

        void AddCollectionReport(
            CollectionReport collectionReport);

        void AddRewardHistory(
            RewardHistory rewardHistory);

        void UpdateCollectionReport(
            CollectionReport collection);

        Task<IEnumerable<ComplaintReport>> GetComplaintReports(
            ComplaintReportStatus? status);

        Task<ComplaintReport?> GetComplaintReportById(
            Guid complaintReportId);

        void UpdateComplaintReport(
            ComplaintReport complaintReport);

        Task<IEnumerable<(Guid CitizenProfileID, string DisplayName, string AvatarName, int TotalPoints)>> GetLeaderboard(
            Guid citizenAreaId);
    }
}
