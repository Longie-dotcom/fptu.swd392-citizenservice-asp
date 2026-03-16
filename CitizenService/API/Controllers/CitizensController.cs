using API.Helper;
using Application.DTO;
using Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWD392.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CitizensController : ControllerBase
    {
        #region Attributes
        private readonly ICitizenService citizenService;
        #endregion

        #region Properties
        #endregion

        public CitizensController(ICitizenService citizenService)
        {
            this.citizenService = citizenService;
        }

        #region Methods
        [AuthorizePrivilege("ViewCitizenProfile")]
        [HttpGet]
        public async Task<IActionResult> GetCitizenProfiles(
            [FromQuery] QueryCitizenProfileDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            var profiles = await citizenService.GetCitizenProfiles(
                dto,
                claims.userId,
                claims.role);
            return Ok(profiles);
        }

        [AuthorizePrivilege("ViewCitizenProfile")]
        [HttpGet("{citizenProfileId:guid}")]
        public async Task<IActionResult> GetCitizenProfileDetail(
            Guid citizenProfileId)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            var profile = await citizenService.GetCitizenProfileDetail(
                citizenProfileId,
                claims.userId,
                claims.role);
            return Ok(profile);
        }

        [AuthorizePrivilege("ViewCitizenProfile")]
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyCitizenProfile(
            [FromQuery] QueryMyCitizenProfileDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            var profile = await citizenService.GetMyCitizenProfile(
                claims.userId,
                claims.role,
                dto);
            return Ok(profile);
        }

        [AuthorizePrivilege("ViewCitizenArea")]
        [HttpGet("area")]
        public async Task<IActionResult> GetCitizenAreas()
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            var areas = await citizenService.GetCitizenAreas(
                claims.userId,
                claims.role);
            return Ok(areas);
        }

        [AuthorizePrivilege("ViewCollectionReport")]
        [HttpGet("collection-report")]
        public async Task<IActionResult> GetCollectionReports(
            [FromQuery] QueryCollectionReportDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            var collectionReports = await citizenService.GetCollectionReports(
                dto,
                claims.userId,
                claims.role);
            return Ok(collectionReports);
        }

        [AuthorizePrivilege("ViewCollectionReport")]
        [HttpGet("complaint-report")]
        public async Task<IActionResult> GetComplaintReports(
            [FromQuery] QueryComplaintReportDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            var complaintReports = await citizenService.GetComplaintReports(
                dto,
                claims.userId,
                claims.role);
            return Ok(complaintReports);
        }

        [AuthorizePrivilege("CreateCollectionReport")]
        [HttpPost("collection-report")]
        public async Task<IActionResult> CreateCollectionReport(
            [FromBody] CreateCollectionReportDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            await citizenService.CreateCollectionReport(
                dto,
                claims.userId,
                claims.role);
            return Ok("Report collection successfully.");
        }

        [AuthorizePrivilege("CreateComplaintReport")]
        [HttpPost("complaint-report")]
        public async Task<IActionResult> CreateComplaintReport(
            [FromBody] CreateComplaintReportDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            await citizenService.CreateComplaintReport(
                dto,
                claims.userId,
                claims.role);
            return Ok("Report complaint successfully.");
        }

        [AuthorizePrivilege("ViewCollectionReport")]
        [HttpPut("complaint-report/resolve")]
        public async Task<IActionResult> ResolveComplaintReport(
            [FromBody] UpdateComplaintReportDTO dto)
        {
            var claims = CheckClaimHelper.CheckClaim(User);
            await citizenService.ResolveComplaintReport(
                dto,
                claims.userId,
                claims.role);
            return Ok("Complaint report resolved successfully.");
        }

        [AllowAnonymous]
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard(
            [FromQuery] QueryLeaderboardDTO dto)
        {
            var result = await citizenService.GetLeaderboard(dto);
            return Ok(result);
        }
        #endregion
    }
}
