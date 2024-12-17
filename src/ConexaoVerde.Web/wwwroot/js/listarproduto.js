    // Adiciona um evento de clique para todos os botões de excluir
    document.querySelectorAll('.excluirButton').forEach(function(button) {
    button.addEventListener('click', function (event) {
        event.preventDefault();  // Impede o envio imediato do formulário

        // Obtém o ID do produto do atributo data-produto-id
        var produtoId = this.getAttribute('data-produto-id');

        // SweetAlert para confirmar exclusão
        Swal.fire({
            title: 'Tem certeza?',
            text: "Você não poderá reverter isso!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3f4147',
            confirmButtonText: 'Sim, excluir!',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                // Se confirmado, cria o formulário e envia via POST
                var form = document.createElement('form');
                form.method = 'POST';
                form.action = '/Produto/DeletarProduto/' + produtoId;

                // Enviar o formulário para o servidor
                document.body.appendChild(form);

                // Captura a resposta ao enviar o formulário
                fetch(form.action, {
                    method: form.method,
                    body: new URLSearchParams(new FormData(form)),
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.sucesso) {
                            Swal.fire('Sucesso!', data.mensagem, 'success').then(() => {
                                // Redireciona ou recarrega a página após a exclusão
                                location.reload(); // Isso recarrega a página atual
                            });
                        } else {
                            Swal.fire('Erro!', 'Não foi possível excluir o produto.', 'error');
                        }
                    })
                    .catch(error => {
                        Swal.fire('Erro!', 'Algo deu errado ao excluir o produto.', 'error');
                    });
            }
        });
    });
});
