//Code for showing and hiding modals in Bootstrap

function showModal(element) {
    console.log(element);
    $(element).modal('show');
}
function hideModal(element) {
    $(element).modal('hide');
}