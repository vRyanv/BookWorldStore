$(document).ready(function () {
    const AppHome = {
        SetUpButton: function () {
            $('btn_logout').click(function () {
                Utils.deleteCookie('__UserToken')
                location.href = "/Home/Login"
            })


        },
        Run: function () {

        }
    }

    AppHome.Run()
})