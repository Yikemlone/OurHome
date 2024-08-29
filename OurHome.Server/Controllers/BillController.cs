using AutoMapper;
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
        IMapper _mapper;

        public BillController(IUnitOfWorkService unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
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

            if (user == null) return Unauthorized();

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
            // Check if the user is in the home?

            Bill bill = await _unitOfWork.BillService.GetAsync(ID);
            if (bill == null) return Unauthorized();                                                                                                             
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

            // Mappers aren't mapping
            Bill bill = _mapper.Map<Bill>(createBillDTO.Bill);
            List<User> billPayors = createBillDTO.BillPayors;
            List<BillCoOwner>? billCoOwners = _mapper.Map<List<BillCoOwner>>(createBillDTO.BillCoOwners);

            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool isUserInHome = await _unitOfWork.HomeUserService
                .IsUserInHomeAsync(user, bill.HomeID);
            if (!isUserInHome) return Unauthorized();

            await _unitOfWork.BillService.AddAsync(bill);

            // TODO: Test for co-owners
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

            await _unitOfWork.SaveAsync();

            return Ok(createBillDTO);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] Bill bill) 
        {
            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            bool isUserInHome = await _unitOfWork.HomeUserService.IsUserInHomeAsync(user, bill.HomeID);

            if (user == null) return Unauthorized();

            if(!isUserInHome) return Unauthorized(); // Is this needed? Do we want users to
            // modify bills of homes they are not in? Even if they own it?

            if (bill.BillOwnerID != user.Id) return Unauthorized(); 

            _unitOfWork.BillService.Update(bill);
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Delete([FromBody] Bill bill)
        {
            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(bill.BillOwnerID != user.Id) return Unauthorized();

            _unitOfWork.BillService.Delete(bill);
            return NoContent();
        }
    }
} 
