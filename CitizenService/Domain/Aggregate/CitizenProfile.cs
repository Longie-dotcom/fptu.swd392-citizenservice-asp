using Domain.DomainException;
using Domain.Entity;
using Domain.ValueObject;

namespace Domain.Aggregate
{
    public class CitizenProfile
    {
        #region Attributes
        private readonly List<RewardHistory> rewardHistories = new List<RewardHistory>();
        private readonly List<ComplaintReport> complaintReports = new List<ComplaintReport>();
        private readonly List<CollectionReport> collectionReports = new List<CollectionReport>();
        #endregion

        #region Properties
        public Guid CitizenProfileID { get; private set; }
        public Guid UserID { get; private set; } // Mapped key with IAM Service
        public string DisplayName { get; private set; }
        public string AvatarName { get; private set; }
        public int PointBalance { get; private set; }
        public DateTime JoinedAt { get; private set; }
        public bool IsActive { get; private set; }

        public IReadOnlyCollection<RewardHistory> RewardHistories
        { 
            get { return rewardHistories.AsReadOnly(); } 
        }
        public IReadOnlyCollection<ComplaintReport> ComplaintReports
        {
            get { return complaintReports.AsReadOnly(); }
        }
        public IReadOnlyCollection<CollectionReport> CollectionReports
        {
            get { return collectionReports.AsReadOnly(); }
        }
        #endregion

        protected CitizenProfile() { }

        public CitizenProfile(
            Guid citizenProfileId,
            Guid userId, 
            string displayName,
            string avatarName,
            bool isActive = true)
        {
            if (userId == Guid.Empty || citizenProfileId == Guid.Empty)
                throw new CitizenProfileAggregateException(
                    "Can not create citizen profile with no account");

            CitizenProfileID = citizenProfileId;
            UserID = userId;
            DisplayName = displayName;
            AvatarName = avatarName;
            PointBalance = 0;
            JoinedAt = DateTime.UtcNow;
            IsActive = isActive;
        }

        #region Methods
        public ComplaintReport AddComplaintReport(
            Guid citizenAreaId,
            Guid complaintReportId,
            string description,
            string title,
            string imageName)
        {
            if (!IsActive)
                throw new CitizenProfileAggregateException(
                    "Inactive citizen cannot create complaint report");

            if (complaintReports.Any(r => r.ComplaintReportID == complaintReportId))
                throw new CitizenProfileAggregateException(
                    "Duplicate complaint report");

            if (string.IsNullOrWhiteSpace(title))
                throw new CitizenProfileAggregateException(
                    "Complaint title is required");

            if (string.IsNullOrWhiteSpace(description))
                throw new CitizenProfileAggregateException(
                    "Complaint description is required");

            var report = new ComplaintReport(
                CitizenProfileID,
                citizenAreaId,
                complaintReportId,
                description,
                title,
                imageName
            );

            complaintReports.Add(report);

            // Return for persistence
            return report; 
        }

        public CollectionReport AddCollectionReport(
            Guid collectionReportId,
            string wasteType,
            string description,
            GPS gps,
            Guid citizenAreaID,
            string imageName)
        {
            if (!IsActive)
                throw new CitizenProfileAggregateException(
                    "Inactive citizen cannot create collection report");

            if (collectionReports.Any(r => r.CollectionReportID == collectionReportId))
                throw new CitizenProfileAggregateException(
                    "Duplicate collection report");

            if (string.IsNullOrEmpty(imageName))
                throw new CitizenProfileAggregateException(
                    "Collection report needs an image for preview");

            if (string.IsNullOrWhiteSpace(description))
                throw new CitizenProfileAggregateException(
                    "Collection description is required");

            var report = new CollectionReport(
                CitizenProfileID,
                collectionReportId,
                wasteType,
                description,
                gps,
                citizenAreaID,
                imageName
            );

            collectionReports.Add(report);

            // Return for persistence
            return report;
        }

        public RewardHistory AddRewardHistory(
            Guid citizenAreaId,
            Guid rewardHistoryId,
            int point,
            string reason)
        {
            if (!IsActive)
                throw new CitizenProfileAggregateException(
                    "Inactive citizen cannot receive rewards");

            var rewardHistory = new RewardHistory(
                CitizenProfileID,
                citizenAreaId,
                rewardHistoryId,
                point,
                reason
            );

            // Update point balance
            PointBalance += point;
            rewardHistories.Add(rewardHistory);

            // Return for persistence
            return rewardHistory;
        }

        public void Inactivate()
        {
            IsActive = false;
        }
        #endregion

        #region Private Validators
        #endregion

        #region Private Helpers
        #endregion
    }
}
