﻿window.singleInputModalView = {

    initialize: function (element, pageRef) {

        $(element).on('hide.bs.modal', function (e) {
            pageRef.invokeMethodAsync('NotifySingleInputModalViewHidden');
        });

    },

    showModal: function (element) {
        $(element).modal('show');
    },

    hideModal: function (element) {
        $(element).modal('hide');
    }

};