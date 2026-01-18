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
        #endregion
    }
}
