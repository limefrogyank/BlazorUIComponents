window.confirmationModalView = {

    initialize: function (element, pageRef) {

        $(element).on('hide.bs.modal', function (e) {
            pageRef.invokeMethodAsync('NotifyConfirmationModalViewHidden');
        });

    },

    showModal: function (element) {
        $(element).modal('show');
    },

    hideModal: function (element) {
        $(element).modal('hide');
    }

};
