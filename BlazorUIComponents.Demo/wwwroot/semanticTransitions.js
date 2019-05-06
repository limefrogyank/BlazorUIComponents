function showMenu(element) {
    //var x = window.matchMedia("(max-width: 767.98px)");
    //if (x.matches) {
        $(element).removeClass("collapse");
        $(element).transition({
            animation: 'slide down in',
            onComplete: function () {

            }
        });
    //}
}

function hideMenu(element) {
    //var x = window.matchMedia("(max-width: 767.98px)");
    //if (x.matches) {
        $(element).transition({
            animation: 'slide down out',
            onComplete: function () {
                $(element).addClass("collapse");
                $(element).removeClass("hidden");
            }
        });
    //}
}

function matchesMediaQuery(query) {
    var x = window.matchMedia(query);
    return x.matches;
}