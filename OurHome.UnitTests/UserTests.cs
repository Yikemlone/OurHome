using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.UnitTests
{
    public class UserTests
    {
        private IUnitOfWorkService _unitOfWorkService;

        public UserTests()
        {
            DbContextOptions<OurHomeDbContext> options = new DbContextOptionsBuilder<OurHomeDbContext>()
                .UseSqlServer("Server=(local)\\SQLEXPRESS;Database=OurHomeDB;TrustServerCertificate=True;MultipleActiveResultSets=True;Trusted_Connection=True;")
                .Options;

            OurHomeDbContext context = new OurHomeDbContext(options);

            _unitOfWorkService = new UnitOfWorkService(context);
        }

        [Fact]
        public async void CreateNewUser_ShouldCreateNewUser()
        {
            // Arrange
            User user = new User()
            {
                BillPayees = null,
                BillsOwned = null,
                BillsCoOwned = null,
                BillPayors = null,
                Homes = null,
                HomesOwned = null,
                HomesJoined = null,
                ReceivedInvitations = null,
                SentInvitations = null,
            };

            // Act
            await _unitOfWorkService.UserService.AddAsync(user);
            await _unitOfWorkService.SaveAsync();

            var actualUser = await _unitOfWorkService.UserService.GetAsync(user.Id);

            // Assert
            Assert.NotNull(actualUser);
        }

        //[Fact]
        //public void DeactivateUser_ShouldDeactivateUser()
        //{
        //    throw new NotImplementedException();
        //}
    }
}