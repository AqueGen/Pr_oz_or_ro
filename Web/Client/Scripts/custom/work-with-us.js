$(document)
    .ready(function () {
        // Осуществляет переход на предыдущий слайд
        $(".prev-slide")
            .click(function () {
                $("#partnerCarousel").carousel('prev');
            });
        // Осуществляет переход на следующий слайд
        $(".next-slide")
            .click(function () {
                $("#partnerCarousel").carousel('next');
            });
    });