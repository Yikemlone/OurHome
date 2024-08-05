using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Models.Models;
using OurHome.Shared.DTO;
using System.Security.Claims;

namespace OurHome.Server.Controllers
{
    //[Authorize("User")]
    [ApiController]
    [Route("/api/[controller]")]
    public class BillCoOwnerController : ControllerBase
    {
        IUnitOfWorkService _unitOfWork;
        private readonly UserManager<User> _userManager;

        public BillCoOwnerController(IUnitOfWorkService unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Bill>>> GetAll() 
        {
            User? user = await _userManager.
                FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null) return NotFound();
            List<Bill> bills = await _unitOfWork.BillService.GetAllAsync(user);

            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("{ID}")]
        public async Task<ActionResult<Bill>> Get(int ID) 
        {
            Bill bill = await _unitOfWork.BillService.GetAsync(ID);
            if (bill == null) return NotFound();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new bill with the users that need to pay for it and the co-owners of the bill
        /// </summary>
        /// <param name="createBillDTO"></param>
        /// <returns>A Ok response if the transaction went through</returns>
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] CreateBillDTO createBillDTO) 
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Bill bill) 
        {
            _unitOfWork.BillService.Update(bill);
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete([FromBody] Bill bill)
        {
            _unitOfWork.BillService.Delete(bill);
            throw new NotImplementedException();
        }
    }
} 
