using MudBlazor;
using OurHome.Shared.DTO;
using System.Net.Http.Json;

namespace OurHome.Client.View.Pages
{
    public partial class Bills
    {
        private string API_URL = "/api/Bills";
        public ChartOptions ChartOptions { get; set; }
        public HttpClient Client { get; set; }
        public List<BillsDto> BillsList { get; set; }
        public string[] InputLabls { get; set; }
        public double[] BillsPrices { get; set; }
        private List<PersonsBillsDto> PersonsBills { get; set; }

        public Bills()
        {
            BillsList = new List<BillsDto>();
            PersonsBills = new List<PersonsBillsDto>();
            ChartOptions = new();

            Client = new HttpClient() { BaseAddress = new Uri("https://localhost:5001") };

            ChartOptions.ChartPalette = new string[]
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
            BillsList = (await GetModelAsync()).ToList();
            UpdateChart();
            PersonsBills = (await GetPeoplesBills()).ToList();
        }

        public void UpdateChart() 
        {
            BillsPrices = new double[BillsList.Count];
            InputLabls = new string[BillsList.Count];

            for (int i = 0; i < BillsList.Count; i++)
            {
                InputLabls[i] = BillsList[i].Bill;
                BillsPrices[i] = (double)BillsList[i].Price;
            }

            StateHasChanged();
        }

        public async Task<IEnumerable<BillsDto>> GetModelAsync()
        {
            return await Client.GetFromJsonAsync<List<BillsDto>>(API_URL);
        }

        public async Task<IEnumerable<PersonsBillsDto>> GetPeoplesBills() 
        {
            return await Client.GetFromJsonAsync<List<PersonsBillsDto>>(API_URL + "/people");
        }

    }
}
