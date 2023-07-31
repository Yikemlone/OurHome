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
    public class BillController : ControllerBase
    {
        IUnitOfWorkService _unitOfWork;
        private readonly UserManager<User> _userManager;

        public BillController(IUnitOfWorkService unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Bill>>> GetAll() 
        {
            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null) return NotFound();

            List<Bill> bills = await _unitOfWork.BillService.GetAllAsync(user);

            return Ok(bills);
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<ActionResult<Bill>> Get(int ID) 
        {
            Bill bill = await _unitOfWork.BillService.GetAsync(ID);

            if (bill == null) return NotFound();

            return Ok(bill);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] CreateBillDTO createBillDTO) 
        {
            var bill = createBillDTO.Bill;
            var billCoOwners = createBillDTO.BillCoOwners;
            var billPayors = createBillDTO.BillPayors;

            await _unitOfWork.BillService.AddAsync(bill);

            if (billCoOwners != null && billCoOwners.Count > 0)
            {
                await _unitOfWork.BillCoOwnerService.AddAsync(billCoOwners, bill);
                await _unitOfWork.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
            }
            else 
            {
                await _unitOfWork.BillPayorBillService.AddAsync(billPayors, bill);
            }

            await _unitOfWork.SaveAsync();

            return Ok();
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
