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
        private List<ChartSeries> Series { get; set; }
        private double BillsTotal { get; set; }
        private bool selected = true;

        public Bills()
        {
            BillsList = new List<BillsDto>();
            PersonsBills = new List<PersonsBillsDto>();
            ChartOptions = new();
            Series = new();

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

            foreach (PersonsBillsDto personsBills in PersonsBills)
            {
                Series.Add(new ChartSeries
                {
                    Name = personsBills.PersonID.ToString(),
                    Data = new double[]
                    {
                        (double) personsBills.Rent,
                        (double) personsBills.Bins,
                        (double) personsBills.Electricity,
                        (double) personsBills.Milk,
                        (double) personsBills.Oil,
                        (double) personsBills.Internet
                    }
                });
            }
        }

        public void UpdateChart() 
        {
            BillsPrices = new double[BillsList.Count];
            InputLabls = new string[BillsList.Count];

            for (int i = 0; i < BillsList.Count; i++)
            {
                InputLabls[i] = BillsList[i].Bill + " - " + (double)BillsList[i].Price;
                BillsPrices[i] = (double)BillsList[i].Price;
                BillsTotal += (double)BillsList[i].Price;
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

        private void ChangeColour() 
        {
            selected = !selected;
            StateHasChanged();
        }

        private void DisplayPeronsBills(PersonsBillsDto personsBills) 
        {
            /// The purpose of this is display the perosn that was clicked on bills
        }

    }
}
