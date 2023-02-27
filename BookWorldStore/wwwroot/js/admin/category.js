$(document).ready(function () {
    $("#Create_category_form").submit(function (e) {
        if ($("#cate_name").val().trim() === "") {
            e.preventDefault()
            $("#empty_name").show()
        }
    })
    $("#Edit_category_form").submit(function (e) {
        if ($("#cate_name_edit").val().trim() === "") {
            e.preventDefault()
            $("#empty_name_edit").show()
        }
    })
})