﻿using Microsoft.AspNetCore.Mvc;
using OurHome.Models.Models;

namespace OurHome.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BillPayorController : ControllerBase
    {
        /// <summary>
        /// Returns all the bills the user needs to pay for
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("all")]
        public Task<List<BillPayorBill>> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a bill by ID that the user needs to pay for
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("{ID}")]
        public Task<BillPayorBill> Get(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the bill payor bill with the new information
        /// </summary>
        /// <param name="payorBill"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("/update")]
        public Task Update([FromBody] BillPayorBill payorBill) 
        {
            throw new NotImplementedException();
        }

    }
}
