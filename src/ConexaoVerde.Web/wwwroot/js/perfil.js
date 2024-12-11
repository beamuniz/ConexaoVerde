document.getElementById('imageUpload').addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            // Atualiza a imagem de perfil com a nova imagem selecionada
            document.getElementById('profileImage').style.backgroundImage = `url(${e.target.result})`;
        };
        reader.readAsDataURL(file); // Lê a imagem selecionada
    }
});

document.getElementById('editProfileBtn').addEventListener('click', function () {
    const formFields = document.querySelectorAll('#profileForm input, #profileForm textarea');

    formFields.forEach(field => {
        if (field.type !== 'button' && field.type !== 'submit') {
            field.readOnly = false;
            if (field.type === 'file') {
                field.disabled = false;
            }
        }
    });
    document.getElementById('saveButton').classList.remove('hidden');
    document.getElementById('editProfileBtn').classList.add('hidden');
});

document.getElementById('saveButton').addEventListener('click', function () {
    document.getElementById('profileForm').submit();
});

window.onload = function () {
    const profileImage = document.querySelector("#profileImage img");

    if (profileImage) {
        profileImage.style.maxWidth = "120px";
        profileImage.style.maxHeight = "120px";
        profileImage.style.objectFit = "cover";
    }
};
