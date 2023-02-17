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
                url:'https://localhost:44378/api/token/login',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ email: email, password: password, role: "", token_reset_pass: ""}),
                dataType: 'json',
                beforeSend: Utils.animation(),
                success: function (data) {
   /*                 data = JSON.parse(data)*/
                    console.log(data)
                    console.log(data.email)
                    document.cookie = '__UserToken=' + data.token + ';path=/'
                    Utils.animation()
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.error(xhr.status);
                    console.error(thrownError);
                    Utils.animation()
                }
            })
        },
        RequestResetPass: function(){
            var email = $("#txt_email_reset_pass").val()
            if(email.trim() == ""){
                alert("Enter email to reset password")
            }
            else{
                $.ajax({
                    url:'https://localhost:44378/api/mail/RequestResetPass',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(email),
                    processData: false,
                    dataType: 'json',
                    beforeSend: Utils.animation(),
                    success: function (data) {
                        console.log(data)
                        Utils.animation()
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        console.error(xhr.status);
                        console.error(thrownError);
                        Utils.animation()
                    }
                })
            }
        },
        run: function(){
            AppLogin.SetupButton()
            AppLogin.SetupHideErrorMess()
        }
    }
    AppLogin.run()
})