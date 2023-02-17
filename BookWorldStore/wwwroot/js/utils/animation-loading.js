const Utils = {
    animation: function () {
        if ($('.canvas-animation').css('display') === 'none') {
            $('.canvas-animation').css('display', 'flex')
        }
        else {
            $('.canvas-animation').css('display', 'none')
        }
    }
}
