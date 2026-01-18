using Application.ApplicationException;
using Application.DTO;
using Application.Enum;
using Application.Interface.IGrpcClient;
using Application.Interface.IPublisher;
using Application.Interface.IService;
using AutoMapper;
using Domain.Aggregate;
using Domain.Enum;
using Domain.IRepository;
using Domain.ValueObject;
using Google.Protobuf.WellKnownTypes;
using IAMServer.gRPC;
using SWD392.MessageBroker;

namespace Application.Service
{
    public class CitizenService : ICitizenService
    {
        private readonly IIAMClient iAMClient;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISignalRPublisher signalRPublisher;

        public CitizenService(
            IIAMClient iAMClient,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISignalRPublisher signalRPublisher
            )
        {
            this.iAMClient = iAMClient;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.signalRPublisher = signalRPublisher;
        }

        #region Methods
        public async Task<IEnumerable<CitizenAreaDTO>> GetCitizenAreas(
            Guid callerId,
            string callerRole)
        {
            // Validate citizen area list existence
            var list = await unitOfWork
                .GetRepository<ICitizenAreaRepository>()
                .GetAllAsync();

            if (list == null || !list.Any())
                throw new CitizenAreaNotFound(
                    "Citizen area list is empty");

            return mapper.Map<IEnumerable<CitizenAreaDTO>>(list);
        }

        public async Task<IEnumerable<CitizenProfileDTO>> GetCitizenProfiles(
            QueryCitizenProfileDTO dto,
            Guid callerId,
            string callerRole)
        {
            // Validate authorization
            ValidateAuthorization(callerRole);

            // Validate citizen profile existence
            var list = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCitizenProfiles(dto.DisplayName, dto.PageIndex, dto.PageSize);

            if (list == null || !list.Any())
                throw new CitizenProfileNotFound(
                    "The citizen profile list is empty");

            return mapper.Map<IEnumerable<CitizenProfileDTO>>(list);
        }

        public async Task<CitizenProfileDetailDTO> GetCitizenProfileDetail(
            Guid citizenProfileId,
            Guid callerId,
            string callerRole)
        {
            // Validate citizen profile existence
            var profile = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCitizenProfileDetailById(citizenProfileId);

            if (profile == null)
                throw new CitizenProfileNotFound(
                    $"The citizen profile with ID: {citizenProfileId} is not found");

            // Validate ownership
            ValidateOwnership(profile.UserID, callerId, callerRole);

            // Get user from IAM Service
            var response = await iAMClient.GetUser(new GetUserRequest()
            {
                CreatedBy = callerId.ToString(),
                Role = callerRole,
                UserId = profile.UserID.ToString()
            });

            // Mapping
            var mappedProfile = mapper.Map<CitizenProfileDetailDTO>(profile);
            mappedProfile.Email = response.Email;
            mappedProfile.FullName = response.FullName;
            mappedProfile.Gender = response.Gender;
            mappedProfile.Dob = response.Dob.ToDateTime();

            return mappedProfile; 
        }

        public async Task<IEnumerable<CollectionReportDTO>> GetCollectionReports(
            QueryCollectionReportDTO dto,
            Guid callerId, 
            string callerRole)
        {
            // Validate collection report list existence
            var list = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCollectionReports(dto.RegionCode, dto.WasteType, dto.Description);

            if (list == null || !list.Any())
                throw new CollectionReportNotFound(
                    "Collection report list is empty");

            return mapper.Map<IEnumerable<CollectionReportDTO>>(list);
        }

        public async Task CreateCitizenProfile(
            CreateCitizenProfileDTO dto)
        {
            // Create user from IAM Service
            var response = await iAMClient.CreateUser(new CreateUserRequest()
            {
                Dob = Timestamp.FromDateTime(dto.Dob.ToUniversalTime()),
                Email = dto.Email,
                FullName = dto.FullName,
                Gender = dto.Gender,
                IsActive = true,
                Password = dto.Password,
                Role = RoleKey.CITIZEN,
            });

            // Apply domain
            var citizenProfile = new CitizenProfile(
                Guid.NewGuid(),
                Guid.Parse(response.UserId),
                dto.DisplayName,
                dto.AvatarName,
                true);

            // Apply persistence
            await unitOfWork.BeginTransactionAsync();
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .Add(citizenProfile);
            await unitOfWork.CommitAsync(response.UserId);
        }

        public async Task CreateCollectionReport(
            CreateCollectionReportDTO dto,
            Guid callerId,
            string callerRole)
        {
            // Validate citizen profile existence
            var profile = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCitizenProfileDetailById(dto.CitizenProfileId);

            if (profile == null)
                throw new CitizenProfileNotFound(
                    $"The citizen profile with ID: {dto.CitizenProfileId} is not found");

            // Validate ownership
            ValidateOwnership(profile.UserID, callerId, callerRole);

            // Convert latitude and longitude to region code
            var area = await unitOfWork
                .GetRepository<ICitizenAreaRepository>()
                .GetCitizenAreaByGPS((double)dto.Latitude, (double)dto.Longitude);

            if (area == null)
               throw new CitizenAreaNotFound("The GPS location is out of service area");
               
            // Apply domain
            var report = profile.AddCollectionReport(
                    Guid.NewGuid(),
                    dto.WasteType,
                    dto.Description,
                    new GPS(dto.Latitude, dto.Longitude),
                    area.CitizenAreaID,
                    dto.ImageName);

            // Apply persistence
            await unitOfWork.BeginTransactionAsync();
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .AddCollectionReport(report);
            await unitOfWork.CommitAsync(callerId.ToString());
        }

        public async Task CreateComplaintReport(
            CreateComplaintReportDTO dto,
            Guid callerId,
            string callerRole)
        {
            // Validate citizen profile existence
            var profile = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetByIdAsync(dto.CitizenProfileId);

            if (profile == null)
                throw new CitizenProfileNotFound(
                    $"The citizen profile with ID: {dto.CitizenProfileId} is not found");

            // Validate ownership
            ValidateOwnership(profile.UserID, callerId, callerRole);

            // Apply domain
            var report = profile.AddComplaintReport(
                dto.CitizenAreaId,
                Guid.NewGuid(),
                dto.Description,
                dto.Title,
                dto.ImageName);

            // Apply persistence
            await unitOfWork.BeginTransactionAsync();
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .AddComplaintReport(report);
            await unitOfWork.CommitAsync(callerId.ToString());
        }

        public async Task UserSyncDeleting(UserDeleteDTO dto)
        {
            // Validate citizen profile existence
            var profile = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCitizenProfileByUserId(dto.UserID);

            if (profile == null)
                throw new CitizenProfileNotFound(
                    $"The citizen profile with ID: {dto.UserID} is not found");

            // Apply domain
            profile.Inactivate();

            // Apply persistence;
            await unitOfWork.BeginTransactionAsync();
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .Update(profile.CitizenProfileID, profile);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateIncentiveReward(IncentiveRewardDTO dto)
        {
            // Validate collection report existence
            var collectionReport = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCollectionReportById(dto.CollectionReportID);

            if (collectionReport == null)
               throw new CollectionReportNotFound(
                    $"The collection report with ID: {dto.CollectionReportID} is not found");
            
            // Validate citizen profile existence
            var profile = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetByIdAsync(collectionReport.CitizenProfileID);

            if (profile == null)
                throw new CitizenProfileNotFound(
                    $"The citizen profile with ID: {collectionReport.CitizenProfileID} is not found");

            // Validate citizen area existence
            var citizenArea = await unitOfWork
                .GetRepository<ICitizenAreaRepository>()
                .GetByIdAsync(collectionReport.CitizenAreaID);

            if (citizenArea == null)
                throw new CitizenAreaNotFound(
                    $"The citizen area with ID: {collectionReport.CitizenAreaID} is not found");

            // Apply domain
            var rewardHistory =  profile.AddRewardHistory(
                collectionReport.CitizenAreaID, 
                Guid.NewGuid(), 
                dto.Point, 
                dto.Reason);

            // Apply persistence
            await unitOfWork.BeginTransactionAsync();
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .AddRewardHistory(rewardHistory);
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .Update(profile.CitizenProfileID, profile);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateCollectionReportStatus(CollectionReportStatusUpdateDTO dto)
        {
            // Validate collection report existence
            var collectionReport = await unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .GetCollectionReportById(dto.CollectionReportID);

            if (collectionReport == null)
                throw new CollectionReportNotFound(
                    $"The collection report with ID: {dto.CollectionReportID} is not found");

            // Validate status enum
            if (!System.Enum.TryParse<CollectionReportStatus>(dto.Status, true, out var status))
                throw new Exception(
                    $"Invalid collection report status: {dto.Status}");

            // Apply domain
            collectionReport.UpdateStatus(status);

            // Apply persistence
            await unitOfWork.BeginTransactionAsync();
            unitOfWork
                .GetRepository<ICitizenProfileRepository>()
                .UpdateCollectionReport(collectionReport);
            await unitOfWork.CommitAsync();

            // Notify via SignalR   
            await signalRPublisher.PublishEnvelop(
                new SignalREnvelope.SignalREnvelope
                {
                    Method = "UpdateStatus",
                    Payload = mapper.Map<CollectionReportDTO>(collectionReport),
                    Timestamp = DateTime.UtcNow,
                    SourceService = "CITIZEN_SERVICE"
                });
        }
        #endregion

        #region Private Helper
        private void ValidateOwnership(
            Guid profileUserId,
            Guid callerId,
            string callerRole)
        {
            if (!(callerRole == RoleKey.ADMIN || callerRole == RoleKey.SUPER_ADMIN)
                && callerId != profileUserId)
                throw new UnauthorizeView(
                    $"User can not view this citizen profile, user has not created this profile");
        }

        private void ValidateAuthorization(
            string callerRole)
        {
            if (!(callerRole == RoleKey.ADMIN || callerRole == RoleKey.SUPER_ADMIN))
                throw new UnauthorizeView(
                    $"{callerRole} can not view all citizen profiles");
        }
        #endregion
    }
}
