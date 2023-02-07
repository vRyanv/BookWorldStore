$(document).ready(function () {
    const controller = $('.controller-in-dashboard')
    const dashboard = {
        listenerEvent: function (){
            // select controller
            controller.click(function (e){
                let name = $(this).data('page-name')
                if(name !== dashboard.getCurrentURL())
                {
                    location.href = '/' + name
                }
            })
        },
        getCurrentURL: function (){
            var locationCurrent = $(location).attr("href");
            var indexSubstring = locationCurrent.lastIndexOf('/');
            return url = locationCurrent.substring(indexSubstring + 1);
        },
        run: function (){
            this.listenerEvent()
        }
    }
    dashboard.run()
})