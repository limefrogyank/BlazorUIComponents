function showMenu(element) {
        $(element).removeClass("collapse");
        $(element).transition({
            animation: 'slide down in',
            onComplete: function () {

            }
        });
}

function hideMenu(element) {
        $(element).transition({
            animation: 'slide down out',
            onComplete: function () {
                $(element).addClass("collapse");
                $(element).removeClass("hidden");
            }
        });
}

function matchesMediaQuery(query) {
    var x = window.matchMedia(query);
    return x.matches;
}