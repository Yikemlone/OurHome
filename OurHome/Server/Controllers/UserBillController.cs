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
        public Task<List<BillPayor>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<BillPayor> Get(int ID)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/update")]
        public Task Update([FromBody] BillPayor payorBill) 
        {
            throw new NotImplementedException();
        }

    }
}
