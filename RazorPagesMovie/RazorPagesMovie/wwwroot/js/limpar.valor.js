// ESTE BLOCO É CRÍTICO para garantir o envio do valor LIMPO
$('form').submit(function () {
    $('.mask-money').each(function () {
        // Obtém o valor numérico puro (ex: 35.00)
        var valorNumericoPuro = $(this).maskMoney('unmasked')[0];
        // Define o valor do campo para o valor numérico puro ANTES de enviar o formulário
        $(this).val(valorNumericoPuro);
    });
});