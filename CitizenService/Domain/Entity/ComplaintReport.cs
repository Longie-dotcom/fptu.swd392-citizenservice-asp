using Domain.Aggregate;
using Domain.Enum;

namespace Domain.Entity
{
    public class ComplaintReport
    {
        #region Attributes
        #endregion

        #region Properties
        public Guid ComplaintReportID { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public ComplaintReportStatus Status { get; private set; }
        public DateTime ReportAt { get; private set; }

        public Guid CitizenProfileID { get; private set; }
        public Guid CitizenAreaID { get; private set; }
        public CitizenArea CitizenArea { get; private set; }
        #endregion

        protected ComplaintReport() { }

        public ComplaintReport(
            Guid citizenProfileId,
            Guid citizenAreaId,
            Guid complaintReportId, 
            string description, 
            string title,
            string imageName)
        {
            CitizenProfileID = citizenProfileId;
            CitizenAreaID = citizenAreaId;
            ComplaintReportID = complaintReportId;
            Description = description;
            Title = title;
            ImageName = imageName;
            Status = ComplaintReportStatus.Pending;
            ReportAt = DateTime.UtcNow;
        }

        #region Methods
        #endregion
    }
}
