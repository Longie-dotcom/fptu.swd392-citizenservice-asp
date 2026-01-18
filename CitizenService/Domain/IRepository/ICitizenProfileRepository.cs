using Domain.Aggregate;
using Domain.Entity;
namespace Domain.IRepository
{
    public interface ICitizenProfileRepository : 
        IGenericRepository<CitizenProfile>, 
        IRepositoryBase
    {
        Task<IEnumerable<CitizenProfile>> GetCitizenProfiles(
            string displayName,
            int pageIndex,
            int pageSize);

        Task<IEnumerable<CollectionReport>> GetCollectionReports();

        Task<CitizenProfile?> GetCitizenProfileDetailById(
            Guid citizenProfileId);

        Task<CitizenProfile?> GetCitizenProfileByUserId(
            Guid userId);

        void AddComplaintReport(
            ComplaintReport complaintReport);

        void AddCollectionReport(
            CollectionReport collectionReport);

        Task<CollectionReport?> GetCollectionReportById(
            Guid collectionReportId);

        void AddRewardHistory(RewardHistory rewardHistory);

        void UpdateCollectionReport(CollectionReport collection);
    }
}
