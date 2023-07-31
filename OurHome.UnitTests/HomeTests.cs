using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.UnitTests
{
    public class HomeTests
    {
        private readonly DbContextOptions<OurHomeDbContext> _options = new DbContextOptionsBuilder<OurHomeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        [Fact]
        public async Task CreateNewHome_ShouldCreateHome()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Home home = new() { Name = "Test Home" };
                
                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.SaveAsync();

                var homes = await unitOfWork.HomeService.GetAllAsync();

                // Assert
                Assert.Single(homes);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void DeleteHome_ShouldCascadeDeleteWithHome()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task UpadateHomeName_ShouldUpdateHomeName()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Home home = new() { Name = "Test Home" };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.SaveAsync();

                home.Name = "Updated Test Home";
                unitOfWork.HomeService.Update(home);
                await unitOfWork.SaveAsync();

                var homes = await unitOfWork.HomeService.GetAllAsync();

                // Assert
                Assert.Equal("Updated Test Home", homes[0].Name);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task AddHomeBills_ShouldAddHomeBills()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Home home = new() { Name = "Test Home" };

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

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.HomeBillService.AddAsync(homeBills);
                await unitOfWork.SaveAsync();

                var homes = await unitOfWork.HomeBillService.GetAllAsync();

                // Assert
                Assert.Single(homes);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task UpadateHomeBills_ShouldUpdateHomeBills()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Home home = new() { Name = "Test Home" };

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

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.HomeBillService.AddAsync(homeBills);
                await unitOfWork.SaveAsync();

                var homes = await unitOfWork.HomeService.GetAllAsync();

                // Assert
                Assert.Single(homes);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task InviteCreated_ShouldCreateInvite()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                User sender = new();
                User receiver = new();

                Home home = new() { Name = "Test Home" };

                Invitation invitation = new Invitation()
                {
                    FromUser = sender,
                    ToUser = receiver,
                    Home = home,
                    Status = "Accepted"
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.InvitationService.AddAsync(invitation);
                await unitOfWork.SaveAsync();

                var invitations = await unitOfWork.InvitationService.GetAllAsync();

                // Assert
                Assert.Single(invitations);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task UserJoinsThroughInvite_ShouldJoinHome()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                User sender = new();
                User receiver = new();

                Home home = new() { Name = "Test Home" };

                Invitation invitation = new Invitation()
                {
                    FromUser = sender,
                    ToUser = receiver,
                    Home = home,
                    Status = "ACCEPTED"
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.InvitationService.AddAsync(invitation);
                await unitOfWork.HomeUserService.AddAsync(invitation);

                await unitOfWork.SaveAsync();

                var homeUsers = await unitOfWork.HomeUserService.GetAllAsync();

                // Assert
                Assert.Single(homeUsers);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task UserDoesJoinThroughInvite_ShouldNotJoinHome()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                User sender = new();
                User receiver = new();

                Home home = new() { Name = "Test Home" };

                Invitation invitation = new Invitation()
                {
                    FromUser = sender,
                    ToUser = receiver,
                    Home = home,
                    Status = "REJECTED"
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.HomeService.AddAsync(home);
                await unitOfWork.InvitationService.AddAsync(invitation);
                await unitOfWork.HomeUserService.AddAsync(invitation);
                await unitOfWork.SaveAsync();

                var homeUsers = await unitOfWork.HomeUserService.GetAllAsync();

                // Assert
                Assert.Empty(homeUsers);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }
    }
}