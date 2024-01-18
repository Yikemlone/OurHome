using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.UnitTests
{
    public class BillTests
    {
        private IUnitOfWorkService _unitOfWorkService;

        public BillTests()
        {
            DbContextOptions<OurHomeDbContext> options = new DbContextOptionsBuilder<OurHomeDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OurHomeDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            OurHomeDbContext context = new OurHomeDbContext(options);

            _unitOfWorkService = new UnitOfWorkService(context);
        }

        //private readonly DbContextOptions<OurHomeDbContext> _options = new DbContextOptionsBuilder<OurHomeDbContext>()
        //    .UseInMemoryDatabase(databaseName: "TestDB")
        //    .Options;

        //[Fact]
        //public async Task CreateNewBill_ShouldCreateBill()
        //{
        //    //using (var context = new OurHomeDbContext(_options))
        //    //{
        //        // Arrange
        //        Bill bill = new Bill() { BillName = "Bins" };

        //        //UnitOfWorkService unitOfWorkService = new(context);

        //        // Act
        //        await _unitOfWorkService.BillService.AddAsync(bill);
        //        await _unitOfWorkService.SaveAsync();

        //        List<Bill> bills = await _unitOfWorkService.BillService.GetAllAsync();

        //        // Assert
        //        Assert.Single(bills);

        //        // Reset DB
        //        //_context.Database.EnsureDeleted();
        //    //}
        //}

        //[Fact]
        //public async Task CreateSplitPayorsBill_ShouldSplitBillPrice()
        //{
        //    //using (var context = new OurHomeDbContext(_options))
        //    //{
        //        // Arrange
        //        Bill bill = new Bill()
        //        {
        //            BillName = "Bins",
        //            DateTime = DateTime.Now,
        //            Price = 20M,
        //            SplitBill = true,
        //        };

        //        List<User> billPayors = new List<User>()
        //        {
        //            new(),
        //            new(),
        //            new(),
        //            new(),
        //            new(),
        //        };

        //        //UnitOfWorkService unitOfWork = new(context);

        //        // Act
        //        await _unitOfWorkService.BillService.AddAsync(bill);
        //        await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
        //        await _unitOfWorkService.SaveAsync();

        //        List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

        //        // Assert
        //        Assert.Equal(bill.Price / billPayors.Count, actualBillPayors[0].UserPrice);

        //        // Reset DB
        //        //context.Database.EnsureDeleted();
        //    //}
        //}

        //[Fact]
        //public async Task CreateNonSplitPayorsBill_ShouldNotSplitBillPrice()
        //{
        //    //using (var context = new OurHomeDbContext(_options))
        //    //{
        //        // Arrange
        //        Bill bill = new Bill()
        //        {
        //            BillName = "Bins",
        //            Price = 20M,
        //            SplitBill = false,
        //        };

        //        List<User> billPayors = new List<User>()
        //        {
        //            new(),
        //            new(),
        //            new(),
        //            new(),
        //            new(),
        //        };

        //        //UnitOfWorkService unitOfWork = new(context);

        //        // Act
        //        await _unitOfWorkService.BillService.AddAsync(bill);
        //        await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
        //        await _unitOfWorkService.SaveAsync();

        //        List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

        //        // Assert
        //        Assert.Equal(bill.Price, actualBillPayors[0].UserPrice);

        //        // Reset DB
        //        //context.Database.EnsureDeleted();
        //    //}
        //}

        [Fact]
        public async Task CreateBill_ShouldCreateCorrectBillPayors()
        {
            //using (var context = new OurHomeDbContext(_options))
            //{
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill() { BillName = "Bins", BillOwner = user, Home = home };

            List<User> billPayors = new List<User>()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };

            //UnitOfWorkService unitOfWork = new(context);
            List<BillPayorBill> actualBillPayors2 = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
            await _unitOfWorkService.SaveAsync();

            List<Bill> bills = await _unitOfWorkService.BillService.GetAllAsync();
            List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

            // Assert
            Assert.Equal(billPayors.Count, actualBillPayors.Count);
            Assert.Equal(actualBillPayors[0].BillID, bills[0].ID);

            // Reset DB
            //context.Database.EnsureDeleted();
            //}
        }

        //    [Fact]
        //    public async Task CreateCoOwnerBill_ShouldSplitBillPrice()
        //    {
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            Bill bill = new Bill() { BillName = "Bins", Price = 20M };

        //            List<User> billPayors = new List<User>()
        //            {
        //                new(),
        //                new(),
        //            };

        //            List<User> billCoOwners = new()
        //            {
        //                new(),
        //                new(),
        //            };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bill);
        //            await _unitOfWorkService.BillCoOwnerService.AddAsync(billCoOwners, bill);
        //            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
        //            await _unitOfWorkService.SaveAsync();

        //            List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();
        //            decimal? expectedPrice = bill.Price / billCoOwners.Count;
        //            int expectedBillPayorsCreatedCount = billCoOwners.Count * billPayors.Count;

        //            // Assert
        //            Assert.Equal(expectedPrice, actualBillPayors[0].UserPrice);
        //            Assert.Equal(expectedBillPayorsCreatedCount, actualBillPayors.Count);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();
        //        //}
        //    }

        //    [Theory]
        //    [InlineData(30)]
        //    [InlineData(20)]
        //    [InlineData(10)]
        //    public async Task CoOwnerBillWithSplitPayors_ShouldSplitTheBillPricesCorrectly(decimal price)
        //    {
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            Bill bill = new Bill() { BillName = "Bins", Price = price, SplitBill = true };

        //            List<User> billPayors = new List<User>()
        //            {
        //                new(),
        //                new(),
        //            };

        //            List<User> billCoOwners = new()
        //            {
        //                new(),
        //                new(),
        //            };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bill);
        //            await _unitOfWorkService.BillCoOwnerService.AddAsync(billCoOwners, bill);
        //            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
        //            await _unitOfWorkService.SaveAsync();

        //            List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();
        //            decimal? expectedPrice = bill.Price / (billCoOwners.Count * billPayors.Count);
        //            int expectedBillPayorsCreatedCount = billCoOwners.Count * billPayors.Count;

        //            // Assert
        //            Assert.Equal(expectedPrice, actualBillPayors[0].UserPrice);
        //            Assert.Equal(expectedBillPayorsCreatedCount, actualBillPayors.Count);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();
        //        //}
        //    }

        //    [Fact]
        //    public async Task ReocurringBillMonthChanged_ShouldCreateNewBillWithUpdatedDate()
        //    {
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            Bill bill = new Bill()
        //            {
        //                BillName = "Bins",
        //                Reoccurring = true,
        //                DateTime = DateTime.Today.AddMonths(-1)
        //            };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            Bill newBill = _unitOfWorkService.BillService.CreateNewReocurringBill(bill);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bill);
        //            await _unitOfWorkService.BillService.AddAsync(newBill);
        //            await _unitOfWorkService.SaveAsync();

        //            // Assert
        //            Assert.Equal(DateTime.Now.Month, newBill.DateTime.Month);
        //            Assert.True(bill.ID != newBill.ID);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();
        //        //}
        //    }

        //    [Fact]
        //    public async Task DeleteBillNoPayments_ShouldDeleteTheBill()
        //    {
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            Bill bill = new Bill()
        //            {
        //                BillName = "Bins",
        //                Reoccurring = true,
        //                DateTime = DateTime.Today.AddMonths(-1)
        //            };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bill);
        //            await _unitOfWorkService.SaveAsync();
        //            _unitOfWorkService.BillService.Delete(bill);
        //            await _unitOfWorkService.SaveAsync();

        //            var bills = await _unitOfWorkService.BillService.GetAllAsync();

        //            // Assert
        //            Assert.True(bills.Count == 0);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();
        //        //}
        //    }

        //    [Fact]
        //    public async Task DeleteBillWithPayments_ShouldNotDeleteTheBill()
        //    {
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            User user = new();

        //            // Arrange
        //            Bill bill = new Bill()
        //            {
        //                BillName = "Bins",
        //                Reoccurring = true,
        //                DateTime = DateTime.Today.AddMonths(-1),
        //                BillOwner = user 
        //            };

        //            List<User> billPayors = new List<User>()
        //            {
        //                new(),
        //                new(),
        //                new(),
        //                new(),
        //                new(),
        //            };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bill);
        //            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
        //            await _unitOfWorkService.SaveAsync();

        //            var test = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

        //            test[0].Payed = true;

        //            _unitOfWorkService.BillService.Delete(bill);
        //            await _unitOfWorkService.SaveAsync();

        //            var bills = await _unitOfWorkService.BillService.GetAllAsync();

        //            // Reset DB
        //            //context.Database.EnsureDeleted();

        //            // Assert
        //            Assert.Single(bills);
        //        //}
        //    }

        //    [Fact]
        //    public async Task UpdateBillWithPayments_ShouldUpdateTheBill()
        //    {
        //        // Arrange
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            Bill bill = new Bill()
        //            {
        //                BillName = "Bins",
        //                Reoccurring = true,
        //                DateTime = DateTime.Today.AddMonths(-1)
        //            };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bill);
        //            await _unitOfWorkService.SaveAsync();

        //            bill.BillName = "Not Bins";
        //            _unitOfWorkService.BillService.Update(bill);
        //            await _unitOfWorkService.SaveAsync();

        //            var bills = await _unitOfWorkService.BillService.GetAllAsync();

        //            // Assert
        //            Assert.Equal("Not Bins", bills[0].BillName);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();
        //        //}
        //    }

        //    [Fact]
        //    public async Task GettingAllUserBill_ShouldReturnAllUsersBills()
        //    {
        //        // Arrange
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            User user = new User();

        //            Bill bins = new Bill() { BillName = "Bins", BillOwner = user };
        //            Bill electric = new Bill() { BillName = "Electric", BillOwner = user };
        //            Bill internet = new Bill() { BillName = "Bins", BillOwner = user };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bins);
        //            await _unitOfWorkService.BillService.AddAsync(electric);
        //            await _unitOfWorkService.BillService.AddAsync(internet);
        //            await _unitOfWorkService    .SaveAsync();

        //            var bills = await   _unitOfWorkService.BillService.GetAllAsync(user);

        //            // Assert
        //            Assert.Equal(3, bills.Count);
        //            //Assert.Equal(user.Id, bills[0].BillOwnerID);
        //            //Assert.Equal(user.Id, bills[1].BillOwnerID);
        //            //Assert.Equal(user.Id, bills[2].BillOwnerID);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();

        //        //}
        //    }

        //    [Fact]
        //    public async Task GettingAllUserBillInisdeAHome_ShouldReturnAllUsersBillsInsideHome()
        //    {
        //        // Arrange
        //        //using (var context = new OurHomeDbContext(_options))
        //        //{
        //            // Arrange
        //            User user = new User();
        //            Home home = new Home() { Name = "My Home", HomeOwner = user };

        //            Bill bins = new Bill() { BillName = "Bins", BillOwner = user, Home = home };
        //            Bill electric = new Bill() { BillName = "Electric", BillOwner = user, Home = home };
        //            Bill internet = new Bill() { BillName = "Bins", BillOwner = user, Home = home };

        //            //UnitOfWorkService unitOfWork = new(context);

        //            // Act
        //            await _unitOfWorkService.BillService.AddAsync(bins);
        //            await _unitOfWorkService.BillService.AddAsync(electric);
        //            await _unitOfWorkService.BillService.AddAsync(internet);
        //            await _unitOfWorkService.SaveAsync();

        //            var bills = await _unitOfWorkService.BillService.GetAllAsync(user);

        //            // Assert
        //            Assert.Equal(3, bills.Count);
        //            Assert.Equal(user.Id, bills[0].BillOwnerID);
        //            Assert.Equal(home.ID, bills[0].HomeID);

        //            // Reset DB
        //            //context.Database.EnsureDeleted();
        //        //}
        //    }
    }
}