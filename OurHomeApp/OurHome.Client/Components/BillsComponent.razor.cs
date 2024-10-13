using MudBlazor;

namespace OurHome.Client.View.Pages
{
    public partial class Bills
    {
        private string API_URL = "/api/Bills";
        public ChartOptions ChartOptions { get; set; }
        public HttpClient Client { get; set; }
        public string[] InputLabls { get; set; }
        public double[] BillsPrices { get; set; }
        private List<ChartSeries> Series { get; set; }
        private double BillsTotal { get; set; }
        private bool selected = true;
    }
}
