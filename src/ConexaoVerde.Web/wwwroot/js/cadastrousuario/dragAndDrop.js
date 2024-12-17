// Referências aos elementos do DOM
const uploadArea = document.getElementById('upload-area');
const uploadMessage = document.getElementById('upload-message');
const fileInput = document.getElementById('fotoPerfilInput');
const previewImage = document.getElementById('preview-img');
const imagePreviewDiv = document.getElementById('image-preview');

// Função para mostrar a imagem de pré-visualização
function displayImagePreview(file) {
    const reader = new FileReader();
    reader.onload = function(event) {
        previewImage.src = event.target.result;  // Definindo o caminho da imagem
        imagePreviewDiv.classList.remove('hidden');  // Exibindo a div com a imagem
    };
    reader.readAsDataURL(file);  // Lendo a imagem como uma URL de dados (data URL)
}

// Função para manipulação do evento de arrastar
uploadArea.addEventListener('dragover', function(event) {
    event.preventDefault();
    uploadArea.classList.add('border-green-500');
    uploadArea.classList.remove('border-gray-300');
});

uploadArea.addEventListener('dragleave', function() {
    uploadArea.classList.remove('border-green-500');
    uploadArea.classList.add('border-gray-300');
});

uploadArea.addEventListener('drop', function(event) {
    event.preventDefault();
    uploadArea.classList.remove('border-green-500');
    uploadArea.classList.add('border-gray-300');
    const file = event.dataTransfer.files[0];
    if (file) {
        fileInput.files = event.dataTransfer.files; // Adiciona o arquivo ao input
        displayImagePreview(file); // Exibe a pré-visualização
    }
});

// Evento de seleção de arquivo (clicando)
fileInput.addEventListener('change', function() {
    const file = fileInput.files[0];
    if (file) {
        displayImagePreview(file); // Exibe a pré-visualização
    }
});