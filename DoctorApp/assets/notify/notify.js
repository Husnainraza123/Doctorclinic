/// <reference path="../content/assets/toastr-master/toastr.js" />
var stack_bottom_right = { "dir1": "up", "dir2": "left", "firstpos1": 25, "firstpos2": 25 };
toastCount = 0;
function Notify(type, message) {
    var shortCutFunction = type;
    var msg = message;
    var title =   '';
    var $showDuration = 300;
    var $hideDuration = 1000;
    var $timeOut = 5000;
    var $extendedTimeOut = 1000;
    var $showEasing ='swing';
    var $hideEasing = 'linear';
    var $showMethod = 'fadeIn';
    var $hideMethod = 'fadeOut';
    var toastIndex = toastCount++;

    toastr.options = {
        closeButton:true,
        debug: false,
        progressBar:true,
        positionClass: 'toast-bottom-right',
        preventDuplicates:true,
        onclick: null
    };




    if ($showDuration.length) {
        toastr.options.showDuration = $showDuration;
    }

    if ($hideDuration.length) {
        toastr.options.hideDuration = $hideDuration;
    }

    if ($timeOut.length) {
        toastr.options.timeOut = $timeOut;
    }

    if ($extendedTimeOut.length) {
        toastr.options.extendedTimeOut = $extendedTimeOut;
    }

    if ($showEasing.length) {
        toastr.options.showEasing = $showEasing;
    }

    if ($hideEasing.length) {
        toastr.options.hideEasing = $hideEasing;
    }

    if ($showMethod.length) {
        toastr.options.showMethod = $showMethod;
    }

    if ($hideMethod.length) {
        toastr.options.hideMethod = $hideMethod;
    }




    $("#toastrOptions").text("Command: toastr["
        + shortCutFunction
        + "](\""
        + msg
        + (title ? "\", \"" + title : '')
        + "\")\n\ntoastr.options = "
        + JSON.stringify(toastr.options, null, 2)
    );
    
   
    
    switch (type) {
        case 'danger':
            toastr.error(msg)

            break;
        case 'info':
            toastr.info(msg)
            break;
        case 'warning':
            toastr.warning(msg)
            break;
        case 'success':
            toastr.success(msg)
            break;
        
    }
    //new PNotify(opts);
}

//Toast Notifications
var notifyDanger = function (message) { Notify('danger', message); };
var notifyWarning = function (message) { Notify('warning', message); };
var notifyInfo = function (message) { Notify('info', message); };
var notifySuccess = function (message) { Notify('success', message); };
var notifyBlue = function (message) { Notify('primary', message); };

var notifyNoChanges = function (hasCancel) {
    hasCancel = hasCancel === undefined ? true : hasCancel;
    var msg = "No changes were made.";
    if (hasCancel)
        msg += "</br>Use Cancel to go back.";
    notifyDanger(msg);
};
