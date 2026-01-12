using Application.DTO;
using AutoMapper;
using Domain.Aggregate;
using Domain.Entity;
using System.Data;

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
            CreateMap<CollectionReport, CollectionReportDTO>()
                .ForMember(dest => dest.CitizenProfile,
                    opt => opt.MapFrom(src => src.CitizenProfile));
            CreateMap<ComplaintReport, ComplaintReportDTO>()
                .ForMember(dest => dest.CitizenProfile,
                    opt => opt.MapFrom(src => src.CitizenProfile));
            CreateMap<RewardHistory, RewardHistoryDTO>()
                .ForMember(dest => dest.CitizenProfile,
                    opt => opt.MapFrom(src => src.CitizenProfile));

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
