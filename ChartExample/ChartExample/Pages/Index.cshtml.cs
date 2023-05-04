using ChartExample.Models;
using ChartExample.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
namespace ChartExample.Pages
{
    public class IndexModel : PageModel
    {
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            StudentInfo student_data = new StudentInfo();
            DB db = new DB();
            student_data = db.StudentInfoById("201739186");
            // Ref: https://www.chartjs.org/docs/latest/
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
                //string[] labelsArray = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
                string[] labelsArray = { "Pycharm", student_data.CodeEditor };
                Chart.data.labels = labelsArray;
                // set up the dataset
                Dataset dataset = new Dataset();
                Chart.data.datasets[0].label = "Favourite Code Editors Votes";
                int[] dataArray = {0, 1};
                Chart.data.datasets[0].data = dataArray;

                ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            } catch(Exception e)
            {
                Console.WriteLine("Exception initialising the chart inside index.cshtml.cs");
                throw e;
            }


        }
    }
}