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
        public string ImageName { get; private set; }
        public CollectionReportStatus Status { get; private set; }
        public DateTime ReportAt { get; private set; }

        public Guid CitizenProfileId { get; private set; }
        public Guid CitizenAreaId { get; private set; }
        public CitizenArea CitizenArea { get; private set; }
        public CitizenProfile CitizenProfile { get; private set; }
        #endregion

        protected CollectionReport() { }

        public CollectionReport(
            Guid citizenProfileId,
            Guid citizenAreaId,
            Guid collectionReportId, 
            string wasteType, 
            string description, 
            GPS gps,
            string imageName)
        {
            CitizenProfileId = citizenProfileId;
            CitizenAreaId = citizenAreaId;
            CollectionReportID = collectionReportId;
            WasteType = wasteType;
            Description = description;
            GPS = gps;
            ImageName = imageName;
            Status = CollectionReportStatus.Pending;
            ReportAt = DateTime.UtcNow;
        }

        #region Methods
        #endregion
    }
}
