$(document).ready(function(){
    const dashboardApp = {
        SetupButton: function () {
            $('#btn_search_revenue_by_date').click(function () {
                dashboardApp.GetRevenue()
                //dashboardApp.UpdateDateChart();
            })
        },
        GetRevenue: function () {
            var fromDate = $('#txt_from_date').val()
            var toDate = $('#txt_to_date').val()
            if (moment(fromDate, 'YYYY-MM-DD', true).isValid() && moment(toDate, 'YYYY-MM-DD', true).isValid()) {
                $.ajax({
                    url: 'https://localhost:44378/api/Statistical/GetStatistical',
                    //url: 'http://api.bookshop.com/api/Statistical/GetStatistical',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({start_date:fromDate,end_date:toDate}),
                    dataType: 'json',
                    beforeSend: Utils.animation(),
                    success: function (data) {
                        console.log(data)
                        if (data.status == 200) {
                           
                        } else {
                            $('#error_login').html('password or username wrong')
                        }
                        Utils.animation()
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
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