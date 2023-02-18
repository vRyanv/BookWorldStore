const Utils = {
    animation: function () {
        if ($('.canvas-animation').css('display') === 'none') {
            $('.canvas-animation').css('display', 'flex')
        }
        else {
            $('.canvas-animation').css('display', 'none')
        }
    },
    deleteCookie: function(key){
        document.cookie = key + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    }
}
