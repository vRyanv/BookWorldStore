$(document).ready(function(){
    const dashboardApp = {
        renderBlankChart: function (){
            chartApp.setUp("#product_statistic_chart")
            chartApp.createBlankChart()
        },
        run: function() {
            dashboardApp.renderBlankChart();
        }
    }

    dashboardApp.run()
})