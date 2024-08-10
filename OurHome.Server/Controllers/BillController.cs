using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Models.Models;
using OurHome.Shared.DTO;
using System.Security.Claims;

namespace OurHome.Server.Controllers
{
    [Authorize(Policy = "User")]
    [ApiController]
    [Route("/api/[controller]")]
    public class BillController : ControllerBase
    {
        IUnitOfWorkService _unitOfWork;
        private readonly UserManager<User> _userManager;

        public BillController(IUnitOfWorkService unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns all the bills the user owns
        /// </summary>
        /// <returns>List of the bills the user owns</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Bill>>> GetAll() 
        {
            User? user = await _userManager.
                FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null) return Unauthorized(); // 403?

            List<Bill> bills = await _unitOfWork.BillService.GetAllAsync(user);
            return Ok(bills);
        }

        /// <summary>
        /// Returns a bill by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>A single bill by ID</returns>
        [HttpGet]
        [Route("{ID}")]
        public async Task<ActionResult<Bill>> Get(int ID) 
        {
            Bill bill = await _unitOfWork.BillService.GetAsync(ID);
            if (bill == null) return NotFound();
            return Ok(bill);
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
            if (createBillDTO == null || createBillDTO.BillPayors == null ||
                createBillDTO.Bill == null) return BadRequest("Invalid bill parameters.");

            Bill bill = createBillDTO.Bill;
            List<User> billPayors = createBillDTO.BillPayors;
            List<BillCoOwner>? billCoOwners = createBillDTO.BillCoOwners;

            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            bool isUserInHome = await _unitOfWork.HomeUserService
                .IsUserInHomeAsync(user, bill.HomeID);

            await _unitOfWork.BillService.AddAsync(bill);

            if (billCoOwners != null && billCoOwners.Count > 0)
            {
                List<User> coOwners = new List<User>();

                foreach (BillCoOwner billCoOwner in billCoOwners)
                { 
                    coOwners.Add(billCoOwner.User);
                }

                await _unitOfWork.BillCoOwnerService.AddAsync(billCoOwners);
                await _unitOfWork.BillPayorBillService.AddAsync(billPayors, bill, coOwners);
            }
            else
            {
                await _unitOfWork.BillPayorBillService.AddAsync(billPayors, bill);
            }

            // Add an if here to send and error message if the transaction fails
            await _unitOfWork.SaveAsync();

            return Ok(createBillDTO);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Bill bill) 
        {
            _unitOfWork.BillService.Update(bill);
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete([FromBody] Bill bill)
        {
            _unitOfWork.BillService.Delete(bill);
            return Ok();
        }
    }
} 
