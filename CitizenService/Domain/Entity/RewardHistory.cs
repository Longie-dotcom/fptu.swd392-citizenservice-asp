using Domain.Aggregate;

namespace Domain.Entity
{
    public class RewardHistory
    {
        #region Attributes
        #endregion

        #region Properties
        public Guid RewardHistoryID { get; private set; }
        public int Point { get; private set; }
        public string Reason { get; private set; }
        public DateTime OccurredAt { get; private set; }

        public Guid CitizenProfileID { get; private set; }
        public Guid CitizenAreaID { get; private set; }
        public CitizenArea CitizenArea { get; private set; }
        #endregion

        protected RewardHistory() { }

        public RewardHistory(
            Guid citizenProfileId,
            Guid citizenAreaId,
            Guid rewardHistoryId, 
            int point, 
            string reason)
        {
            CitizenProfileID = citizenProfileId;
            CitizenAreaID = citizenAreaId;
            RewardHistoryID = rewardHistoryId;
            Point = point;
            Reason = reason;
            OccurredAt = DateTime.UtcNow;
        }

        #region Methods
        #endregion
    }
}
