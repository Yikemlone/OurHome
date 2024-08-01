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
        private Mock<IUnitOfWorkService> _mockUnitOfWorkService;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<IUserStore<User>> _mockUserStore;
        private Mock<ControllerContext> _mockControllerContext;

        private HomeController _homeController;

        private readonly User _user = new User() { UserName = "Thomas"};

        public HomeControllerTests()
        {
            _mockUnitOfWorkService = new Mock<IUnitOfWorkService>();

            _mockUserStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object, null, null, null, null, null, null, null, null);
            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);

            _mockControllerContext = new Mock<ControllerContext>();

            HttpContext httpContext = new DefaultHttpContext() 
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "this-is-a-valid-id"),
                    new Claim(ClaimTypes.Name, "Thomas"),
                }))
            };

            _mockControllerContext.Object.HttpContext = httpContext;
            _homeController = new HomeController(_mockUnitOfWorkService.Object, _mockUserManager.Object);
            _homeController.ControllerContext = _mockControllerContext.Object;
        }

        [Fact]
        public async Task Add_GivenValidData_ShouldReturnStatus201() 
        { 
            // Arrange
            HomeDTO homeDTO = new();
            homeDTO.Name = "TestHome";
            homeDTO.HomeOwnerID = new Guid();

            Home home = new();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;

            Task<Home> homeTask = Task.FromResult(home);

            _mockUnitOfWorkService.Setup(e => e.HomeService.AddAsync(home)).Returns(homeTask);

            // Act
            var result = await _homeController.Add(homeDTO);
            CreatedResult? objectResult = result as CreatedResult;

            // Assert
            Assert.Equal(201, objectResult.StatusCode);
        }

        [Fact]
        public async Task Add_GivenInvalidData_ShouldReturnStatus400()
        {
            // Arrange
            HomeDTO homeDTO = new();
            homeDTO.Name = null;
            homeDTO.HomeOwnerID = new Guid();

            // Act
            var result = await _homeController.Add(homeDTO);
            BadRequestObjectResult? objectResult = result as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAll_WhenGettingAllHomes_ShouldReturnStatus200()
        {
            // Arrange
            Home home = new() { Name = "Test", HomeOwnerID = new Guid()};
            List<Home> homes = new(){ home };

            Task<List<Home>> homesTask = Task.FromResult(homes);
            
            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAllAsync(It.IsAny<User>())).Returns(homesTask);

            // Act
            ActionResult<List<Home>> result = await _homeController.GetAll();
            OkObjectResult? objectResult = result.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, objectResult.StatusCode);
        }


        [Fact]
        public async Task Get_WhenGettingHomeByID_ShouldReturnStatus200()
        {
            // Arrange
            Home home = new() { Name = "Test", HomeOwnerID = new Guid()};

            Task<Home> homeTask = Task.FromResult(home);

            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAsync(It.IsAny<int>())).Returns(homeTask);

            // Act
            ActionResult<Home> result = await _homeController.Get(1);
            OkObjectResult? objectResult = result.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task Get_WhenGettingInvalidHomeID_ShouldReturnStatus404()
        {
            // Arrange
            Task<Home> homeTask = Task.FromResult<Home>(null);

            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAsync(It.IsAny<int>())).Returns(homeTask);

            // Act
            ActionResult<Home> result = await _homeController.Get(1);
            NotFoundResult? objectResult = result.Result as NotFoundResult;

            // Assert
            Assert.Equal(404, objectResult.StatusCode);
        }
        

        // TODO 
        // - Add test for update but 200 response
        // - Add test for update but 400 bad request
        // - Add test for delete but 204 response
        // - Add test for delete but 403 unauthorized
    }
}
