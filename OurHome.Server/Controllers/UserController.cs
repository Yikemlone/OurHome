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
    public class UserController : ControllerBase
    {
        IUnitOfWorkService _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserController(IUnitOfWorkService unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update([FromBody] Bill bill) 
        {
            _unitOfWork.BillService.Update(bill);
            //return Ok();
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete([FromBody] Bill bill)
        {
            _unitOfWork.BillService.Delete(bill);
            //return Ok();
            throw new NotImplementedException();
        }
    }
} 
