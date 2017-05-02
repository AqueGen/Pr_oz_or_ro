$(document)
    .ready(function () {

        $(function() {
            if ($('#physicalCompany').hasClass('active')) {
                $('#companyName').val('').hide();
                $('#CompanyType').val('Individual');
            } else {
                $('#companyName').show();
                $('#CompanyType').val('Corporation');
            }
        });

        $('#physicalCompany')
            .click(function() {
                $('#companyName').val('').hide();
                $('#physicalCompany').addClass('active');
                $('#legalCompany').removeClass('active');
                $('#CompanyType').val('Individual');
            });


        $('#legalCompany')
            .click(function() {
                $('#companyName').show();
                $('#physicalCompany').removeClass('active');
                $('#legalCompany').addClass('active');
                $('#CompanyType').val('Corporation');
            });
    });