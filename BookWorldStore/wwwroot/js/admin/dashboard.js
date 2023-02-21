$(document).ready(function(){
    const dashboardApp = {
        SetupButton: function () {
            $('#btn_search_revenue_by_date').click(function () {
                dashboardApp.UpdateDateChart();
            })
        },
        renderBlankChart: function (){
            chartApp.setUp("#product_statistic_chart")
            chartApp.createBlankChart()
        },
        UpdateDateChart: function () {
            var title = ['khang', 'Nhan', 'son', 'Lam']
            var value = [100,200,50,150]
            chartApp.renderDataChart(title, value)
        },
        run: function() {
            dashboardApp.renderBlankChart();
            dashboardApp.SetupButton();
        }
    }

    dashboardApp.run()
})