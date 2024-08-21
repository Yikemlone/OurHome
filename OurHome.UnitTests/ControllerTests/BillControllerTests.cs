using AutoMapper;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurHome.Server.Controllers;
using OurHome.Shared.DTO;
using System.Security.Claims;

namespace OurHome.UnitTests.ControllerTests
{
    public class BillControllerTests
    {

        private Mock<IUnitOfWorkService> _mockUnitOfWorkService;

        private Mock<UserManager<User>> _mockUserManager;
        private Mock<IUserStore<User>> _mockUserStore;
        private Mock<ControllerContext> _mockControllerContext;
        private Mock<IMapper> _mockMapper;
        private BillController _billController;

        private readonly User _validUser = new User() { UserName = "Jerry" };
        private readonly User _invalidUser = null;

        public BillControllerTests()
        {
            _mockUnitOfWorkService = new Mock<IUnitOfWorkService>();

            _mockUserStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object, null, null, null, null, null, null, null, null);
            _mockMapper = new Mock<IMapper>();

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
            _billController = new BillController(_mockUnitOfWorkService.Object, _mockUserManager.Object, _mockMapper.Object);
            _billController.ControllerContext = _mockControllerContext.Object;
        }

        [Fact]
        public async Task GetAll_WhenValidUser_ShouldReturn200()
        {
            // Set up
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(x => x.BillService.GetAllAsync(_validUser)).ReturnsAsync(new List<Bill>());

            // Act
            var result = await _billController.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public async Task GetAll_WhenInvalidUser_ShouldReturn401()
        {
            // Set up
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_invalidUser);

            // Act
            var result = await _billController.GetAll();

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }


        [Fact]
        public async Task Add_WhenGivenValidData_ShouldReturn200()
        {
            // Set up
            CreateBillDTO createBillDTO = new CreateBillDTO()
            {
                Bill = new BillDTO(),
                BillPayors = new List<User>(),
                BillCoOwners = new List<BillCoOwnerDTO>()
            };

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.BillService.AddAsync(It.IsAny<Bill>())).Returns(Task.CompletedTask);
            _mockUnitOfWorkService.Setup(e => e.BillPayorBillService.AddAsync(It.IsAny<BillPayorBill>())).Returns(Task.CompletedTask);

            // Act
            var result = await _billController.Add(createBillDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Update bill tests
        // Delete bill tests

        [Fact]
        public async Task Update_WhenValidUserAndDetails_ShouldUpdateBill() 
        {
            // Set up

            // Act
            
            // Assert
        }

    }
}
