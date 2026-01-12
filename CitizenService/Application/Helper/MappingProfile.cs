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
