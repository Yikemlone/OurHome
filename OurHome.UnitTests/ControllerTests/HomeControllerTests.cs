using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OurHome.Server.Controllers;

namespace OurHome.UnitTests.ControllerTests
{
    // Ensure proper status codes and responses are returned

    public class HomeControllerTests
    {
        private Mock<IUnitOfWorkService> _unitOfWorkService;
        private HomeController _homeController;

        public HomeControllerTests()
        {
            _unitOfWorkService = new Mock<IUnitOfWorkService>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUserHomes()
        {
            // Arrange


            // Act
            var result = await _homeController.GetAll();
            OkResult? objectResult = result.Result as OkResult;

            // Assert
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
