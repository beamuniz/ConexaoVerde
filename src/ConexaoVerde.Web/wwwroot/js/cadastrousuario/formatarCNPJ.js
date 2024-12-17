
// formatar CNPJ
function formatCNPJ(input) {
    let value = input.value.replace(/\D/g, ''); // Remove tudo que não é número
    
    // Formata o CNPJ para o padrão XX.XXX.XXX/XXXX-XX
    if (value.length <= 14) {
        value = value.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, "$1.$2.$3/$4-$5");
    }
    
    input.value = value;
}