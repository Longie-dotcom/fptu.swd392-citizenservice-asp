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

        [AuthorizePrivilege("ViewCitizenProfile")]
        [HttpGet("profile")]
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
        [HttpGet("profile/{citizenProfileId:guid}")]
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

        [AuthorizePrivilege("ReportComplaint")]
        [HttpPost("report-complaint")]
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

        [AuthorizePrivilege("ReportCollection")]
        [HttpPost("report-collection")]
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

        // UC07 - View Complaint Reports (Admin only)
        [AuthorizePrivilege("ViewComplaintReport")]
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

        // UC07 - Resolve Complaint Report (Admin only)
        [AuthorizePrivilege("ResolveComplaintReport")]
        [HttpPatch("complaint-report/{complaintReportId:guid}/resolve")]
        public async Task<IActionResult> ResolveComplaintReport(
            Guid complaintReportId,
            [FromBody] UpdateComplaintReportDTO dto)
        {
            dto.ComplaintReportId = complaintReportId;
            var claims = CheckClaimHelper.CheckClaim(User);
            await citizenService.ResolveComplaintReport(
                dto,
                claims.userId,
                claims.role);
            return Ok("Complaint report resolved successfully.");
        }

        // UC08 - View Area Leaderboard (Public)
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
