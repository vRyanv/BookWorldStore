﻿$(document).ready(function () {
    $("#Create_supplier_form").submit(function (e) {
        if ($("#txt_sup_name").val().trim() === "") {
            e.preventDefault()
            $("#empty_name").show()
        }
    })
    $("#Edit_category_form").submit(function (e) {
        if ($("#txt_sup_name_edit").val().trim() === "") {
            e.preventDefault()
            $("#empty_name_edit").show()
        }
    })
})