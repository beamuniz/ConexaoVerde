// Função para exibição de formulários
document.addEventListener('DOMContentLoaded', function () {
    // Nenhum botão deve estar selecionado por padrão
    let currentType = null; // Nenhum tipo de conta está selecionado inicialmente
  
    function toggleAccountType(isCliente) {
      // Esconde os campos de ambos os tipos
      document.getElementById('clienteFields').style.display = 'none';
      document.getElementById('fornecedorFields').style.display = 'none';
  
      // Remover o estado de selecionado de ambos os botões
      document.getElementById('clienteBtn').classList.remove('bg-green-500', 'text-white', 'border-white');
      document.getElementById('fornecedorBtn').classList.remove('bg-green-500', 'text-white', 'border-white');
  
      // Aplica o estilo de "selecionado" no botão correto
      if (isCliente !== null) { // Se o tipo estiver definido
        if (isCliente) {
          // Estilos do botão para cliente
          document.getElementById('clienteBtn').classList.add('bg-white-500', 'text-white', 'border-white');
          document.getElementById('fornecedorBtn').classList.remove('bg-white-500', 'text-white', 'border-white');
          
          // Exibe os campos do cliente
          document.getElementById('clienteFields').style.display = 'block';
        } else {
          // Estilos do botão para fornecedor
          document.getElementById('fornecedorBtn').classList.add('bg-white-500', 'text-white', 'border-white');
          document.getElementById('clienteBtn').classList.remove('bg-white-500', 'text-white', 'border-white');
          
          // Exibe os campos do fornecedor
          document.getElementById('fornecedorFields').style.display = 'block';
        }
      }
    }
  
    // Inicializa a exibição do tipo de conta sem nenhum tipo de conta selecionado
    toggleAccountType(currentType);
  
    // Event listeners para os botões de cliente e fornecedor
    document.getElementById('clienteBtn').addEventListener('click', function () {
      toggleAccountType(true); // Cliente
    });
  
    document.getElementById('fornecedorBtn').addEventListener('click', function () {
      toggleAccountType(false); // Fornecedor
    });
  });
  