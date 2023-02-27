$(document).ready(function () {
    $("#Create_supplier_form").submit(function (e) {
        if ($("#txt_sup_name").val().trim() === "") {
            e.preventDefault()
            $("#empty_name").show()
        }
        if ($("#txt_sup_address").val().trim() === "") {
            e.preventDefault()
            $("#empty_address").show()
        }
    })
    $("#Edit_supplier_form").submit(function (e) {
        if ($("#txt_sup_name_edit").val().trim() === "") {
            e.preventDefault()
            $("#empty_name_edit").show()
        }
        if ($("#txt_sup_address_edit").val().trim() === "") {
            e.preventDefault()
            $("#empty_address_edit").show()
        }
    })
})