using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.RepositoryServices;
using OurHome.Models.Models;

namespace OurHome.DataAccess.Services.InvitationServices
{
    public class InvitationService : RepositoryService<Invitation>, IInvitationService
    {
        public InvitationService(OurHomeDbContext context) : base(context)
        {
        }
    }
}
