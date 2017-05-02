//$('#Query_ApplicationsSubmissionPeriod').click(function(e) { disableDatePeriodButton(e) });
//$('#Query_ClarificationPeriod').click(function(e) { disableDatePeriodButton(e) });
//$('#Query_AuctionPeriod').click(function(e) { disableDatePeriodButton(e) });
//$('#Query_QualificationPeriod').click(function(e) { disableDatePeriodButton(e) });


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
        if ($('.block-applicationsSubmissionPeriod').length > 0) {
            $('a.applicationsSubmissionPeriod').addClass('disabled');
        }
        if ($('.block-clarificationPeriod').length > 0) {
            $('a.clarificationPeriod').addClass('disabled');
        }
        if ($('.block-auctionPeriod').length > 0) {
            $('a.auctionPeriod').addClass('disabled');
        }
        if ($('.block-qualificationPeriod').length > 0) {
            $('a.qualificationPeriod').addClass('disabled');
        }
    });

$('#blocks')
    .on('click',
        '.delete',
        function() {
            var elementParent = $(this).parent();
            if (elementParent.hasClass('block-applicationsSubmissionPeriod') &&
                $('#Query_ApplicationsSubmissionPeriod').hasClass('disabled')) {
                $('#Query_ApplicationsSubmissionPeriod').removeClass('disabled');
            } else if (elementParent.hasClass('block-clarificationPeriod') &&
                $('#Query_ClarificationPeriod').hasClass('disabled')) {
                $('#Query_ClarificationPeriod').removeClass('disabled');
            } else if (elementParent.hasClass('block-auctionPeriod') &&
                $('#Query_AuctionPeriod').hasClass('disabled')) {
                $('#Query_AuctionPeriod').removeClass('disabled');
            } else if (elementParent.hasClass('block-qualificationPeriod') &&
                $('#Query_QualificationPeriod').hasClass('disabled')) {
                $('#Query_QualificationPeriod').removeClass('disabled');
            }

            elementParent.remove();
            var basePathName = "Tenders";
            changeQueryUrl(basePathName);
        });


function initAutocomplete(selector, data) {
    $(selector)
        .kendoAutoComplete({
            filter: 'contains',
            dataSource: data
        });
}