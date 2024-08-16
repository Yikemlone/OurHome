using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Models.Models;
using OurHome.Shared.DTO;
using System.Security.Claims;

namespace OurHome.Server.Controllers
{
    [Authorize("User")]
    [ApiController]
    [Route("/api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly UserManager<User> _userManager;

        public HomeController(IUnitOfWorkService unitOfWorkService, UserManager<User> userManager) 
        { 
            _unitOfWork = unitOfWorkService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Home>>> GetAll()
        {
            var user = await GetUser();
            var usersHomes = _unitOfWork.HomeService.GetAllAsync(user);
            return Ok(usersHomes);
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<ActionResult<Home>> Get(int ID)
        {
            User user = await GetUser();

            if (user == null) return StatusCode(403);

            bool isUserInHome = await _unitOfWork.HomeUserService
                .IsUserInHomeAsync(user, ID);

            if (!isUserInHome) return Unauthorized();

            Home home = await _unitOfWork.HomeService.GetAsync(ID);

            if (home == null) return NotFound();

            return Ok(home);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] HomeDTO homeDTO)
        {
            if(homeDTO == null || homeDTO.Name == string.Empty || homeDTO.Name == null)
                return BadRequest("Invalid home parameters.");

            Home home = new Home();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            await _unitOfWork.HomeService.AddAsync(home);
            await _unitOfWork.SaveAsync();

            return Created("/api/home/add", homeDTO);
        }

        [HttpPost]
        [Authorize(Policy = "HomeOwner, HomeAdmin")]
        [Route("update")]
        public async Task<ActionResult<HomeDTO>> Update([FromBody] HomeDTO homeDTO) 
        {
            if (homeDTO == null || homeDTO.Name == string.Empty || homeDTO.Name == null)
                return BadRequest("Invalid home parameters.");

            var exposedClaims = User.Claims.ToDictionary(c => c.Type, c => c.Value);
            var user = await GetUser();

            bool isUserInHome = await _unitOfWork.HomeUserService
                .IsUserInHomeAsync(user, homeDTO.HomeID);

            if (!isUserInHome) return Unauthorized();

            if (!exposedClaims.ContainsKey("HomeOwner") && !exposedClaims.ContainsKey("HomeAdmin"))
                return Unauthorized();

            Home home = new Home();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            _unitOfWork.HomeService.Update(home);
            int recordsChanged = await _unitOfWork.SaveAsync();

           // if (recordsChanged == 0) return Sta("No records were changed.");

            return Ok(homeDTO);
        }

        [HttpPost]
        [Route("delete/{ID}")]
        public async Task<ActionResult> Delete(int ID)
        {
            var exposedClaims = User.Claims.ToDictionary(c => c.Type, c => c.Value);
            var user = await GetUser();

            bool isUserInHome = await _unitOfWork.HomeUserService
                .IsUserInHomeAsync(user, ID);

            if (!isUserInHome) return Unauthorized();

            if (!exposedClaims.ContainsKey("HomeOwner"))
                return Unauthorized();

            Home home = await _unitOfWork.HomeService.GetAsync(ID);
            _unitOfWork.HomeService.Delete(home);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        public virtual async Task<User?> GetUser() 
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _userManager.FindByIdAsync(userID);
            return user;
        }

    }
}
