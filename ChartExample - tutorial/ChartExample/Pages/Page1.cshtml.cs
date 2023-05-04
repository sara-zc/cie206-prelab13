using ChartExample.Models.Chart;
using ChartExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ChartExample.Pages
{
    public class Page1Model : PageModel
    {
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public Page1Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Ref: https://www.chartjs.org/docs/latest/
            var chartData = @"
            {
            type: 'bar',
            responsive: true,
            data:
            {
                labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
                datasets: [{
                    label: 'Favourite Colors Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                        ],
                    borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
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
                //string[] labelsArray = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
                // set up the dataset
                /*Dataset dataset = new Dataset();
                Chart.data.datasets[0].label = "Favourite Code Editors Votes";*/
                // set up data
                /*int[] dataArray = { 0, 1 };
                Chart.data.datasets[0].data = dataArray;*/

                ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception initialising the chart inside page1.cshtml.cs");
                throw e;
            }
        }
    }
}
