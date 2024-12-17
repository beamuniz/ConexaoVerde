// Função que consulta o CEP e preenche os campos automaticamente
function consultarCEP() {
    var cep = document.getElementById("cep").value.replace(/\D/g, ''); // Remove qualquer caractere não numérico

    // Verifica se o CEP tem o formato correto (8 dígitos)
    if (cep.length === 8) {
        var url = `https://viacep.com.br/ws/${cep}/json/`;

        // Requisição para a API do ViaCEP
        fetch(url)
            .then(response => response.json())
            .then(data => {
                if (data.erro) {
                    alert("CEP não encontrado.");
                } else {
                    // Preenche os campos com os dados retornados pela API
                    document.getElementById("Endereco_Rua").value = data.logradouro || ''; // Preenche a rua
                    document.getElementById("Endereco_Cidade").value = data.localidade || ''; // Preenche a cidade
                    document.getElementById("Endereco_Estado").value = data.uf || ''; // Preenche o estado

                    // Habilita os campos de rua, cidade e estado
                    document.getElementById("Endereco_Rua").disabled = false;
                    document.getElementById("Endereco_Cidade").disabled = false;
                    document.getElementById("Endereco_Estado").disabled = false;
                }
            })
            .catch(error => {
                console.error('Erro ao consultar o CEP:', error);
                alert("Erro ao consultar o CEP. Tente novamente.");
            });
    } else {
        alert("CEP inválido. Por favor, insira um CEP válido.");
    }
}

// A função será chamada assim que a página carregar
window.onload = function () {
    // Verifica se já existe um valor no campo CEP e chama a função de consulta automaticamente
    var cepField = document.getElementById("cep");
    if (cepField.value.length === 8) {
        consultarCEP();
    }

    // Adiciona o atributo disabled aos campos de rua, cidade e estado desde o início
    document.getElementById("Endereco_Rua").disabled = true;
    document.getElementById("Endereco_Cidade").disabled = true;
    document.getElementById("Endereco_Estado").disabled = true;
};
