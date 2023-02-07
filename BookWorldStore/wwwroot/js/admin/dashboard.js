$(document).ready(function(){
    const dashboardApp = {
        renderBlankChart: function (){
            chartApp.createBlankChart("#product_statistic_chart")
        },
        run: function() {
            dashboardApp.renderBlankChart();
        }
    }

    dashboardApp.run()
})