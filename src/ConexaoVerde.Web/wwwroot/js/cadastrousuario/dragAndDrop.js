// Seleção de múltiplos elementos de upload e pré-visualização
const uploadAreas = document.querySelectorAll('.upload-area');
const fileInputs = document.querySelectorAll('.fotoPerfilInput');
const imagePreviewDivs = document.querySelectorAll('.image-preview');
const previewImages = document.querySelectorAll('.preview-img');

// Função para mostrar a imagem de pré-visualização
function displayImagePreview(file, previewImage, imagePreviewDiv) {
    const reader = new FileReader();
    reader.onload = function(event) {
        previewImage.src = event.target.result;  // Definindo o caminho da imagem
        imagePreviewDiv.classList.remove('hidden');  // Exibindo a div com a imagem
    };
    reader.readAsDataURL(file);  // Lendo a imagem como uma URL de dados (data URL)
}

// Função para manipulação do evento de arrastar
uploadAreas.forEach((uploadArea, index) => {
    const fileInput = fileInputs[index];
    const previewImage = previewImages[index];
    const imagePreviewDiv = imagePreviewDivs[index];

    // Manipulação do evento de arrastar
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
            displayImagePreview(file, previewImage, imagePreviewDiv); // Exibe a pré-visualização
        }
    });

    // Evento de seleção de arquivo (clicando)
    fileInput.addEventListener('change', function() {
        const file = fileInput.files[0];
        if (file) {
            displayImagePreview(file, previewImage, imagePreviewDiv); // Exibe a pré-visualização
        }
    });
});