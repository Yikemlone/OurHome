using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurHome.Server.Controllers;
using OurHome.Shared.DTO;
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

        private readonly User _user = new User() { UserName = "Jerry"};
        private readonly User _user2 = null;

        public HomeControllerTests()
        {
            _mockUnitOfWorkService = new Mock<IUnitOfWorkService>();

            _mockUserStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object, null, null, null, null, null, null, null, null);

            _mockControllerContext = new Mock<ControllerContext>();

            HttpContext httpContext = new DefaultHttpContext() 
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("ID", "this-is-a-valid-id"),
                    new Claim("Name", "Thomas"),
                    new Claim("User", "User"),
                    new Claim("HomeAdmin", "Admin")
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

            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
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

            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAsync(It.IsAny<int>())).Returns(homeTask);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));

            // Act
            ActionResult<Home> result = await _homeController.Get(1);
            OkObjectResult? objectResult = result.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task Get_WhenGettingHomeByID_ShouldReturnStatus401()
        {
            // Arrange
            Home home = new() { Name = "Test", HomeOwnerID = new Guid() };

            Task<Home> homeTask = Task.FromResult(home);

            _mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAsync(It.IsAny<int>())).Returns(homeTask);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));

            // Act
            ActionResult<Home> result = await _homeController.Get(1);
            UnauthorizedResult? objectResult = result.Result as UnauthorizedResult;

            // Assert
            Assert.Equal(401, objectResult.StatusCode);
        }

        [Fact]
        public async Task Get_WhenNotLoggedInGettingHomeByID_ShouldReturnStatus401()
        {
            // Arrange
            Home home = new() { Name = "Test", HomeOwnerID = new Guid() };

            Task<Home> homeTask = Task.FromResult(home);

            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user2);
            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAsync(It.IsAny<int>())).Returns(homeTask);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));

            // Act
            ActionResult<Home> result = await _homeController.Get(1);
            StatusCodeResult? objectResult = result.Result as StatusCodeResult;

            // Assert
            Assert.Equal(401, objectResult.StatusCode);
        }


        [Fact]
        public async Task Get_WhenGettingInvalidHomeID_ShouldReturnStatus404()
        {
            // Arrange
            Task<Home> homeTask = Task.FromResult<Home>(null);
             
            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.HomeService.GetAsync(It.IsAny<int>())).Returns(homeTask);

            // Act
            ActionResult<Home> result = await _homeController.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Update_WhenUpdatingHomeAsAdmin_ShouldReturnStatus200()
        {
            // Arrange
            HomeDTO homeDTO = new();
            homeDTO.Name = "TestHome";
            homeDTO.HomeOwnerID = new Guid();

            Home home = new();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;


            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.HomeService.Update(home)).Verifiable();

            // Act
            var result = await _homeController.Update(homeDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_WhenUpdatingHomeAsHomeOwner_ShouldReturnStatus200()
        {
            // Arrange
            HomeDTO homeDTO = new();
            homeDTO.Name = "TestHome";
            homeDTO.HomeOwnerID = new Guid();

            Home home = new();
            home.Name = homeDTO.Name;
            home.HomeOwnerID = homeDTO.HomeOwnerID;


            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.HomeService.Update(home)).Verifiable();


            HttpContext httpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("HomeOwner", "homeOwner")
                }))
            };

            _mockControllerContext.Object.HttpContext = httpContext;

            // Act
            var result = await _homeController.Update(homeDTO);
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public async Task Update_WhenUpdatingHomeWithInvalidData_ShouldReturnStatus400()
        {
            // Arrange
            HomeDTO homeDTO = new();
            homeDTO.Name = null;
            homeDTO.HomeOwnerID = new Guid();

            // Act
            var result = await _homeController.Update(homeDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_WhenDeletingHomeAsHomeOwner_ShouldReturnStatus204()
        {
            // Arrange
            Home home = new();
            home.Name = "TestHome";
            home.HomeOwnerID = new Guid();

            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.HomeService.Delete(home)).Verifiable();

            HttpContext httpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim("HomeOwner", "homeOwner")
                }))
            };

            _mockControllerContext.Object.HttpContext = httpContext;

            // Act
            var result = await _homeController.Delete(1);
            NoContentResult? objectResult = result as NoContentResult;

            // Assert
            Assert.Equal(204, objectResult.StatusCode);
        }


        [Fact]
        public async Task Delete_WhenDeletingHomeWhileNotBeingInIt_ShouldReturnStatus401()
        {
            // Arrange
            Home home = new();
            home.Name = "TestHome";
            home.HomeOwnerID = new Guid();

            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                           .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));

            // Act
            var result = await _homeController.Delete(1);
            UnauthorizedResult? objectResult = result as UnauthorizedResult;

            // Assert
            Assert.Equal(401, objectResult.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenDeletingHomeAsAdmin_ShouldReturnStatus401()
        {
            // Arrange
            Home home = new();
            home.Name = "TestHome";
            home.HomeOwnerID = new Guid();

            _mockUserManager.Setup(e => e.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user2);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                           .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));
            // Act
            var result = await _homeController.Delete(1);
            UnauthorizedResult? objectResult = result as UnauthorizedResult;

            // Assert
            Assert.Equal(401, objectResult.StatusCode);
        }
    }
}
