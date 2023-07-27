using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;

namespace OurHome.DataAccess.Services.InvitationServices
{
    public class Invitation : RepositoryService<Invitation>, IInvitationService
    {
        public Invitation(OurHomeDbContext context) : base(context)
        {
        }
    }
}
