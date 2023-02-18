﻿const chartApp = {
    variables: {
        canvasChart: null,
        revenueProductChart: null
    },
    setUp: function(canvasID){
        chartApp.variables.canvasChart = $(canvasID)[0].getContext('2d')
    },
    createBlankChart : function() {
        chartApp.variables.revenueProductChart = new Chart(
            chartApp.variables.canvasChart
            , {
            type: 'bar',
            data: {
                labels: null,
                datasets: [{
                    label: 'Revenue',
                    data: null,
                    backgroundColor: [
                        'rgba(153, 102, 255, 0.2)'
                    ],
                    borderColor: [
                        'rgba(153, 102, 255, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        }
        );
    }
}