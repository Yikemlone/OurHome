using AutoFixture;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OurHome.Server.Controllers;
using OurHome.Shared.DTO;
using System.Net.Sockets;
using System.Security.Claims;

namespace OurHome.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        private Mock<IUnitOfWorkService> MockUnitOfWorkService;
        private Mock<UserManager<User>> MockUserManager;
        private Mock<IUserStore<User>> MockUserStore;
        private Mock<HomeController> MockHomeController;

        private HomeController _homeController;

        private readonly User user = new User() { UserName = "Thomas"};

        public HomeControllerTests()
        {
            MockUnitOfWorkService = new Mock<IUnitOfWorkService>();
            MockUserStore = new Mock<IUserStore<User>>();
            MockUserManager = new Mock<UserManager<User>>(MockUserStore.Object, null, null, null, null, null, null, null, null);

            MockUserManager.Setup(m => m.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            MockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            MockUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            MockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

            MockHomeController = new Mock<HomeController>();

            _homeController = new HomeController(MockUnitOfWorkService.Object, MockUserManager.Object);
        }


        [Fact]
        public async Task AddValidHome_ShouldReturnStatus201() 
        { 
            // Arrange
            HomeDTO homeDTO = new();
            homeDTO.Name = "TestHome";
            homeDTO.HomeOwnerID = new Guid();

            Home home = new();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            Task<Home> homeTask = Task.FromResult(home);

            MockUnitOfWorkService.Setup(e => e.HomeService.AddAsync(home)).Returns(homeTask);

            // Act
            var result = await _homeController.Add(homeDTO);
            CreatedResult? objectResult = result as CreatedResult;

            // Assert
            Assert.Equal(201, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetAll_ShouldReturnStatus200()
        {
            // Arrange
            Home home = new();
            home.Name = "Test";
            home.HomeOwnerID = new Guid();
            List<Home> homes = new()
            {
                home
            };

            Task<List<Home>> homesTask = Task.FromResult(homes);
            Task<User> userTask = Task.FromResult(user);

            MockUnitOfWorkService.Setup(e => e.HomeService.GetAllAsync(It.IsAny<User>())).Returns(homesTask);
            MockHomeController.Setup(m => m.GetUser()).Returns(userTask);

            // Act
            ActionResult<List<Home>> result = await _homeController.GetAll();
            OkObjectResult? objectResult = result.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, objectResult.StatusCode);
        }

        // TODO 
        // - Add test for get all but 404
        // - Add test for for get 200
        // - Add test for get by ID but 404
        // - Add test for add but 400 bad request
        // - Add test for update but 200 response
        // - Add test for update but 400 bad request
        // - Add test for delete but 204 response
        // - Add test for delete but 403 unauthorized
    }


}
