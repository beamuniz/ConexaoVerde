// Hamburguer
const hamburgerButton = document.getElementById('hamburger');
const menuItems = document.querySelector('.toggle.hidden');

hamburgerButton.addEventListener('click', () => {
    menuItems.classList.toggle('hidden');
});

// Perfil
document.getElementById('profileIcon').addEventListener('click', function (event) {
    event.stopPropagation();
    const dropdown = document.getElementById('dropdownMenu');
    dropdown.classList.toggle('hidden');
});
window.addEventListener('click', function (event) {
    const dropdown = document.getElementById('dropdownMenu');
    const profileIcon = document.getElementById('profileIcon');

    if (!profileIcon.contains(event.target) && !dropdown.contains(event.target)) {
        dropdown.classList.add('hidden');
    }
});
