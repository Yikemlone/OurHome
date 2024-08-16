using AutoMapper;
using OurHome.Models.Models;

namespace OurHome.Shared.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Bill, BillDTO>();
            CreateMap<BillDTO, Bill>();
        }
    }
}
