window.confirmationModalViewSemantic = {

    initialize: function (element, pageRef) {

        $(element).modal({
            onHide: function () { pageRef.invokeMethodAsync('NotifyConfirmationModalViewHiddenSemantic'); return true; }
        });

        //$(element).on('hide.bs.modal', function (e) {
        //    pageRef.invokeMethodAsync('NotifyConfirmationModalViewHidden');
        //});

    },

    showModal: function (element) {
        $(element).modal('show');
    },

    hideModal: function (element) {
        $(element).modal('hide');
    }

};
