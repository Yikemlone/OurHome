using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Model.Models;
using OurHome.Models.Models;
using OurHome.Shared.DTO;

namespace OurHome.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class BillController : ControllerBase
    {
        IUnitOfWorkService _unitOfWork;

        public BillController(IUnitOfWorkService unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("all")]
        public Task<List<Bill>> GetAll() 
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<Bill> Get(int ID) 
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task Add([FromBody] CreateBillDTO createBillDTO) 
        {
            await _unitOfWork.BillService.AddAsync(createBillDTO.Bill);

            if (createBillDTO.BillCoOwners != null && createBillDTO.BillCoOwners.Count > 0) 
            {
                await _unitOfWork.BillCoOwnerService.AddAsync(createBillDTO.BillCoOwners, createBillDTO.Bill);
            }

            await _unitOfWork.BillPayorBillService.AddAsync(createBillDTO.BillPayors, createBillDTO.Bill);
            await _unitOfWork.SaveAsync();
        }

        [HttpPost]
        [Route("update")]
        public void Update([FromBody] Bill bill) 
        {
            _unitOfWork.BillService.Update(bill);
        }

        [HttpPost]
        [Route("delete")]
        public void Delete([FromBody] Bill bill)
        {
            _unitOfWork.BillService.Delete(bill);
        }
        
    }
} 
