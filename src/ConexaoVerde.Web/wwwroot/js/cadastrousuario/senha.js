// Função para verificar se as senhas coincidem (para o formulário de fornecedor)
const senhaFornecedor = document.getElementById('senha-fornecedor');
const confirmSenhaFornecedor = document.getElementById('confirm-senha-fornecedor');
const senhaErrorFornecedor = document.getElementById('senha-error-fornecedor');

confirmSenhaFornecedor.addEventListener('input', () => {
    if (confirmSenhaFornecedor.value !== senhaFornecedor.value) {
        senhaErrorFornecedor.classList.remove('hidden');
    } else {
        senhaErrorFornecedor.classList.add('hidden');
    }
});

// Função para calcular a força da senha (para o formulário de fornecedor)
const strengthBarFornecedor = document.getElementById('password-strength-bar-fornecedor');

senhaFornecedor.addEventListener('input', () => {
    const password = senhaFornecedor.value;
    let strength = 0;

    // Regras para avaliar a força da senha
    if (password.length >= 8) strength += 1; // Comprimento mínimo de 8 caracteres
    if (/[A-Z]/.test(password)) strength += 1; // Contém uma letra maiúscula
    if (/[a-z]/.test(password)) strength += 1; // Contém uma letra minúscula
    if (/[0-9]/.test(password)) strength += 1; // Contém um número
    if (/[^A-Za-z0-9]/.test(password)) strength += 1; // Contém um caractere especial

    // Alterar a cor da barra de força
    if (strength === 0) {
        strengthBarFornecedor.style.width = '0%';
        strengthBarFornecedor.style.backgroundColor = '#e0e0e0'; // cinza
    } else if (strength === 1) {
        strengthBarFornecedor.style.width = '20%';
        strengthBarFornecedor.style.backgroundColor = '#ff4d4d'; // vermelho
    } else if (strength === 2) {
        strengthBarFornecedor.style.width = '40%';
        strengthBarFornecedor.style.backgroundColor = '#ffcc00'; // amarelo
    } else if (strength === 3) {
        strengthBarFornecedor.style.width = '60%';
        strengthBarFornecedor.style.backgroundColor = '#66cc66'; // verde claro
    } else if (strength === 4) {
        strengthBarFornecedor.style.width = '80%';
        strengthBarFornecedor.style.backgroundColor = '#339933'; // verde
    } else if (strength === 5) {
        strengthBarFornecedor.style.width = '100%';
        strengthBarFornecedor.style.backgroundColor = '#006600'; // verde escuro
    }
});

// Função para verificar se as senhas coincidem (para o formulário Cliente)
const senhaOutro = document.getElementById('senha-outro');
const confirmSenhaOutro = document.getElementById('confirm-senha-outro');
const senhaErrorOutro = document.getElementById('senha-error-outro');

confirmSenhaOutro.addEventListener('input', () => {
    if (confirmSenhaOutro.value !== senhaOutro.value) {
        senhaErrorOutro.classList.remove('hidden');
    } else {
        senhaErrorOutro.classList.add('hidden');
    }
});

// Função para calcular a força da senha (para o formulário Cliente)
const strengthBarOutro = document.getElementById('password-strength-bar-outro');

senhaOutro.addEventListener('input', () => {
    const password = senhaOutro.value;
    let strength = 0;

    // Regras para avaliar a força da senha
    if (password.length >= 8) strength += 1; // Comprimento mínimo de 8 caracteres
    if (/[A-Z]/.test(password)) strength += 1; // Contém uma letra maiúscula
    if (/[a-z]/.test(password)) strength += 1; // Contém uma letra minúscula
    if (/[0-9]/.test(password)) strength += 1; // Contém um número
    if (/[^A-Za-z0-9]/.test(password)) strength += 1; // Contém um caractere especial

    // Alterar a cor da barra de força
    if (strength === 0) {
        strengthBarOutro.style.width = '0%';
        strengthBarOutro.style.backgroundColor = '#e0e0e0'; // cinza
    } else if (strength === 1) {
        strengthBarOutro.style.width = '20%';
        strengthBarOutro.style.backgroundColor = '#ff4d4d'; // vermelho
    } else if (strength === 2) {
        strengthBarOutro.style.width = '40%';
        strengthBarOutro.style.backgroundColor = '#ffcc00'; // amarelo
    } else if (strength === 3) {
        strengthBarOutro.style.width = '60%';
        strengthBarOutro.style.backgroundColor = '#66cc66'; // verde claro
    } else if (strength === 4) {
        strengthBarOutro.style.width = '80%';
        strengthBarOutro.style.backgroundColor = '#339933'; // verde
    } else if (strength === 5) {
        strengthBarOutro.style.width = '100%';
        strengthBarOutro.style.backgroundColor = '#006600'; // verde escuro
    }
});
