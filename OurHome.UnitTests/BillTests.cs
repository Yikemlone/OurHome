using OurHome.Models.Models;

namespace OurHome.UnitTests
{
    public class BillTests
    {
        private readonly IBillsService billsService;

        //public BillTests(IBillsService billsService)
        //{
        //    this.billsService = billsService;
        //}

        [Fact]
        public void CreateNewBillDefault_ShouldCreateBill()
        {
            // Arrange
           // Bill bill = new Bill();

            // Act
           // BillTests.CreateBill(bill);

            // Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void CreateSplitBill_ShouldSplitBillPrice()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CreateCoOwnerBill_ShouldSplitBillPrice()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [InlineData()]
        public void CoOwnerBillWithSplit_ShouldSplitTheBillPricesCorrectly() 
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