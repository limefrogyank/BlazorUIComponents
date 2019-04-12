window.singleInputModalView = {

    initialize: function (element, pageRef) {



        $(element).on('hide.bs.modal', function (e) {
            pageRef.invokeMethodAsync('BlazorUIComponents.Controls', 'NotifySingleInputModalViewHidden');
        });

    },


    showModal: function (element) {
        console.log('Here is the element');
        console.log(element);
        $(element).modal('show');
    },

    hideModal: function (element) {
        $(element).modal('hide');
    }



};
