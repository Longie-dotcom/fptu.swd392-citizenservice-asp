using Domain.Enum;
using Domain.ValueObject;

namespace Application.DTO
{
    // Citizen Area
    public class CitizenAreaDTO
    {
        public Guid CitizenAreaID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    // Citizen Profile
    public class CitizenProfileDetailDTO
    {
        public Guid CitizenProfileID { get; set; }
        public Guid UserID { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string AvatarName { get; set; } = string.Empty;
        public int PointBalance { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsActive { get; set; }

        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Dob { get; set; }

        public List<CollectionReportDTO> CollectionReports { get; set; } = new();
        public List<ComplaintReportDTO> ComplaintReports { get; set; } = new();
        public List<RewardHistoryDTO> RewardHistories { get; set; } = new();
    }

    public class CitizenProfileDTO
    {
        public Guid CitizenProfileID { get; set; }
        public Guid UserID { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string AvatarName { get; set; } = string.Empty;
        public int PointBalance { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class QueryCitizenProfileDTO
    {
        public string DisplayName { get; set; } = string.Empty;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 1;
    }

    public class CreateCitizenProfileDTO
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public string Password { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;
        public string AvatarName { get; set; } = string.Empty;
    }

    // Collection Report
    public class CollectionReportDTO
    {
        public Guid CollectionReportID { get; set; }
        public string WasteType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public GPS GPS { get; set; } = new GPS(0, 0);
        public string RegionCode { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public CollectionReportStatus Status { get; set; }
        public DateTime ReportAt { get; set; }
        public Guid CitizenProfileID { get; set; }
    }

    public class QueryCollectionReportDTO
    {
        public string RegionCode { get; set; } = string.Empty;
        public string WasteType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CreateCollectionReportDTO
    {
        public Guid CitizenProfileId { get; set; }
        public string WasteType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ImageName { get; set; } = string.Empty;
    }

    // Complaint Report
    public class ComplaintReportDTO
    {
        public Guid ComplaintReportID { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public ComplaintReportStatus Status { get; set; }
        public DateTime ReportAt { get; set; }
        public Guid CitizenProfileID { get; set; }
        public CitizenAreaDTO CitizenArea { get; set; } = new CitizenAreaDTO();
    }

    public class CreateComplaintReportDTO
    {
        public Guid CitizenAreaId { get; set; }
        public Guid CitizenProfileId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
    }

    // Reward History
    public class RewardHistoryDTO
    {
        public Guid RewardHistoryID { get; set; }
        public int Point { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; }
        public Guid CitizenProfileID { get; set; }
        public CitizenAreaDTO CitizenArea { get; set; } = new CitizenAreaDTO();
    }
}
