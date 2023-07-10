using OurHome.DataAccess.Services.BillsServices;
using OurHome.Model.Models;
using OurHome.Models.Models;

namespace OurHome.UnitTests
{
    public class BillTests
    {
        private readonly IBillsService _billsService;

        private readonly DbContextOptions<OurHomeDbContext> _options = new DbContextOptionsBuilder<OurHomeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        [Fact]
        public async void CreateNewBill_ShouldCreateBill()
        {
            using (var context = new OurHomeDbContext(_options)) 
            {
                // Arrange
                User user = new();

                Home home = new Home() 
                {
                    Name = "OurHome",
                    HomeOwner = user,
                };

                Bill bill = new Bill()
                {
                    BillName = "Bins",
                    DateTime = DateTime.Now,
                    Home = home,
                    BillOwner = user,
                    Price = 20M,
                    Note = "Hello, please pay the bill for bins",
                    Reoccurring = true,
                    SplitBill =  false
                };
                
                BillService billService = new(context);

                // Act
                context.Add(bill);
                context.SaveChanges();
                List<Bill> bills = billService.GetAll().ToList();

                // Assert
                Assert.Single(bills);

                context.Database.EnsureDeleted(); // Reset Database
            }
        }
         
        [Fact]
        public void CreateSplitPayorsBill_ShouldSplitBillPrice()
        {
            using (var context = new OurHomeDbContext(_options))
            {
                // Arrange
                User ownerUser = new();

                Home home = new Home()
                {
                    Name = "OurHome",
                    HomeOwner = ownerUser,
                };

                Bill bill = new Bill()
                {
                    BillName = "Bins",
                    DateTime = DateTime.Now,
                    Home = home,
                    BillOwner = ownerUser,
                    Price = 20M,
                    Note = "Hello, please pay the bill for bins",
                    Reoccurring = true,
                    SplitBill = true,
                };

                List<BillPayor> billPayors = new List<BillPayor>() 
                {
                    new()
                    {
                        User = new(),
                        Bill = bill,
                    },
                    new()
                    {
                        User = new(),
                        Bill = bill
                    },
                    new()
                    {
                        User = new(),
                        Bill = bill
                    },
                    new()
                    {
                        User = new(),
                        Bill = bill
                    }
                };

                BillService billService = new(context);

                // Act
                context.Add(bill);
                context.SaveChanges();

                List<Bill> bills = billService.GetAll().ToList();

                // Assert
                Assert.Fail("Still needs to be implemented"); // Throws error 
                Assert.Single(bills);

                context.Database.EnsureDeleted(); // Reset Database
            }
        }

        [Fact]
        public void CreateCoOwnerBill_ShouldSplitBillPrice()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [InlineData()]
        public void CoOwnerBillWithSplitPayors_ShouldSplitTheBillPricesCorrectly() 
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ReocurringBillMonthChanged_ShouldCreateNewBillWithUpdatedDate() 
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteBillNoPayments_ShouldDeleteTheBill() 
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteBillWithPayments_ShouldNotDeleteTheBill()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void UpdateBillWithPayments_ShouldUpdateTheBill()
        {
            throw new NotImplementedException();
        }
    }
}