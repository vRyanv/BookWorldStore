$(document).ready(function () {
    $('#register_form').submit(function (e) {
        var pass = $('#txt_pass').val()
        var confirmPass = $('#txt_confirm_pass').val()
        if (pass !== confirmPass) {
            e.preventDefault()
            $('#error_confirm_pass').html("Confirm password not match")
        }
    })

    $('#txt_confirm_pass').click(function () {
        $('#error_confirm_pass').html("")
    })
})