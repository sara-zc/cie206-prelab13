using ChartExample.Models.Chart;
using ChartExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ChartExample.Pages
{
    public class Page2Model : PageModel
    {
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }

        private readonly ILogger<IndexModel> _logger;
        public Page2Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var chartData = @"
            {
            type: 'bar',
            responsive: true,
            data:
            {
                datasets: [{
                    backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                        ],
                    borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                        ],
                    borderWidth: 1
                }]
            },  
            options:
            {
                scales:
                {
                    y: [{
                        ticks:
                        {
                            beginAtZero: true
                        }
                    }]
                }
            } 
        }"; //end of chartdata

            try
            {
                Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
                // set up the labels
                string[] labelsArray = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
                Chart.data.labels = labelsArray;
                // set up the dataset
                Dataset dataset = new Dataset();
                Chart.data.datasets[0].label = "Favourite Colors Votes";
                int[] dataArray = { 12, 19, 3, 5, 2, 3 };
                Chart.data.datasets[0].data = dataArray;

                ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception initialising the chart inside page2.cshtml.cs");
                throw e;
            }
        }
    }
}
