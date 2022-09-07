using MudBlazor;
using OurHome.Shared.DTO;
using System.Net.Http.Json;

namespace OurHome.Client.View.Pages
{
    public partial class Bills
    {
        public ChartOptions chartOptions { get; set; }
        public HttpClient client { get; set; }
        public List<BillsDto> bills { get; set; }
        public string[] inputLabls { get; set; }
        public double[] billsPrices { get; set; }

        public Bills()
        {   
            bills = new List<BillsDto>();
            chartOptions = new();
            client = new HttpClient() { BaseAddress = new Uri("https://localhost:5001") };

            chartOptions.ChartPalette = new string[]
            {
                "#ff4081",
                "#81FF40",
                "#4081ff",
                "#40CDff",
                "#ff4100",
                "#ff2300"
            };
        }

        protected override async Task OnInitializedAsync()
        {
            bills = (await GetModelAsync()).ToList();
            UpdateChart();
        }

        public void UpdateChart() 
        {
            billsPrices = new double[bills.Count];
            inputLabls = new string[bills.Count];

            for (int i = 0; i < bills.Count; i++)
            {
                inputLabls[i] = bills[i].Bill;
                billsPrices[i] = (double) bills[i].Price;
            }

            StateHasChanged();
        }

        public async Task<List<BillsDto>> GetModelAsync()
        {
            string endpoint = "/api/Bills";
            return bills = await client.GetFromJsonAsync<List<BillsDto>>(endpoint);
        }

    }
}
