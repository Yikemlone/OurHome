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
        public async void CreateNewHome_ShouldCreateHome ()
        {
            // Arrange
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
        public void UpadateHomeName_ShouldUpdateHomeName()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void AddHomeBills_ShouldAddHomeBills()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UpadateHomeBills_ShouldUpdateHomeBills()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UserJoinsThroughInvite_ShouldJoinHome()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UserDoesJoinThroughInvite_ShouldNotJoinHome()
        {
            throw new NotImplementedException();
        }

    }
}