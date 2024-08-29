using AutoMapper;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OurHome.Models.Models;
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
        public async Task Get_WhenUserInHome_ShouldReturn200() 
        {
            // Set up
            int id = 1;
            Bill bill = new Bill();

            Task<Bill> billTask = Task.FromResult(bill);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.BillService.GetAsync(id)).Returns(billTask);
            
            // Act
            var result = await _billController.Get(id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public async Task Get_WhenBillDoesntExist_ShouldReturn404()
        {
            // Set up
            int id = 1;
            Bill bill =null;

            Task<Bill> billTask = Task.FromResult(bill);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockUnitOfWorkService.Setup(e => e.BillService.GetAsync(id)).Returns(billTask);

            // Act
            var result = await _billController.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task Get_WhenInvalidUser_ShouldReturn401()
        {
            // Set up
            int id = 1;

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_invalidUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));

            // Act
            var result = await _billController.Get(id);

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }

        [Fact]
        public async Task Get_WhenUserNotInHome_ShouldReturn401()
        {
            // Set up
            int id = 1;
            Bill bill = new Bill();

            Task<Bill> billTask = Task.FromResult(bill);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));
            _mockUnitOfWorkService.Setup(e => e.BillService.GetAsync(id)).Returns(billTask);

            // Act
            var result = await _billController.Get(id);

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
            _mockMapper.Setup(e => e.Map<Bill>(createBillDTO.Bill)).Returns(new Bill());
            _mockMapper.Setup(e => e.Map<List<BillCoOwner>>(createBillDTO.BillCoOwners)).Returns(new List<BillCoOwner>());
            // Will need to Map bill payors at some point ex. List<User> billPayors = createBillDTO.BillPayors;

            // Act
            var result = await _billController.Add(createBillDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Add_WhenGivenValidDataAndCoOwner_ShouldReturn200()
        {
            List<BillCoOwner> billCoOwners = new()
            {
                new(),
                new()
            };

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
            _mockUnitOfWorkService.Setup(e => e.BillCoOwnerService.AddAsync(It.IsAny<BillCoOwner>())).Returns(Task.CompletedTask);
            
            _mockMapper.Setup(e => e.Map<Bill>(createBillDTO.Bill)).Returns(new Bill());
            _mockMapper.Setup(e => e.Map<List<BillCoOwner>>(createBillDTO.BillCoOwners)).Returns(billCoOwners);

            // Act
            var result = await _billController.Add(createBillDTO);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            
        }

        [Fact]
        public async Task Update_WhenValidUserAndDetails_ShouldReturn200()
        {
            // Set up
            BillDTO billDTO = new BillDTO();
            Bill bill = new Bill();

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockMapper.Setup(e => e.Map<Bill>(billDTO)).Returns(bill);
            _mockUnitOfWorkService.Setup(e => e.BillService.Update(It.IsAny<Bill>())).Verifiable();


            // Act
            var result = await _billController.Update(billDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Update_WhenInvalidUser_ShouldReturn401()
        {
            // Set up
            BillDTO billDTO = new BillDTO();
            Bill bill = new Bill();

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_invalidUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));
            _mockMapper.Setup(e => e.Map<Bill>(billDTO)).Returns(bill);
            _mockUnitOfWorkService.Setup(e => e.BillService.Update(It.IsAny<Bill>())).Verifiable();


            // Act
            var result = await _billController.Update(billDTO);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Update_WhenUserNotInHome_ShouldReturn401()
        {
            // Set up
            BillDTO billDTO = new BillDTO();
            Bill bill = new Bill();

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(false));
            _mockMapper.Setup(e => e.Map<Bill>(billDTO)).Returns(bill);
            _mockUnitOfWorkService.Setup(e => e.BillService.Update(It.IsAny<Bill>())).Verifiable();


            // Act
            var result = await _billController.Update(billDTO);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Update_WhenDoesNotOwnTheBill_ShouldReturn401()
        {
            // Set up
            BillDTO billDTO = new BillDTO();
            Bill bill = new Bill() { BillOwnerID = new Guid("{10000000-0000-0000-0000-000000000000}") };

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockMapper.Setup(e => e.Map<Bill>(billDTO)).Returns(bill);
            _mockUnitOfWorkService.Setup(e => e.BillService.Update(It.IsAny<Bill>())).Verifiable();

            // Act
            var result = await _billController.Update(billDTO);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Delete_ValidUser_ShouldReturn201()
        {

            // Set up
            BillDTO billDTO = new BillDTO();
            Bill bill = new Bill();

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockMapper.Setup(e => e.Map<Bill>(billDTO)).Returns(bill);
            _mockUnitOfWorkService.Setup(e => e.BillService.Delete(It.IsAny<Bill>())).Verifiable();

            // Act
            var result = await _billController.Delete(billDTO);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NotAValidUser_ShouldReturn401()
        {
            BillDTO billDTO = new BillDTO();
            
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_invalidUser);

            // Act
            var result = await _billController.Delete(billDTO);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Delete_UserDoesNotOwnTheBill_ShouldReturn401() 
        {

            // Set up
            BillDTO billDTO = new BillDTO();
            Bill bill = new Bill() { BillOwnerID = new Guid("{10000000-0000-0000-0000-000000000000}") };

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_validUser);
            _mockUnitOfWorkService.Setup(e => e.HomeUserService
                .IsUserInHomeAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.FromResult(true));
            _mockMapper.Setup(e => e.Map<Bill>(billDTO)).Returns(bill);
            _mockUnitOfWorkService.Setup(e => e.BillService.Delete(It.IsAny<Bill>())).Verifiable();

            // Act
            var result = await _billController.Delete(billDTO);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }

    // Test if 
                //                // TODO: Test for co-owners
                //            if (billCoOwners != null && billCoOwners.Count > 0)
                //            {
                //                List<User> coOwners = new List<User>();

                //                foreach (BillCoOwner billCoOwner in billCoOwners)
                //                { 
                //                    coOwners.Add(billCoOwner.User);
                //                }

                //await _unitOfWork.BillCoOwnerService.AddAsync(billCoOwners);
                //await _unitOfWork.BillPayorBillService.AddAsync(billPayors, bill, coOwners);
                //            }
}
