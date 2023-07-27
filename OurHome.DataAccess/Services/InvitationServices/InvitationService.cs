using FlashCardBlazorApp.DataAccess.Services.RepositoryService;
using OurHome.DataAccess.Context;
using OurHome.Model.Models;

namespace OurHome.DataAccess.Services.InvitationServices
{
    public class InvitationService : RepositoryService<Invitation>, IInvitationService
    {
        public InvitationService(OurHomeDbContext context) : base(context)
        {
        }
    }
}
