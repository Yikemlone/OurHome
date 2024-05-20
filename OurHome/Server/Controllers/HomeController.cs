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

        /// <summary>
        /// Returns all the users homes they own or are a part of
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Home>>> GetAll()
        {
            // Need to get all the users homes here, not all the bills
            // Can used the User object that is with the Authorization 
            return Ok();
        }

        /// <summary>
        /// Returns a the home by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("{ID}")]
        public Task<Home> Get(int ID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Add a new bill with the users that need to pay for it and the co-owners of the bill
        /// </summary>
        /// <param name="createBillDTO"></param>
        /// <returns>A Ok response if the transaction went through</returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] HomeDTO homeDTO)
        {
            Home home = new Home();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            await _unitOfWork.HomeService.AddAsync(home);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        /// <summary>
        /// Updates the home information
        /// </summary>
        /// <param name="payorBill"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("/update")]
        public Task Update([FromBody] HomeDTO homeDTO) 
        {
            throw new NotImplementedException();
        }

    }
}
