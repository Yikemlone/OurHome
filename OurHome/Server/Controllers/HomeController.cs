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

            if (user == null) return NotFound();

            var usersHomes = _unitOfWork.HomeService.GetAllAsync(user);
            return Ok(usersHomes);
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<ActionResult<Home>> Get(int ID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] HomeDTO homeDTO)
        {
            if(homeDTO == null || homeDTO.Name == string.Empty || homeDTO.Name == null)
                return BadRequest("Invalid data");

            Home home = new Home();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            await _unitOfWork.HomeService.AddAsync(home);
            await _unitOfWork.SaveAsync();

            return Created("/api/home/add", homeDTO);
        }

        [HttpPost]
        [Route("update")]
        public Task Update([FromBody] HomeDTO homeDTO) 
        {

            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("delete/{ID}")]
        public Task Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<User> GetUser() 
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _userManager.FindByIdAsync(userID);
            return user;
        }

    }
}
