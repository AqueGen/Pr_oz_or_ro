$(document)
    .ready(function() {
        var individual = 0;
        var corporation = 1;

        var companyType = $('#CompanyType');
        var companyName = $('#CompanyName');
        var companyNameDiv = $('#companyNameDiv');

        initEditPersonal();

        function initEditPersonal() {
            var selectedIndex = parseInt(companyType.val());
            if (selectedIndex === individual) {
                companyNameDiv.hide();
                companyName.val('');

            } else if (selectedIndex === corporation) {
                companyNameDiv.show();
            };
        }

        companyType.on('change', function (e) {
            initEditPersonal();
        });

    });