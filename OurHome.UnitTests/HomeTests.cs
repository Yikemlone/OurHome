using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.UnitTests
{
    public class HomeTests
    {
        private IUnitOfWorkService _unitOfWorkService;

        public HomeTests()
        {
            DbContextOptions<OurHomeDbContext> options = new DbContextOptionsBuilder<OurHomeDbContext>()
                .UseSqlServer("Server=(local)\\SQLEXPRESS;Database=OurHomeDB;TrustServerCertificate=True;MultipleActiveResultSets=True;Trusted_Connection=True;")
                .Options;

            OurHomeDbContext context = new OurHomeDbContext(options);

            _unitOfWorkService = new UnitOfWorkService(context);
        }


        [Fact]
        public async Task CreateNewHome_ShouldCreateHome()
        {
            // Arrange
            User user = new() { UserName = "Test User" };
            Home home = new() 
            {
                Name = "Test Home", 
                HomeOwner = user, 
                HomeOwnerID = user.Id, 
                HomeBills = null,
                HomeUsers = null,
                Invitations = null,
                Users = null
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.SaveAsync();

            var actualHome = await _unitOfWorkService.HomeService.GetAsync(home.ID);

            // Assert
            Assert.Equal(home.ID, actualHome.ID);
        }

        [Fact]
        public async Task CreateNewHome_HomeCreatorShouldBeAHomeUser()
        {
            // Arrange
            User user = new() { UserName = "Test User" };
            Home home = new()
            {
                Name = "Test Home",
                HomeOwner = user,
                HomeOwnerID = user.Id,
                HomeBills = null,
                HomeUsers = null,
                Invitations = null,
                Users = null
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.HomeUserService.AddHomeOwnerAsync(user, home);
            await _unitOfWorkService.SaveAsync();

            Home actualHome = await _unitOfWorkService.HomeService.GetAsync(home.ID);

            List<HomeUser> actualHomeUsers = 
                await _unitOfWorkService.HomeUserService.GetHomeUsersByHomeIDAsync(home.ID);

            // Assert
            Assert.Equal(home.ID, actualHome.ID);
            Assert.Equal(user.Id, actualHomeUsers[0].UserID);

        }

        [Fact]
        public async void DeleteHome_ShouldDeleteHome()
        {
            // Arrange
            User user = new() { UserName = "Test User" };
            Home home = new() { Name = "Test Home", HomeOwner = user };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.SaveAsync();

            _unitOfWorkService.HomeService.Delete(home);
            await _unitOfWorkService.SaveAsync();

            var actualHome = await _unitOfWorkService.HomeService.GetAsync(home.ID);

            // Assert
            Assert.Null(actualHome);
        }

        [Fact]
        public async Task UpadateHomeName_ShouldUpdateHomeName()
        {
            // Arrange
            User user = new() { UserName = "Test User" };
            Home home = new() { Name = "Test Home", HomeOwner = user };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.SaveAsync();

            home.Name = "Updated Test Home";
            _unitOfWorkService.HomeService.Update(home);
            await _unitOfWorkService.SaveAsync();

            var actualHome = await _unitOfWorkService.HomeService.GetAsync(home.ID);

            // Assert
            Assert.Equal("Updated Test Home", actualHome.Name);
        }

        [Fact]
        public async Task AddHomeBills_ShouldAddHomeBills()
        {
            // Arrange
            User user = new() { UserName = "Test User" };
            Home home = new() { Name = "Test Home", HomeOwner = user };

            List<HomeBill> homeBills = new()
            {
                new()
                {
                    BillName = "Bins",
                    DueDate = DateTime.Now.AddMonths(1),
                    Price = 25M,
                    Home = home,
                    HomeID = home.ID,
                    PriceVaries = false
                }
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.HomeBillService.AddAsync(homeBills);
            await _unitOfWorkService.SaveAsync();

            var actualHomeBills = await _unitOfWorkService.HomeBillService
                .GetHomeBillsByHomeIDAsync(home.ID);

            // Assert
            Assert.Single(actualHomeBills);
        }

        [Fact]
        public async Task UpadateHomeBills_ShouldUpdateHomeBills()
        {
            // Arrange
            User user = new() { UserName = "Test User" };
            Home home = new() { Name = "Test Home", HomeOwner = user };

            List<HomeBill> homeBills = new()
            {
                new()
                {
                    BillName = "Bins",
                    DueDate = DateTime.Now.AddMonths(1),
                    Price = 25M,
                    Home = home
                }
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.HomeBillService.AddAsync(homeBills);
            await _unitOfWorkService.SaveAsync();

            homeBills[0].BillName = "Updated Bins";
            _unitOfWorkService.HomeBillService.Update(homeBills[0]);

            var actualHomeBills = await _unitOfWorkService.HomeBillService
                .GetAsync(homeBills[0].ID);

            // Assert
            Assert.Equal("Updated Bins", actualHomeBills.BillName);
        }

        [Fact]
        public async Task InviteCreated_ShouldCreateInvite()
        {
            // Arrange
            User sender = new();
            User receiver = new();
            Home home = new() { Name = "Test Home", HomeOwner = sender};

            Invitation invitation = new Invitation()
            {
                FromUser = sender,
                ToUser = receiver,
                Home = home,
                Status = "PENDING"
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.InvitationService.AddAsync(invitation);
            await _unitOfWorkService.SaveAsync();

            var actualInvitation = await _unitOfWorkService.InvitationService.GetAsync(invitation.ID);

            // Assert
            Assert.NotNull(actualInvitation);
        }

        [Fact]
        public async Task UserJoinsThroughInvite_ShouldJoinHome()
        {
            // Arrange
            User sender = new();
            User receiver = new();
            Home home = new() { Name = "Test Home", HomeOwner = sender};

            Invitation invitation = new Invitation()
            {
                FromUser = sender,
                FromUserID = sender.Id,
                ToUser = receiver,
                ToUserID = receiver.Id,
                HomeID = home.ID,
                Home = home,
                Status = "PENDING"
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.InvitationService.AddAsync(invitation);
            await _unitOfWorkService.SaveAsync();

            invitation.Status = "ACCEPTED";
            _unitOfWorkService.InvitationService.Update(invitation);
            await _unitOfWorkService.HomeUserService.AddAsync(invitation);
            await _unitOfWorkService.SaveAsync();

            var actualInvitation = await _unitOfWorkService.InvitationService.GetAsync(invitation.ID);
            var actualHomeUsers = await _unitOfWorkService.HomeUserService.GetHomeUsersByHomeIDAsync(home.ID);

            // Assert
            Assert.Equal("ACCEPTED", actualInvitation.Status);
            Assert.Single(actualHomeUsers);
            Assert.Equal(home.ID, actualHomeUsers[0].HomeID);
            Assert.Equal(receiver.Id, actualHomeUsers[0].UserID);
        }

        [Fact]
        public async Task UserRejectedInvite_ShouldNotJoinHome()
        {
            // Arrange
            User sender = new();
            User receiver = new();
            Home home = new() { Name = "Test Home", HomeOwner = sender};

            Invitation invitation = new Invitation()
            {
                FromUser = sender,
                ToUser = receiver,
                Home = home,
                Status = "PENDING"
            };

            // Act
            await _unitOfWorkService.HomeService.AddAsync(home);
            await _unitOfWorkService.InvitationService.AddAsync(invitation);
            await _unitOfWorkService.HomeUserService.AddAsync(invitation);
            await _unitOfWorkService.SaveAsync();

            invitation.Status = "REJECTED";
            _unitOfWorkService.InvitationService.Update(invitation);
            await _unitOfWorkService.SaveAsync();

            var homeUsers = await _unitOfWorkService.HomeUserService.GetHomeUsersByHomeIDAsync(home.ID);
            var actualInvitation = await _unitOfWorkService.InvitationService.GetAsync(invitation.ID);

            // Assert
            Assert.Equal("REJECTED", actualInvitation.Status);
            Assert.Empty(homeUsers);
             
        }
    }
}