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

        public HomeControllerTests()
        {
            _fixture = new Fixture();
            _unitOfWorkService = new Mock<IUnitOfWorkService>();
            _userManager = new Mock<UserManager<User>>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllBills()
        {
            // Arrange
            var home = new Home();

            _unitOfWorkService.Setup(x => x.HomeService.GetAllAsync()).ReturnsAsync(new List<Home> { home });

            _homeController = new HomeController(_unitOfWorkService.Object);
            var result = await _homeController.GetAll();
            OkResult? objectResult = result.Result as OkResult;

            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
