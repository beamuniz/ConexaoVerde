//formatar Telefone
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
