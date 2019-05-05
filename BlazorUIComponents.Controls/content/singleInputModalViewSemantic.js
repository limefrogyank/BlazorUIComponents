window.singleInputModalViewSemantic = {

    initialize: function (element, pageRef) {

        $(element).modal({
            onHide: function () { pageRef.invokeMethodAsync('NotifySingleInputModalViewSemanticHidden'); return true; },

            onApprove: function () { pageRef.invokeMethodAsync('NotifySingleInputModalViewSemanticApproved'); return true; }
        });

       
    },

    showModal: function (element) {
        $(element).modal('show');
    },

    hideModal: function (element) {
        $(element).modal('hide');
    }

};
