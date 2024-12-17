// Função para formatar o CPF
function formatarCpf(input) {
    // Remove qualquer coisa que não seja número
    let cpf = input.value.replace(/\D/g, '');

    // Formata o CPF com pontos e traço
    if (cpf.length <= 11) {
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
    }

    // Atualiza o campo de input com o CPF formatado
    input.value = cpf;

    // Chama a função de validação sem os caracteres especiais
    validarCpf(cpf.replace(/\D/g, ''));
}