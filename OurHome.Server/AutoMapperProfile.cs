using AutoMapper;
using OurHome.Models.Models;

namespace OurHome.Shared.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BillCoOwner, BillCoOwnerDTO>();
            CreateMap<BillCoOwnerDTO, BillCoOwner>();

            CreateMap<Bill, BillDTO>();
            CreateMap<BillDTO, Bill>();

            CreateMap<BillPayorBill, BillPayorBillDTO>();
            CreateMap<BillPayorBillDTO, BillPayorBill>();

            CreateMap<Home, HomeDTO>();
            CreateMap<HomeDTO, Home>();

            CreateMap<HomeUser, HomeUserDTO>();
            CreateMap<HomeUserDTO, HomeUser>();

            CreateMap<HomeBill, HomeBillDTO>();
            CreateMap<HomeBillDTO, HomeBill>();

            CreateMap<HomeUserDTO, HomeUser>();
            CreateMap<HomeUser, HomeUserDTO>();

            CreateMap<Invitation, InvitationDTO>();
            CreateMap<InvitationDTO, Invitation>();
        }
    }
}
