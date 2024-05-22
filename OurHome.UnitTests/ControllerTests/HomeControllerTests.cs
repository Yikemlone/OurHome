using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurHome.Server.Controllers;

namespace OurHome.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        private Mock<IUnitOfWorkService> _unitOfWorkService;
        private Mock<UserManager<User>> _userManager;
        private Fixture _fixture;
        private HomeController _homeController;
        private Mock<IUserStore<User>> _userStore;

        public HomeControllerTests()
        {
            _fixture = new Fixture();
            _unitOfWorkService = new Mock<IUnitOfWorkService>();
            _userManager = new Mock<UserManager<User>>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUserHomes()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });

            // Arrange
            var user = new User()
            {
                Id = new Guid()
            };

            var home = new Home
            {
                HomeOwnerID = user.Id
            };

            _unitOfWorkService.Setup(x => x.HomeService.GetAllAsync(user)).ReturnsAsync(new List<Home> { home });
            _homeController = new HomeController(_unitOfWorkService.Object, _userManager.Object);

            // Act
            var result = await _homeController.GetAll();
            OkResult? objectResult = result.Result as OkResult;

            // Assert
            Assert.Equal(200, objectResult.StatusCode);
        }

        //// Arrange
        //var store = new Mock<IUserStore<IdentityUser>>();
        //store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
        //    .ReturnsAsync(new IdentityUser()
        //{
        //    UserName = "test@email.com",
        //        Id = "123"
        //    });

        //var mgr = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);

        //var controller = new SweetController(mgr);

        //// Act
        //var result = await controller.GetUser("123");

        //// Assert
        //Assert.NotNull(result);
        //Assert.Equal("123", result.Id);
    }
}
