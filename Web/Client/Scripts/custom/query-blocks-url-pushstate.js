var queryTextSelector = ".query";
var dateTextSelector = ":input.datetimepicker";

//$(document).on('change', queryTextSelector, changeQueryUrl);
//$(document).on('change', dateTextSelector, changeQueryUrl);


function changeQueryUrl(basePathName) {
    var textQueryes = $(queryTextSelector);
    var dateQueryes = $(dateTextSelector);

    var pathname = window.location.pathname;
    if (!pathname) {
        pathname = basePathName.pathname;
    }
       

    var newUrl = pathname + "?";

    for (var i = 0; i < textQueryes.length; i++) {
        var item = textQueryes[i];
        newUrl += $(item).attr('name') + "=" + encodeURIComponent($(item).val()) + "&";
    }

    for (var i = 0; i < dateQueryes.length; i++) {
        var date = dateQueryes[i];
        newUrl += $(date).attr('name') + "=" + encodeURIComponent($(date).val()) + "&";
    }

    newUrl = newUrl.substring(0, newUrl.length - 1);
    window.history.pushState("", "", newUrl);
}