namespace OurHome.UnitTests.ServiceTests
{
    public class BillTests
    {
        private IUnitOfWorkService _unitOfWorkService;

        public BillTests()
        {
            DbContextOptions<OurHomeDbContext> options = new DbContextOptionsBuilder<OurHomeDbContext>()
                .UseSqlServer("Server=(local)\\SQLEXPRESS;Database=OurHomeDBTest;TrustServerCertificate=True;MultipleActiveResultSets=True;Trusted_Connection=True;")
                .Options;

            OurHomeDbContext context = new OurHomeDbContext(options);

            _unitOfWorkService = new UnitOfWorkService(context);
        }

        [Fact]
        public async Task CreateNewBill_ShouldCreateBill()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill() { BillName = "Bins", BillOwner = user, Home = home };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.SaveAsync();

            Bill bills = await _unitOfWorkService.BillService.GetAsync(bill.ID);

            // Assert
            Assert.NotNull(bills);
        }

        [Fact]
        public async Task CreateSplitPayorsBill_ShouldSplitBillPrice()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                BillName = "Bins",
                DateTime = DateTime.Now,
                Price = 20M,
                SplitBill = true,
                BillOwner = user,
                Home = home
            };

            List<User> billPayors = new List<User>()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
            await _unitOfWorkService.SaveAsync();

            List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();
            BillPayorBill billPayorBill =
                await _unitOfWorkService.BillPayorBillService.GetAsync(actualBillPayors[actualBillPayors.Count - 1].ID);

            // Assert
            Assert.Equal(bill.Price / billPayors.Count, billPayorBill.UserPrice);
        }

        [Fact]
        public async Task CreateNonSplitPayorsBill_ShouldNotSplitBillPrice()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                BillName = "Bins",
                Price = 20M,
                SplitBill = false,
                BillOwner = user,
                Home = home
            };

            List<User> billPayors = new List<User>()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
            await _unitOfWorkService.SaveAsync();

            List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();
            BillPayorBill billPayorBill =
                await _unitOfWorkService.BillPayorBillService.GetAsync(actualBillPayors[actualBillPayors.Count - 1].ID);

            // Assert
            Assert.Equal(bill.Price, billPayorBill.UserPrice);
        }

        [Fact]
        public async Task CreateBill_ShouldCreateCorrectBillPayors()
        {
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

            List<BillPayorBill> billPayorsBefore = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
            await _unitOfWorkService.SaveAsync();

            List<Bill> bills = await _unitOfWorkService.BillService.GetAllAsync();
            List<BillPayorBill> actualBillPayors = await _unitOfWorkService.BillPayorBillService.GetAllAsync();

            // Assert
            Assert.Equal(billPayorsBefore.Count + 5, actualBillPayors.Count);
        }

        [Fact]
        public async Task CreateCoOwnerBill_ShouldSplitBillPrice()
        {
            // Arrange
            User user = new User();
            List<User> billCoOwners = new() { user, new() };
            Home home = new Home() { Name = "My Home", HomeOwner = user };

            Bill bill = new Bill()
            {
                BillName = "Bins",
                Price = 20M,
                BillOwner = user,
                BillOwnerID = user.Id,
                Note = "This is a note",
                CoOwners = billCoOwners,
                DateTime = DateTime.Now,
                Reoccurring = false,
                SplitBill = false,
                HomeID = home.ID,
                Home = home
            };

            List<User> billPayors = new List<User>() { new(), new() };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            var billPayorsCreated = await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
            await _unitOfWorkService.SaveAsync();

            decimal? expectedPrice = bill.Price / billCoOwners.Count;
            int expectedBillPayorsCreatedCount = billCoOwners.Count * billPayors.Count;

            // Assert
            Assert.Equal(expectedPrice, billPayorsCreated[0].UserPrice);
            Assert.Equal(expectedBillPayorsCreatedCount, billPayorsCreated.Count);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(20)]
        [InlineData(10)]
        public async Task CoOwnerBillWithSplitPayors_ShouldSplitTheBillPricesCorrectly(decimal price)
        {
            // Arrange
            User user = new User();
            List<User> billCoOwners = new()
            {
                user,
                new(),
            };

            Home home = new Home() { Name = "My Home", HomeOwner = user };

            Bill bill = new Bill()
            {
                BillName = "Bins",
                Price = 20M,
                BillOwner = user,
                CoOwners = billCoOwners,
                Home = home,
                SplitBill = true
            };

            List<User> billPayors = new List<User>()
            {
                new(),
                new(),
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            var billPayorsCreated = await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill, billCoOwners);
            await _unitOfWorkService.SaveAsync();

            decimal? expectedPrice = bill.Price / (billCoOwners.Count * billPayors.Count);
            int expectedBillPayorsCreatedCount = billCoOwners.Count * billPayors.Count;

            // Assert
            Assert.Equal(expectedPrice, billPayorsCreated[0].UserPrice);
            Assert.Equal(expectedBillPayorsCreatedCount, billPayorsCreated.Count);
        }

        [Fact]
        public async Task ReocurringBillMonthChanged_ShouldCreateNewBillWithUpdatedDate()
        {
            //Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                BillName = "Bins",
                Reoccurring = true,
                DateTime = DateTime.Today.AddMonths(-1),
                BillOwner = user,
                Home = home
            };

            Bill newBill = _unitOfWorkService.BillService.CreateNewReocurringBill(bill);

            //Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.BillService.AddAsync(newBill);
            await _unitOfWorkService.SaveAsync();

            // Assert
            Assert.Equal(DateTime.Now.Month, newBill.DateTime.Month);
            Assert.True(bill.ID != newBill.ID);
        }

        [Fact]
        public async Task DeleteBillNoPayments_ShouldDeleteTheBill()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                BillName = "Bins",
                Reoccurring = true,
                DateTime = DateTime.Today.AddMonths(-1),
                BillOwner = user,
                Home = home
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.SaveAsync();

            _unitOfWorkService.BillService.Delete(bill);
            await _unitOfWorkService.SaveAsync();

            var deletedBill = await _unitOfWorkService.BillService.GetAsync(bill.ID);

            // Assert
            Assert.Null(deletedBill);

        }

        [Fact]
        public async Task DeleteBillWithPayments_ShouldNotDeleteTheBill()
        {
            // Arrange
            User user = new();
            Home home = new() { Name = "My Home", HomeOwner = user };

            Bill bill = new Bill()
            {
                BillName = "Funny New Bill",
                Reoccurring = true,
                DateTime = DateTime.Today.AddMonths(-1),
                BillOwner = user,
                Home = home
            };

            List<User> billPayors = new List<User>()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
            await _unitOfWorkService.SaveAsync();

            var userBills = await _unitOfWorkService.BillPayorBillService.GetAllAsync();
            userBills[0].Payed = true;
            _unitOfWorkService.BillPayorBillService.Update(userBills[0]);

            _unitOfWorkService.BillService.Delete(bill);

            var actualBill = await _unitOfWorkService.BillService.GetAsync(bill.ID);

            // Assert
            Assert.NotNull(actualBill);
        }

        [Fact]
        public async Task UpdateBillWithPayments_ShouldUpdateTheBill()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                Home = home,
                BillOwner = user,
                BillName = "Bins",
                Reoccurring = true,
                DateTime = DateTime.Today.AddMonths(-1)
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            await _unitOfWorkService.SaveAsync();

            bill.BillName = "Not Bins";
            _unitOfWorkService.BillService.Update(bill);
            await _unitOfWorkService.SaveAsync();

            var actualBill = await _unitOfWorkService.BillService.GetAsync(bill.ID);

            // Assert
            Assert.Equal("Not Bins", actualBill.BillName);
        }

        [Fact]
        public async Task GettingAllUserBill_ShouldReturnAllUsersBills()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };

            Bill bins = new Bill() { BillName = "Bins", BillOwner = user, Home = home };
            Bill electric = new Bill() { BillName = "Electric", BillOwner = user, Home = home };
            Bill internet = new Bill() { BillName = "Bins", BillOwner = user, Home = home };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bins);
            await _unitOfWorkService.BillService.AddAsync(electric);
            await _unitOfWorkService.BillService.AddAsync(internet);
            await _unitOfWorkService.SaveAsync();

            var bills = await _unitOfWorkService.BillService.GetAllAsync(user);

            // Assert
            Assert.Equal(3, bills.Count);
            Assert.Equal(user.Id, bills[0].BillOwnerID);
        }

        [Fact]
        public async Task GettingAllUserBillInisdeAHome_ShouldReturnAllUsersBillsInsideHome()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Home newHome = new Home() { Name = "My New Home", HomeOwner = user };

            Bill bins = new Bill() { BillName = "Bins", BillOwner = user, Home = home };
            Bill electric = new Bill() { BillName = "Electric", BillOwner = user, Home = home };
            Bill internet = new Bill() { BillName = "Bins", BillOwner = user, Home = home };
            Bill newHomeBill = new Bill() { BillName = "New Home Bill", BillOwner = user, Home = newHome };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bins);
            await _unitOfWorkService.BillService.AddAsync(electric);
            await _unitOfWorkService.BillService.AddAsync(internet);
            await _unitOfWorkService.BillService.AddAsync(newHomeBill);
            await _unitOfWorkService.SaveAsync();

            var bills = await _unitOfWorkService.BillService.GetUserBillsByHomeIDAsync(user, home.ID);

            // Assert
            Assert.Equal(3, bills.Count);
            Assert.Equal(user.Id, bills[0].BillOwnerID);
            Assert.Equal(home.ID, bills[0].HomeID);
        }

        [Fact]
        public async Task BillPayorPaysBill_ShouldUpdateBillPayorBillDetails()
        {
            // Arrange
            User user = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                BillName = "Bins",
                Price = 20M,
                BillOwner = user,
                Home = home
            };

            List<User> billPayors = new List<User>()
            {
                new(),
                new(),
            };

            // Act
            await _unitOfWorkService.BillService.AddAsync(bill);
            var billPayorsCreated = await _unitOfWorkService.BillPayorBillService.AddAsync(billPayors, bill);
            await _unitOfWorkService.SaveAsync();

            DateTime datePayed = DateTime.Now;

            billPayorsCreated[0].Payed = true;
            billPayorsCreated[0].PersonalNote = "Payed the bill today";
            billPayorsCreated[0].DatePayed = datePayed;
            billPayorsCreated[0].PaymentType = "Cash";
            billPayorsCreated[0].PendingApproval = true;

            _unitOfWorkService.BillPayorBillService.Update(billPayorsCreated[0]);
            await _unitOfWorkService.SaveAsync();

            var actualBillPayor = await _unitOfWorkService.BillPayorBillService.GetAsync(billPayorsCreated[0].ID);

            // Assert
            Assert.True(actualBillPayor.Payed);
            Assert.Equal("Payed the bill today", actualBillPayor.PersonalNote);
            Assert.Equal(datePayed, actualBillPayor.DatePayed);
            Assert.Equal("Cash", actualBillPayor.PaymentType);
            Assert.True(actualBillPayor.PendingApproval);
            Assert.Equal(bill.ID, actualBillPayor.BillID);
            Assert.Equal(bill.BillOwnerID, actualBillPayor.PayeeID);
            Assert.Equal(billPayors[0].Id, actualBillPayor.PayorID);
        }

        [Fact]
        public async Task CreatingCoOwnerBill_CoOwnerBillDetailsShouldBeCorrect()
        {
            // Arrange
            User user = new User();
            User coOwner = new User();
            Home home = new Home() { Name = "My Home", HomeOwner = user };
            Bill bill = new Bill()
            {
                BillName = "Bins",
                Price = 20M,
                BillOwner = user,
                Note = "This is a note",
                DateTime = DateTime.Now,
                Reoccurring = false,
                SplitBill = false,
                Home = home
            };

            List<BillCoOwner> billCoOwners = new List<BillCoOwner>()
            {
                new()
                {
                    Bill = bill,
                    User = user,
                    Price = bill.Price / 2
                },
                new()
                {
                    Bill = bill,
                    User = coOwner,
                    Price = bill.Price / 2
                }
            };

            // Act
            await _unitOfWorkService.BillCoOwnerService.AddAsync(billCoOwners);
            await _unitOfWorkService.SaveAsync();

            var actualCoOwnerBill = await _unitOfWorkService.BillCoOwnerService.GetAsync(bill.ID, coOwner.Id);
            var actualCoOwnerBills = await _unitOfWorkService.BillCoOwnerService.GetAllBillCoOwnersByBillIDAsync(bill.ID);

            // Assert
            Assert.Equal(2, actualCoOwnerBills.Count);
            Assert.Equal(bill.ID, actualCoOwnerBill.BillID);
            Assert.Equal(coOwner.Id, actualCoOwnerBill.UserID);
            Assert.Equal(bill.Price / 2, actualCoOwnerBill.Price);
        }
    }
}