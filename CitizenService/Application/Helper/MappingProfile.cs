using Application.DTO;
using AutoMapper;
using Domain.Aggregate;
using Domain.Entity;

namespace Application.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Citizen Area
            // Aggregate
            CreateMap<CitizenArea, CitizenAreaDTO>();
            #endregion

            #region Citizen
            // Entity
            CreateMap<CollectionReport, CollectionReportDTO>();
            CreateMap<Domain.DTO.CollectionReportDTO, Application.DTO.CollectionReportDTO>()
                .ForMember(dest => dest.CitizenName,
                    opt => opt.MapFrom(src => src.CitizenName))
                .ForMember(dest => dest.CollectionReportID,
                    opt => opt.MapFrom(src => src.CollectionReport.CollectionReportID))
                .ForMember(dest => dest.WasteType,
                    opt => opt.MapFrom(src => src.CollectionReport.WasteType))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.CollectionReport.Description))
                .ForMember(dest => dest.GPS,
                    opt => opt.MapFrom(src => src.CollectionReport.GPS))
                .ForMember(dest => dest.RegionCode,
                    opt => opt.MapFrom(src => src.CollectionReport.CitizenArea.RegionCode))
                .ForMember(dest => dest.ImageName,
                    opt => opt.MapFrom(src => src.CollectionReport.ImageName))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.CollectionReport.Status))
                .ForMember(dest => dest.ReportAt,
                    opt => opt.MapFrom(src => src.CollectionReport.ReportAt))
                .ForMember(dest => dest.CitizenProfileID,
                    opt => opt.MapFrom(src => src.CollectionReport.CitizenProfileID));

            CreateMap<ComplaintReport, ComplaintReportDTO>()
                .ForMember(dest => dest.CitizenArea,
                    opt => opt.MapFrom(src => src.CitizenArea));
            CreateMap<RewardHistory, RewardHistoryDTO>()
                .ForMember(dest => dest.CitizenArea,
                    opt => opt.MapFrom(src => src.CitizenArea));

            // Aggregate
            CreateMap<CitizenProfile, CitizenProfileDTO>();
            CreateMap<CitizenProfile, CitizenProfileDetailDTO>()
                .ForMember(dest => dest.CollectionReports,
                    opt => opt.MapFrom(src => src.CollectionReports.ToList()))
                .ForMember(dest => dest.ComplaintReports,
                    opt => opt.MapFrom(src => src.ComplaintReports.ToList()))
                .ForMember(dest => dest.RewardHistories,
                    opt => opt.MapFrom(src => src.RewardHistories.ToList()));
            #endregion
        }
    }
}
