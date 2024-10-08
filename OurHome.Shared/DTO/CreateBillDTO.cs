﻿using OurHome.Models.Models;

namespace OurHome.Shared.DTO
{
    public class CreateBillDTO
    {
        public BillDTO Bill { get; set; }
        public List<User> BillPayors { get; set; }
        public List<BillCoOwnerDTO>? BillCoOwners { get; set; }
    }
}