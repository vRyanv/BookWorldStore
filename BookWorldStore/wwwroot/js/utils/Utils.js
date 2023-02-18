const Utils = {
    animation: function () {
        if ($('.canvas-animation').css('display') === 'none') {
            $('.canvas-animation').css('display', 'flex')
        }
        else {
            $('.canvas-animation').css('display', 'none')
        }
    },
    previewImage: function () {
        document.getElementById("img_preview").src = '/images/decoration/image-product.png';
        var imageReader = new FileReader();
        imageReader.readAsDataURL(document.getElementById("pro_img").files[0]);
        imageReader.onload = function (oFREvent) {
            document.getElementById("img_preview").src = oFREvent.target.result;
        };
    },
    deleteCookie: function(key){
        document.cookie = key + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    }

}
