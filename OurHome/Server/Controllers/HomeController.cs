using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Models.Models;
using OurHome.Shared.DTO;

namespace OurHome.Server.Controllers
{
    //[Authorize("User")]
    [ApiController]
    [Route("/api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;

        public HomeController(IUnitOfWorkService unitOfWorkService) 
        { 
            _unitOfWork = unitOfWorkService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Home>>> GetAll()
        {
            // Need to get all the users homes here, not all the bills
            // Can used the User object that is with the Authorization 
            return Ok();
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("{ID}")]
        public Task<Home> Get(int ID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] HomeDTO homeDTO)
        {
            Home home = new Home();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            await _unitOfWork.HomeService.AddAsync(home);
            await _unitOfWork.SaveAsync();
            //return Ok();
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/update")]
        public Task Update([FromBody] HomeDTO homeDTO) 
        {
            throw new NotImplementedException();
        }

    }
}
