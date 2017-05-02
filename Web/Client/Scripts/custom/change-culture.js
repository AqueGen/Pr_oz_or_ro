function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString() + "; Path=/");
    console.log(c_name);
    console.log(c_value);
    console.log(c_name + "=" + c_value);
    document.cookie = c_name + "=" + c_value;
    window.location.reload();
}