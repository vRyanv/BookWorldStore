$(document).ready(function(){
    const dashboardApp = {
        SetupButton: function () {
            $('#btn_search_revenue_by_date').click(function () {
                var fromDate = $('#txt_from_date').val()
                var toDate = $('#txt_to_date').val()
                dashboardApp.GetRevenue(fromDate, toDate)
            })
        },
        GetRevenue: function (fromDate, toDate) {
            if (moment(fromDate, 'YYYY-MM-DD', true).isValid() && moment(toDate, 'YYYY-MM-DD', true).isValid()) {
                $.ajax({
                    url: 'http://api.bookshop.com:81/api/Statistical/GetStatistical',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({start_date:fromDate,end_date:toDate}),
                    dataType: 'json',
                    beforeSend: Utils.animation(),
                    success: function (data) {
                        console.log(data)
                        dashboardApp.UpdateChart(data)
                        Utils.animation()
                    },
                    error: function () {
                        alert("Server error!")
                        Utils.animation()
                    }
                })
            } else {
                alert('Invalid date')
            }
            
        },
        renderBlankChart: function (){
            chartApp.setUp("#product_statistic_chart")
            chartApp.createBlankChart()
        },
        UpdateChart: function (data) {
            var totalRevenue = 0
            var title = []
            var value = []
            for(var i = 0; i < data.length; i++)
            {
                title[i] = data[i].title
                value[i] = data[i].totalPrice
                totalRevenue += data[i].totalPrice
            }

            $('#total_revenue').html('$'+totalRevenue)
            chartApp.renderDataChart(title, value)
        },
        GetRevenueToday: function(){
            var date = moment();
            var currentDate = date.format('YYYY-MM-D');
            $('#txt_from_date').val(currentDate)
            $('#txt_to_date').val(currentDate)
            dashboardApp.GetRevenue(currentDate, currentDate)
        },
        run: function() {
            dashboardApp.renderBlankChart();
            dashboardApp.GetRevenueToday();
            dashboardApp.SetupButton();
        }
    }

    dashboardApp.run()
})