using Application.DTO;

namespace Application.Interface.IService
{
    public interface ICitizenService
    {
        Task<IEnumerable<CitizenProfileDTO>> GetCitizenProfiles(
            QueryCitizenProfileDTO dto,
            Guid callerId,
            string callerRole);

        Task<CitizenProfileDetailDTO> GetCitizenProfileDetail(
            Guid citizenProfileId,
            Guid callerId,
            string callerRole);

        Task<CitizenProfileDetailDTO> GetMyCitizenProfile(
            Guid callerId,
            string callerRole,
            QueryMyCitizenProfileDTO dto);

        Task<IEnumerable<CitizenAreaDTO>> GetCitizenAreas(
            Guid callerId,
            string callerRole);

        Task<IEnumerable<CollectionReportDTO>> GetCollectionReports(
            QueryCollectionReportDTO dto,
            Guid callerId,
            string callerRole);

        Task CreateCitizenProfile(
            CreateCitizenProfileDTO dto);

        Task CreateCollectionReport(
            CreateCollectionReportDTO dto,
            Guid callerId,
            string callerRole);

        Task CreateComplaintReport(
            CreateComplaintReportDTO dto,
            Guid callerId,
            string callerRole);

        Task UserSyncDeleting(
            SWD392.MessageBroker.UserDeleteDTO dto);

        Task UpdateIncentiveReward(
            SWD392.MessageBroker.IncentiveRewardDTO dto);

        Task UpdateCollectionReportStatus(
            SWD392.MessageBroker.CollectionReportStatusUpdateDTO dto);

        Task<IEnumerable<ComplaintReportDTO>> GetComplaintReports(
            QueryComplaintReportDTO dto,
            Guid callerId,
            string callerRole);

        Task ResolveComplaintReport(
            UpdateComplaintReportDTO dto,
            Guid callerId,
            string callerRole);

        Task<IEnumerable<LeaderboardEntryDTO>> GetLeaderboard(
            QueryLeaderboardDTO dto);
    }
}