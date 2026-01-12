using Application.DTO;
using Application.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Attributes
        private readonly ICitizenService citizenService;
        #endregion

        #region Properties
        #endregion

        public AuthController(ICitizenService citizenService)
        {
            this.citizenService = citizenService;
        }

        #region Methods
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CreateCitizenProfile(
            [FromBody] CreateCitizenProfileDTO dto)
        {
            await citizenService.CreateCitizenProfile(dto);
            return Ok("The citizen account has been created successfully.");
        }
        #endregion
    }
}
