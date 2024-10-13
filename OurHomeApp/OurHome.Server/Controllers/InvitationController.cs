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
    public class InvitationController : ControllerBase
    {
        IUnitOfWorkService _unitOfWork;
        private readonly UserManager<User> _userManager;

        public InvitationController(IUnitOfWorkService unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Invitation>>> GetAll() 
        {
            User? user = await _userManager.
                FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null) return NotFound();

            //List<Bill> bills = await _unitOfWork.InvitationService.GetAllAsync(user);
            //return Ok(bills);
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<ActionResult<Invitation>> Get(int ID) 
        {
            Invitation bill = await _unitOfWork.InvitationService.GetAsync(ID);
            if (bill == null) return NotFound();
            return Ok(bill);
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] CreateBillDTO createBillDTO) 
        {
            //return Ok();
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Invitation invitation) 
        {
            _unitOfWork.InvitationService.Update(invitation);
            //return Ok();
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete([FromBody] Invitation invitation)
        {
            _unitOfWork.InvitationService.Delete(invitation);
            //return Ok();
            throw new NotImplementedException();
        }
    }
} 
