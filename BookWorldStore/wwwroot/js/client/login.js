$(document).ready(function(){
    const AppLogin = {

        SetUpEventListener: function(){
            $('.login__submit').click(function(){
                AppLogin.RequestLogin();
            })
        },
        RequestLogin: function(){
            var email = $("#txt_email").val()
            var password = $("#txt_pass").val()
            $.ajax({
                url:'https://localhost:44378/api/token/login',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ email: email, password: password, role: "" }),
                success: function (data){
                    console.log(data)
                    document.cookie = '__UserToken=' + data.token + ';path=/'
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.error(xhr.status);
                    console.error(thrownError);
                }
            })
        },
        run: function(){
            AppLogin.SetUpEventListener()
        }
    }

    AppLogin.run()
})