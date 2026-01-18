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

        Task<CitizenProfile?> GetCitizenProfileDetailById(
            Guid citizenProfileId);

        Task<CitizenProfile?> GetCitizenProfileByUserId(
            Guid userId);

        Task<IEnumerable<CollectionReport>> GetCollectionReports(
            string regionCode,
            string wasteType,
            string description);

        void AddComplaintReport(
            ComplaintReport complaintReport);

        void AddCollectionReport(
            CollectionReport collectionReport);
    }
}
