$(document).ready(function () {
    $('.mask-money').maskMoney({
        prefix: 'R$ ',
        allowNegative: false,
        thousands: '.',
        decimal: ',',
        allowZero: true,
        affixesStay: true  
    }).maskMoney('mask');

    $('.mask-money').each(function () {
        if ($(this).val() === '') {
            $(this).val('R$ 0,00');  
        }
    });
});