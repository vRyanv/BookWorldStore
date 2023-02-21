$(document).ready(function () {
    const AppHome = {
        SetUpButton: function () {
            $('#btn_logout').click(function () {
                Utils.deleteCookie('__UserToken')
                location.href = "/Home/Login"
            })

            $('#btn_search_book').click(function () {
                AppHome.SearchBook()
            })
        },
        SearchBook: function(){
            if ($('#txt_search_book').val().trim() !== "") {
                location.href = "/Home/SearchBook?title=" + $('#txt_search_book').val()
            }
        },
        Run: function () {
            AppHome.SetUpButton()
        }
    }

    AppHome.Run()
})