// Lógica de interação das estrelas
document.querySelectorAll('.star').forEach(function (star) {
    star.addEventListener('mouseover', function () {
        const value = parseInt(this.getAttribute('data-value'));
        updateStarColors(value);
    });

    star.addEventListener('click', function () {
        const value = parseInt(this.getAttribute('data-value'));
        document.getElementById('ratingStars').setAttribute('data-rating', value); // Armazenar a avaliação
    });
});

function updateStarColors(rating) {
    document.querySelectorAll('.star').forEach(function (star, index) {
        if (index < rating) {
            star.classList.remove('text-gray-300');
            star.classList.add('text-yellow-500');
        } else {
            star.classList.remove('text-yellow-500');
            star.classList.add('text-gray-300');
        }
    });
}

// Atualiza as estrelas quando o formulário for enviado, mantendo a cor das estrelas selecionadas
function handleFormSubmit(event) {
    event.preventDefault(); // Previne o envio padrão

    const form = event.target;
    const formData = new FormData(form);
    const rating = document.getElementById('ratingStars').getAttribute('data-rating');
    const comentario = document.getElementById('comentario').value.trim();

    if (!rating || comentario === "") {
        Swal.fire({
            icon: 'error',
            title: 'Campos Incompletos',
            text: 'Por favor, preencha a avaliação e o comentário antes de enviar.',
            confirmButtonColor: '#E57373'
        });
        return false;
    }

    formData.append('Avaliacao', rating);

    Swal.fire({
        title: 'Enviando avaliação...',
        text: 'Por favor, aguarde enquanto processamos sua avaliação.',
        allowOutsideClick: false,
        didOpen: () => {
            toggleLoading(true);
            Swal.showLoading();
        }
    });

    fetch(form.action, {
        method: form.method,
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            Swal.close();

            if (data.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Avaliação Enviada!',
                    text: data.message,
                    confirmButtonColor: '#4CAF50'
                }).then(() => {
                    location.reload(); // Atualizar a página
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro',
                    text: data.message,
                    confirmButtonColor: '#E57373'
                });
            }
        })
        .catch(error => {
            Swal.close();
            Swal.fire({
                icon: 'error',
                title: 'Erro Inesperado',
                text: 'Algo deu errado. Tente novamente mais tarde.',
                confirmButtonColor: '#E57373'
            });
        });

    return false;
}


const submitButton = document.getElementById('submitButton');
const buttonText = document.getElementById('buttonText');
const loadingSpinner = document.getElementById('loadingSpinner');

function toggleLoading(state) {
    if (state) {
        buttonText.textContent = 'Enviando...';
        loadingSpinner.classList.remove('hidden');
        submitButton.disabled = true;
    } else {
        buttonText.textContent = 'Enviar Avaliação';
        loadingSpinner.classList.add('hidden');
        submitButton.disabled = false;
    }
}