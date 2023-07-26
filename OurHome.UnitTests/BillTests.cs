using OurHome.DataAccess.Services.UnitOfWorkServices;
using OurHome.Model.Models;
using OurHome.Models.Models;
using System.Diagnostics;

namespace OurHome.UnitTests
{
    public class BillTests
    {
        private readonly DbContextOptions<OurHomeDbContext> _options = new DbContextOptionsBuilder<OurHomeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        [Fact]
        public async void CreateNewBill_ShouldCreateBill()
        {
            using (var context = new OurHomeDbContext(_options)) 
            {
                // Arrange
                Bill bill = new Bill() { BillName = "Bins"};

                UnitOfWorkService unitOfWorkService = new(context);

                // Act
                await unitOfWorkService.BillService.AddAsync(bill);
                await unitOfWorkService.Save();

                List<Bill> bills = await unitOfWorkService.BillService.GetAllAsync();

                // Assert
                Assert.Single(bills);

                // Reset DB
                context.Database.EnsureDeleted(); 
            }
        }
         
        [Fact]
        public async void CreateSplitPayorsBill_ShouldSplitBillPrice()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill()
                {
                    BillName = "Bins",
                    DateTime = DateTime.Now,
                    Price = 20M,
                    SplitBill = true,
                };

                List<User> billPayors = new List<User>() 
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillPayorBillService.AddAsync(billPayors, bill);
                await unitOfWork.Save();

                List<BillPayorBill> actualBillPayors = await unitOfWork.BillPayorBillService.GetAllAsync();

                // Assert
                Assert.Equal(bill.Price/billPayors.Count, actualBillPayors[0].UserPrice);

                // Reset DB
                context.Database.EnsureDeleted(); 
            }
        }

        [Fact]
        public async void CreateNonSplitPayorsBill_ShouldNotSplitBillPrice()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill()
                {
                    BillName = "Bins",
                    Price = 20M,
                    SplitBill = false,
                };

                List<User> billPayors = new List<User>()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillPayorBillService.AddAsync(billPayors, bill);
                await unitOfWork.Save();

                List<BillPayorBill> actualBillPayors = await unitOfWork.BillPayorBillService.GetAllAsync();

                // Assert
                Assert.Equal(bill.Price, actualBillPayors[0].UserPrice);

                // Reset DB
                context.Database.EnsureDeleted(); 
            }
        }

        [Fact]
        public async void CreateBill_ShouldCreateCorrectBillPayors()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill(){ BillName = "Bins" };

                List<User> billPayors = new List<User>()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillPayorBillService.AddAsync(billPayors, bill);
                await unitOfWork.Save();

                List<Bill> bills = await unitOfWork.BillService.GetAllAsync();
                List<BillPayorBill> actualBillPayors = await unitOfWork.BillPayorBillService.GetAllAsync();

                // Assert
                Assert.Equal(billPayors.Count, actualBillPayors.Count);
                Assert.Equal(actualBillPayors[0].BillID, bills[0].ID);

                // Reset DB
                context.Database.EnsureDeleted(); 
            }
        }

        [Fact]
        public async void CreateCoOwnerBill_ShouldSplitBillPrice()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill() { BillName = "Bins", Price = 20M };

                List<User> billPayors = new List<User>()
                {
                    new(),
                    new(),
                };

                List<User> billCoOwners = new() 
                { 
                    new(),
                    new(),
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillCoOwnerService.AddAsync(billCoOwners, bill);
                await unitOfWork.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
                await unitOfWork.Save();

                List<BillPayorBill> actualBillPayors = await unitOfWork.BillPayorBillService.GetAllAsync();
                decimal? expectedPrice = bill.Price / billCoOwners.Count;
                int expectedBillPayorsCreatedCount = billCoOwners.Count * billPayors.Count;

                // Assert
                Assert.Equal(expectedPrice, actualBillPayors[0].UserPrice);
                Assert.Equal(expectedBillPayorsCreatedCount, actualBillPayors.Count);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Theory]
        [InlineData(30)]
        [InlineData(20)]
        [InlineData(10)]
        public async void CoOwnerBillWithSplitPayors_ShouldSplitTheBillPricesCorrectly(decimal price) 
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill() { BillName = "Bins", Price = price, SplitBill = true };

                List<User> billPayors = new List<User>()
                {
                    new(),
                    new(),
                };

                List<User> billCoOwners = new()
                {
                    new(),
                    new(),
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillCoOwnerService.AddAsync(billCoOwners, bill);
                await unitOfWork.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
                await unitOfWork.Save();

                List<BillPayorBill> actualBillPayors = await unitOfWork.BillPayorBillService.GetAllAsync();
                decimal? expectedPrice = bill.Price / (billCoOwners.Count * billPayors.Count);
                int expectedBillPayorsCreatedCount = billCoOwners.Count * billPayors.Count;

                // Assert
                Assert.Equal(expectedPrice, actualBillPayors[0].UserPrice);
                Assert.Equal(expectedBillPayorsCreatedCount, actualBillPayors.Count);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void ReocurringBillMonthChanged_ShouldCreateNewBillWithUpdatedDate()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill() 
                { 
                    BillName = "Bins", 
                    Reoccurring = true, 
                    DateTime = DateTime.Today.AddMonths(-1) 
                };

                UnitOfWorkService unitOfWork = new(context);

                Bill newBill = unitOfWork.BillService.CreateNewReocurringBill(bill);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillService.AddAsync(newBill);
                await unitOfWork.Save();

                // Assert
                Assert.Equal(DateTime.Now.Month, newBill.DateTime.Month);
                Assert.True(bill.ID != newBill.ID);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void DeleteBillNoPayments_ShouldDeleteTheBill() 
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill()
                {
                    BillName = "Bins",
                    Reoccurring = true,
                    DateTime = DateTime.Today.AddMonths(-1)
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.Save();
                unitOfWork.BillService.Delete(bill);
                await unitOfWork.Save();

                var bills = await unitOfWork.BillService.GetAllAsync();

                // Assert
                Assert.True(bills.Count == 0);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async void DeleteBillWithPayments_ShouldNotDeleteTheBill()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                Bill bill = new Bill()
                {
                    BillName = "Bins",
                    Reoccurring = true,
                    DateTime = DateTime.Today.AddMonths(-1)
                };

                List<User> billPayors = new List<User>()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(),
                };

                UnitOfWorkService unitOfWork = new(context);

                // Act
                await unitOfWork.BillService.AddAsync(bill);
                await unitOfWork.BillPayorBillService.AddAsync(billPayors, bill);
                await unitOfWork.Save();
                unitOfWork.BillService.Delete(bill);
                await unitOfWork.Save();

                var bills = await unitOfWork.BillService.GetAllAsync();

                // Assert
                Assert.Single(bills);

                // Reset DB
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void UpdateBillWithPayments_ShouldUpdateTheBill()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GettingAllUserBill_ShouldReturnAllUsersBills()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GettingAllUserBillInisdeAHome_ShouldReturnAllUsersBillsInsideHome()
        {
            throw new NotImplementedException();
        }
    }
}