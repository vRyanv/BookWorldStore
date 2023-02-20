$(document).ready(function(){
    const AppLogin = {
        SetupButton : function(){
            $('.login__submit').click(function () {
                var email = $('#txt_email').val()
                var pass = $('#txt_pass').val()
                var error = false
                if(email.trim() == ""){
                    $('#error_email_mess').show()
                    console.log("click")
                    error = true
                }
                if (pass.trim() == "") {
                    $('#error_pass_mess').show()
                    error = true
                }
                if(!error){
                    AppLogin.RequestLogin()
                }
            })

            $('#btn_forget_pass').click(function () {
                $('#btn_open_forget_pass_modal').click()
            })

            $('#btn_send_request_reset_pass').click(function(){
                AppLogin.RequestResetPass()
            })
        },
        SetupHideErrorMess : function() {
            $('#txt_email').click(function(){
                $('#error_email_mess').hide()
            })

            $('#txt_pass').click(function(){
                $('#error_pass_mess').hide()
            })
        },
        RequestLogin: function(){
            var email = $("#txt_email").val()
            var password = $("#txt_pass").val()
            $.ajax({
                url:'http://localhost:44378/api/token/login',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ email: email, password: password}),
                dataType: 'json',
                beforeSend: Utils.animation(),
                success: function (data) {
                    if (data.status == 200) {
                        document.cookie = '__UserToken=' + data.token + ';path=/'
                        if (data.role == "onwner") {
                            location.href = "/dashboard";
                        } else if (data.role == "admin") {
                            location.href = "/supperAdmin";
                        } else {
                            location.href = "/home";
                        }
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
        },
        RequestResetPass: function(){
            var email = $("#txt_email_reset_pass").val()
            if(email.trim() == ""){
                alert("Enter email to reset password")
            }
            else {
                location.href = "/Home/SendResetPassRequest?email=" + email
            }
        },
        Run: function(){
            AppLogin.SetupButton()
            AppLogin.SetupHideErrorMess()
        }
    }
    AppLogin.Run()
})