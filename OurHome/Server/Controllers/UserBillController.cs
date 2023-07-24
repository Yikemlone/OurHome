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
        public Task<List<BillPayorBill>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<BillPayorBill> Get(int ID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/update")]
        public Task Update([FromBody] BillPayorBill payorBill) 
        {
            throw new NotImplementedException();
        }

    }
}
