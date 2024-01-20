using ChartExample.Models.Chart;
using ChartExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ChartExample.Pages
{
    public class Page3Model : PageModel
    {

        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }
        public DB db { get; set; }

        public Page3Model(DB db)
        {
            Chart = new ChartJs();
            this.db = db;
        }

        public void OnGet()
        {
            Dictionary<string, int> codeEditorVotes = db.getCodeEditors();
            setUpChart(codeEditorVotes);
        }
        private void setUpChart(Dictionary<string, int> dataToDisplay)
        {
            try
            {
                // 1. set up chart options
                Chart.type = "bar";
                Chart.options.responsive = true;

                // 2. separate the received Dictionary data into labels and data arrays
                var labelsArray = new List<string>();
                var dataArray = new List<double>();

                foreach (var data in dataToDisplay)
                {
                    labelsArray.Add(data.Key);
                    dataArray.Add(data.Value);
                }

                Chart.data.labels = labelsArray;

                // 3. set up a dataset
                var firsDataset = new Dataset();
                firsDataset.label = "Favourite Code Editors Votes";
                firsDataset.data = dataArray.ToArray();

                Chart.data.datasets.Add(firsDataset);

                // 4. finally, convert the object to json to be able to inject in HTML code
                ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error initialising the chart inside page3.cshtml.cs");
                throw e;
            }
        }
    }
}