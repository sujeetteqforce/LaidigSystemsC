/// <reference path="ChartJs.js" />
$(document).ready(function () {
   
    $.getScript('../../Scripts/Custom/Chart.js', function () {
        
        var data = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    fillColor: "rgba(255, 227, 82, 0.5)",
                    strokeColor: "rgba(255, 227, 82,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    data: [65, 59, 90, 81, 56, 55, 40]
                },
                {
                    fillColor: "rgba(46, 212, 152, 0.5)",
                    strokeColor: "rgba(46, 212, 152,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    data: [28, 48, 40, 19, 96, 27, 100]
                }
            ]
        }

        var options = {
            animation: true
        };

        //Get the context of the canvas element we want to select
        var c = $('#myChart');
        var ct = c.get(0).getContext('2d');
        var ctx = document.getElementById("myChart").getContext("2d");
        /*********************/
        new Chart(ctx).Bar(data, options);

    })
});