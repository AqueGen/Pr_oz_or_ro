$('#Query_ProcedurePeriod').click(function (e) { disableDatePeriodButton(e) });


//$.getJSON("/Tender/GetCpvCode", function (json) {initAutocomplete(".block-cpvCode > input", json);});
//$.getJSON("/Tender/GetScgsCode", function (json) { initAutocomplete(".block-scgsCode > input", json); });
//$.getJSON("/Tender/GetProcurer", function (json) { initAutocomplete(".block-Procurer > input", json); });
//$.getJSON("/Tender/GetRegion", function (json) { initAutocomplete(".block-region > input", json); });
//$.getJSON("/Tender/GetStatus", function (json) { initAutocomplete(".block-status > input", json); });

function disableDatePeriodButton(e) {
    $('#' + e.target.id).addClass('disabled');
}

$(document)
    .ready(function() {
        if ($('.block-procedurePeriod').length > 0) {
            $('a.procedurePeriod').addClass('disabled');
        }
    });

$('#blocks')
    .on('click',
        '.delete',
        function() {
            var elementParent = $(this).parent();
            if (elementParent.hasClass('block-procedurePeriod') &&
                $('#Query_ProcedurePeriod').hasClass('disabled')) {
                $('#Query_ProcedurePeriod').removeClass('disabled');
            }

            elementParent.remove();
            var basePathName = "Plans";
            changeQueryUrl(basePathName);
        });


function initAutocomplete(selector, data) {
    $(selector)
        .kendoAutoComplete({
            filter: 'contains',
            dataSource: data
        });
}