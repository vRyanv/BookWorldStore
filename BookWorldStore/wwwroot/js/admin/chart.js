const chartApp = {
    variables: {
        canvasID: "",
        canvasChart: $(canvasID)[0].getContext('2d'),
        revenueProductChart: null
    },
    createBlankChart : function(canvasID) {
        chartApp.variables.canvasID = canvasID
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