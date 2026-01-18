using Domain.Aggregate;
using Domain.Enum;
using Domain.ValueObject;

namespace Domain.Entity
{
    public class CollectionReport
    {
        #region Attributes
        #endregion

        #region Properties
        public Guid CollectionReportID { get; private set; } // Mapped key with Enterprise Service, Collection Service
        public string WasteType { get; private set; }
        public string Description { get; private set; }
        public GPS GPS { get; private set; }
        public string RegionCode { get; private set; }
        public string ImageName { get; private set; }
        public CollectionReportStatus Status { get; private set; }
        public DateTime ReportAt { get; private set; }

        public Guid CitizenProfileID { get; private set; }
        public Guid CitizenAreaID { get; private set; }
        public CitizenArea CitizenArea { get; private set; }
        #endregion

        protected CollectionReport() { }

        public CollectionReport(
            Guid citizenProfileId,
            Guid collectionReportId, 
            string wasteType, 
            string description, 
            GPS gps,
            Guid citizenAreaId,
            string imageName)
        {
            CitizenProfileID = citizenProfileId;
            CollectionReportID = collectionReportId;
            WasteType = wasteType;
            Description = description;
            GPS = gps;
            CitizenAreaID = citizenAreaId;
            ImageName = imageName;
            Status = CollectionReportStatus.Pending;
            ReportAt = DateTime.UtcNow;
        }

        #region Methods
        public void UpdateStatus(CollectionReportStatus status)
        {
            Status = status;
        }
        #endregion
    }
}
