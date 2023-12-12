$(document).ready(function () {
    $(document).on('submit', '.ajax-form', function (event) {
        var $form = $(this);
        var grid = window[$(this).data('grid')];
        var popupItem = $(this).data('popup');
        var functionName = window[$(this).data('function')];
        var $frmData = $form.serialize();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $frmData,
            async: true,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            processData: false,
            success: function (result) {
                var info = result.info;
                var error = result.error;
                var title = result.title;
                var url = result.url;
                var modal = result.modal;
                var urlTime = result.urlTime;
                var popup = result.popup;
                var newTap = result.newTap;
                var resultFunctionName = window[result.functionName];
                var Id = result.Id;
                toastr.clear();
                if (result.error) {
                    toastr.error(error, title);
                }
                else if (urlTime) {
                    toastr.success(info, title);
                    setTimeout(function () {
                        location.href = url;
                    }, 700);
                }
                else if (info) {
                    toastr.success(info, title);
                    if (grid) {
                        grid();
                    }
                }
                else if (url) {
                    if (newTap) {
                        window.open(url, 'name');
                    }
                    else {
                        location.href = url;
                    }
                }
                else if (title) {
                    toastr.success(info, title);
                }
                if (popup) {
                    if (popupItem) {
                        var el = document.querySelector("#" + popupItem);
                        var modal = tailwind.Modal.getOrCreateInstance(el);
                        modal.hide();                        
                    }                 
                }       
                if (functionName) {
                    functionName();
                }
                if (resultFunctionName) {
                    window[resultFunctionName];
                    resultFunctionName();
                }
            },
            complete: function () {
            }
        });
        return false;
    });
});
