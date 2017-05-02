var currentTimeElement = '#currentTime';

$(currentTimeElement)
    .ready(function () {
        setInterval(
            function () {
                var time = new Date();
                var currentTimeZoneOffsetInHours = -time.getTimezoneOffset() / 60;

                var formatedTime = getDateTimeToString(time, 'hh:mm:ss');
                $(currentTimeElement).text(formatedTime);
            },
            1000);
    });

/**
* Генерирует дату и время в заданном формате
* @param {String} strFormat формат-строка
* @returns {String} отформатированная дата
*
yyyy - полный год, пр.: 2012
yy - последние цифры года, пр.: 12
MM - номер месяца, пр.: 02
dddd - полное название дня недели, пр.: Среда
ddd - сокращенное название дня недели, пр.: Ср.
dd - число, пр.: 01
hh - часы, пр.: 10
mm - минуты, пр.: 05
ss - секунды, пр.: 23
zz - миллисекунды, пр.: 233

ПРИМЕР ИСПОЛЬЗОВАНИЯ:

getDateTimeToString("dd/MM/yy г. Время:hh:mm:ss");
getDateTimeToString("dd-MM-yyyy hh:mm");
getDateTimeToString("Год: yyyy, День недели: dddd");

*/
function getDateTimeToString(date, strFormat) {
    var resultDateTime = strFormat;

    var daysLong = ["Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье"];
    var daysShort = ["Пн.", "Вт.", "Ср.", "Чт.", "Пт.", "Сб.", "Вс."];
    var yearRegExp = date.getFullYear();
    var monthRegExp = (String(date.getMonth() + 1).length == 1) ? ("0" + (date.getMonth() + 1)) : (date.getMonth() + 1);
    var dayRegExp = (date.getDate().toString().length == 1) ? ("0" + date.getDate()) : date.getDate();
    var dayNameRegExp = date.getDay();
    var hoursRegExp = (date.getHours().toString().length == 1) ? ("0" + date.getHours()) : date.getHours();
    var minuteRegExp = (date.getMinutes().toString().length == 1) ? ("0" + date.getMinutes()) : date.getMinutes();
    var secondsRegExp = (date.getSeconds().toString().length == 1) ? ("0" + date.getSeconds()) : date.getSeconds();
    var milisecondsRegExp = (date.getMilliseconds().toString().length == 1) ? ("00" + date.getMilliseconds()) : ((date.getMilliseconds().toString().length == 2) ? ("0" + date.getMilliseconds()) : date.getMilliseconds());

    resultDateTime = resultDateTime.replace(new RegExp('yyyy', 'g'), yearRegExp);
    resultDateTime = resultDateTime.replace(new RegExp('yy', 'g'), String(yearRegExp).slice(-2));
    resultDateTime = resultDateTime.replace(new RegExp('MM', 'g'), monthRegExp);
    resultDateTime = resultDateTime.replace(new RegExp('dddd', 'g'), daysLong[dayNameRegExp - 1]);
    resultDateTime = resultDateTime.replace(new RegExp('ddd', 'g'), daysShort[dayNameRegExp - 1]);
    resultDateTime = resultDateTime.replace(new RegExp('dd', 'g'), dayRegExp);
    resultDateTime = resultDateTime.replace(new RegExp('hh', 'g'), hoursRegExp);
    resultDateTime = resultDateTime.replace(new RegExp('mm', 'g'), minuteRegExp);
    resultDateTime = resultDateTime.replace(new RegExp('ss', 'g'), secondsRegExp);
    resultDateTime = resultDateTime.replace(new RegExp('zz', 'g'), milisecondsRegExp);

    return resultDateTime + "";
};