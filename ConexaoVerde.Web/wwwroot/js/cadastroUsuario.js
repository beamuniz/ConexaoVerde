document.addEventListener('DOMContentLoaded', function () {
    let currentType = true; // Cliente por padrão

    function toggleAccountType(isCliente) {
        document.getElementById('clienteFields').style.display = 'none';
        document.getElementById('fornecedorFields').style.display = 'none';

        if (isCliente) {
            // Estilos do botão para cliente
            document.getElementById('clienteBtn').classList.add('bg-blue-500', 'text-white');
            document.getElementById('fornecedorBtn').classList.remove('bg-blue-500', 'text-white');
            // Exibe os campos do cliente
            document.getElementById('clienteFields').style.display = 'block';
        } else {
            // Estilos do botão para fornecedor
            document.getElementById('fornecedorBtn').classList.add('bg-blue-500', 'text-white');
            document.getElementById('clienteBtn').classList.remove('bg-blue-500', 'text-white');
            // Exibe os campos do fornecedor
            document.getElementById('fornecedorFields').style.display = 'block';
        }
    }

    // Inicializa a exibição do tipo de conta com base no valor de currentType
    toggleAccountType(currentType);

    // Event listeners para os botões de cliente e fornecedor
    document.getElementById('clienteBtn').addEventListener('click', function () {
        toggleAccountType(true); // Cliente
    });
    document.getElementById('fornecedorBtn').addEventListener('click', function () {
        toggleAccountType(false); // Fornecedor
    });
});


// Função para verificar se as senhas coincidem
const senhaInput = document.getElementById('senha');
const confirmSenhaInput = document.getElementById('confirm-senha');
const senhaError = document.getElementById('senha-error');

confirmSenhaInput.addEventListener('input', () => {
    if (confirmSenhaInput.value !== senhaInput.value) {
        senhaError.classList.remove('hidden');
    } else {
        senhaError.classList.add('hidden');
    }
});

// Função para calcular a força da senha
const strengthBar = document.getElementById('password-strength-bar');

senhaInput.addEventListener('input', () => {
    const password = senhaInput.value;
    let strength = 0;

    // Regras para avaliar a força da senha
    if (password.length >= 8) strength += 1; // Comprimento mínimo de 8 caracteres
    if (/[A-Z]/.test(password)) strength += 1; // Contém uma letra maiúscula
    if (/[a-z]/.test(password)) strength += 1; // Contém uma letra minúscula
    if (/[0-9]/.test(password)) strength += 1; // Contém um número
    if (/[^A-Za-z0-9]/.test(password)) strength += 1; // Contém um caractere especial

    // Alterar a cor da barra de força
    if (strength === 0) {
        strengthBar.style.width = '0%';
        strengthBar.style.backgroundColor = '#e0e0e0'; // cinza
    } else if (strength === 1) {
        strengthBar.style.width = '20%';
        strengthBar.style.backgroundColor = '#ff4d4d'; // vermelho
    } else if (strength === 2) {
        strengthBar.style.width = '40%';
        strengthBar.style.backgroundColor = '#ffcc00'; // amarelo
    } else if (strength === 3) {
        strengthBar.style.width = '60%';
        strengthBar.style.backgroundColor = '#66cc66'; // verde claro
    } else if (strength === 4) {
        strengthBar.style.width = '80%';
        strengthBar.style.backgroundColor = '#339933'; // verde
    } else if (strength === 5) {
        strengthBar.style.width = '100%';
        strengthBar.style.backgroundColor = '#006600'; // verde escuro
    }
});

// Função para formatar o CPF enquanto o usuário digita
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

    // Chama a função de validação
    validarCpf(cpf);
}

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
    if (cpf[9] === digito1 && cpf[10] === digito2) {
        document.getElementById("cpfError").classList.add("hidden");
        return true;
    } else {
        document.getElementById("cpfError").classList.remove("hidden");
        return false;
    }
}


// Função para visualização da imagem selecionada
const fileInput = document.getElementById('fotoPerfilInput');
const imagePreview = document.getElementById('imagePreview');
const imageElement = imagePreview.querySelector('img');

fileInput.addEventListener('change', function (e) {
    const file = e.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (event) {
            imagePreview.classList.remove('hidden');
            imageElement.src = event.target.result;
        };
        reader.readAsDataURL(file);
    }
});

// Função para permitir Drag and Drop
const dropArea = document.querySelector('.relative');

dropArea.addEventListener('dragover', function (e) {
    e.preventDefault();
    dropArea.classList.add('border-blue-500');
});

dropArea.addEventListener('dragleave', function () {
    dropArea.classList.remove('border-blue-500');
});

dropArea.addEventListener('drop', function (e) {
    e.preventDefault();
    dropArea.classList.remove('border-blue-500');

    const file = e.dataTransfer.files[0];
    if (file && file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = function (event) {
            imagePreview.classList.remove('hidden');
            imageElement.src = event.target.result;
        };
        reader.readAsDataURL(file);
    } else {
        alert('Por favor, insira um arquivo de imagem válido.');
    }
});


function formatPhoneNumber(input) {
    let phoneNumber = input.value.replace(/\D/g, "");
    if (phoneNumber.length <= 2) {
        phoneNumber = phoneNumber.replace(/(\d{2})/, "($1)");
    } else if (phoneNumber.length <= 6) {
        phoneNumber = phoneNumber.replace(/(\d{2})(\d{4})/, "($1) $2");
    } else {
        phoneNumber = phoneNumber.replace(/(\d{2})(\d{5})(\d{4})/, "($1) $2-$3");
    }
    input.value = phoneNumber;
}


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
    