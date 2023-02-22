const chartApp = {
    variables: {
        canvasChart: null,
        revenueProductChart: null
    },
    setUp: function(canvasID){
        chartApp.variables.canvasChart = $(canvasID)[0].getContext('2d')
    },
    renderDataChart: function (arrLabel, arrValue) {
        chartApp.variables.revenueProductChart.config._config.data.labels = arrLabel
        chartApp.variables.revenueProductChart.config._config.data.datasets[0].data = arrValue
        chartApp.variables.revenueProductChart.update()
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
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)'
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