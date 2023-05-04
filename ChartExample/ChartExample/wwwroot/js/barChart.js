
    document.addEventListener('DOMContentLoaded', (event) => {

        var ctx = document.getElementById('myChart');
    var myChart = new Chart(ctx, @Html.Raw(Model.ChartJson) );

    });