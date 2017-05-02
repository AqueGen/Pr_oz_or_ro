function formatDate(value, format) {
    var i = parseInt(value, 10)
    if (isNaN(i)) {
        return ''
    } else {
        return moment(i).format(format || "L LT");
    }
};

$.validator.addMethod("date",
    function(value, element) {
        var isOptional = this.optional(element);
        var isTest = !/Invalid|NaN/.test(new Date(value).toString());
        var isMoment = moment(value, $(element).attr("data-date-format")).isValid();
        return isOptional || isTest || isMoment || true;
    });

$(function() {
    var culture = Cookies.get('_culture') || window.navigator.userLanguage || window.navigator.language || 'uk';
    moment.locale(culture);
    kendo.culture(culture);
    Cookies.set("TimeZoneName", moment.tz.guess(), { path: '/', expires: null });

    initNumberTextBox();
    initCurrencyPercentage();
    correctDates();
    $(document)
        .ajaxSuccess(function() {
            initNumberTextBox();
            correctDates();
        });

    //Currency
    function initNumberTextBox() {
        $('.currency')
            .kendoNumericTextBox({
                culture: culture,
                decimals: 2,
                //format: 'c',
                min: 0,
                step: 0.1
            });

        $('.percent')
            .kendoNumericTextBox({
                culture: culture,
                format: '#.## \\%',
                min: 0,
                step: 0.01,
                decimals: 2
            });
    }

    //Currency feature percentage
    function initCurrencyPercentage() {
        $('.currency-feature')
            .kendoNumericTextBox({
                //culture: culture,
                format: 'p',
                min: 0,
                max: 0.3,
                step: 0.01
            });
    }


});

function correctDates() {
    $('[data-date]')
        .each(function () {
            var $this = $(this),
                value = formatDate($this.attr('data-date'), $this.attr('data-date-format'));
            $(this).html(value);
        });

    var pikers = $('input.datetimepicker');

    $('input.datetimepicker')
        .each(function () {
            var hidden = $('#' + this.id.slice(0, -6));
            var utcTime = hidden.val();
            if (hidden) {
                var $this = $(this);
                $this.kendoDateTimePicker({
                    value: utcTime ? moment.utc(utcTime).toDate() : null,
                    format: $this.attr('data-date-format'),
                    change: function () {
                        var localTime = moment(this.value());
                        hidden.val(localTime.isValid()
                            ? localTime.utc().format().slice(0, -1)
                            : null);
                    }
                });
            }
        });
}