// Função para validar o CPF
function validarCpf(cpf) {
    // Remove os caracteres especiais para validação
    cpf = cpf.replace(/\D/g, '');

    // Verifica se o CPF tem 11 dígitos
    if (cpf.length !== 11) {
        document.getElementById("cpfError").classList.remove("hidden");
        return false;
    }

    // Verifica se todos os números são iguais (caso do CPF 111.111.111-11, por exemplo)
    if (/^(\d)\1{10}$/.test(cpf)) {
        document.getElementById("cpfError").classList.remove("hidden");
        return false;
    }

    // Cálculo do primeiro dígito verificador
    let soma = 0;
    let peso = 10;
    for (let i = 0; i < 9; i++) {
        soma += cpf[i] * peso;
        peso--;
    }
    let digito1 = 11 - (soma % 11);
    if (digito1 === 10 || digito1 === 11) digito1 = 0;

    // Cálculo do segundo dígito verificador
    soma = 0;
    peso = 11;
    for (let i = 0; i < 10; i++) {
        soma += cpf[i] * peso;
        peso--;
    }
    let digito2 = 11 - (soma % 11);
    if (digito2 === 10 || digito2 === 11) digito2 = 0;

    // Verifica se os dígitos calculados coincidem com os fornecidos
    if (cpf[9] == digito1 && cpf[10] == digito2) {
        document.getElementById("cpfError").classList.add("hidden");
        return true;
    } else {
        document.getElementById("cpfError").classList.remove("hidden");
        return false;
    }
}
