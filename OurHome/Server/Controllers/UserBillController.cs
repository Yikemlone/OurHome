using Microsoft.AspNetCore.Mvc;
using OurHome.Models.Models;

namespace OurHome.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserBillController : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        public Task<List<Bill>> GetAllUserBills()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<Bill> GetUserBill()
        {
            throw new NotImplementedException();
        }

    }
}
