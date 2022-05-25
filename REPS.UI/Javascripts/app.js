// REPS
// Version: 1.0.0
// Built: 09/01/2017

// This loads foundation plugins... Do not remove!
//$(document).foundation();

//#region Document Ready Function

// Load Javascripts when the document is ready (when everything has been loaded)
$(document).ready(function () {

    //#region "Please wait..." on Ajax requests
    $(document).ajaxSend(ShowLoader);
    $(document).ajaxComplete(HideLoader);
    $(document).ajaxError(HideLoader);
    //#endregion

    // toastr Options
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        //"timeOut": "5000",
        "timeOut": "0", //Set to 0 to prevent it from dismissing automatically
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


    //display Border after document is load - ajax call
    $(document).ajaxSend(HideBorder);
    $(document).ajaxComplete(ShowBorder);
    //end of display Border after document is load - ajax call



    // Close more-tabs panel when clicking the close icons.
    $('.more-tabs-close-icon').on('click', function () {
        $('#more-tabs-panel').removeClass('clicked');
        $('#more-tabs-arrow-top').removeClass('clicked');
    });
    //#endregion

    //#region Notification Panel

    // Make notification panel appears from the right side of the screen.
    $('#notifications').on('click', function () {
        $('#notifications-panel').addClass('clicked');
        //$('#content-wrapper').toggleClass('alerts-clicked');
        //$('#sidebar').addClass('alerts-clicked');
        $('.invisible-overlay').addClass('show');
        $('html').addClass('noscroll');
    });

    // Close notification panel when clicking on close icon.
    $('.notification-close-icon').on('click', function () {
        $('#notifications-panel').removeClass('clicked');
        $('#notifications-arrow-top').removeClass('show');
        $('#content-wrapper').removeClass('alerts-clicked');
        $('#sidebar').removeClass('alerts-clicked');
        $('html').removeClass('noscroll');
    });
    //#endregion

    //#region Invisible Overlay

    /* Close all popups and slides 
     * when invisible overlay is clicked
     */
    $('.invisible-overlay').on('click', function () {
        $('body').removeClass('noscroll');
        $('.invisible-overlay').removeClass('show');
        $('.overlay').removeClass('show');
        $('.profile-panel').removeClass('show');

        $('#notifications-panel').removeClass('clicked');
        $('#notifications-arrow-top').removeClass('show');

        $('#more-tabs-panel').removeClass('clicked');
        $('#more-tabs-arrow-top').removeClass('clicked');

        $('#sub-menu-slide').removeClass('clicked');
        $('#deals-menu').removeClass('clicked');

        $('#sidebar-profile').removeClass('clicked');
        $('#sidebar-profile span').removeClass('clicked');
        $('#sidebar-profile li').removeClass('clicked');

        $('#sidebar-profile span').removeClass('clicked');
        $('#sidebar-profile li').removeClass('clicked');

        $('#sidebar').removeClass('clicked');
        $('#content-wrapper').removeClass('clicked');

        $('html').removeClass('noscroll');
    });
    //#endregion

    //#region Mobile Menu
    $('.mobile-menu').on('click', function () {
        $('#sidebar').addClass('clicked');
        $('.invisible-overlay').addClass('show');
        $('#content-wrapper').addClass('clicked');
    });
    //#endregion Mobile Menu


});
// End $(document).ready(function
//#endregion

/*Display toaster message*/
function displayMessage(message, msgType) {
    if (msgType == "error") {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "0", //Set to 0 to prevent it from dismissing automatically
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }

    else {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }

    toastr[msgType](message);
};
/*Display toaster message*/


//auto geetings on dashboard
function Greeting() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    if (h < 12) {
        $('#greeting').html("Good Morning");
    } else if (h < 17) {
        $('#greeting').html("Good Afternoon");
    } else {
        $('#greeting').html("Good Evening");
    }
    $('#time').html(h + ":" + m + ":" + s);
    var t = setTimeout(Greeting, 500);
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i
    }; // add zero in front of numbers < 10
    return i;
}
//end of auto geetings on dashboard



//#region Generate Documents Button

/* This creates the Generate dropdown button for Documents page */

/**
 * Author: Kris Olszewski
 * CodePen: http://codepen.io/KrisOlszewski/full/wBQBNX
 */

;
(function ($, window, document, undefined) {
    'use strict';
    var $html = $('html');

    $html.on('click.ui.dropdown', '.js-dropdown', function (e) {
        e.preventDefault();
        $(this).toggleClass('is-open');
    });

    $html.on('click.ui.dropdown', '.js-dropdown [data-dropdown-value]', function (e) {
        e.preventDefault();
        var $item = $(this);
        var $dropdown = $item.parents('.js-dropdown');
        $dropdown.find('.js-dropdown__input').val($item.data('dropdown-value'));
        $dropdown.find('.js-dropdown__current').text($item.text());
    });

    $html.on('click.ui.dropdown', function (e) {
        var $target = $(e.target);
        if (!$target.parents().hasClass('js-dropdown')) {
            $('.js-dropdown').removeClass('is-open');
        }
    });

})(jQuery, window, document);
//#endregion

//#region General Functions
function ShowLoader() {
    $('.loader').addClass('show');
}

function HideLoader() {
    animate: false;
    $('.loader').removeClass('show');

    // Remove no scroll on body
    $('html').removeClass('noscroll');
}

function toogleOverlayShow() {
    $('.overlay').addClass('show');
    //$('html').addClass('noscroll');
}

function toogleOverlayHide() {
    $('.overlay').removeClass('show');
    //$('html').removeClass('noscroll');
}
//#endregion

//#region Upload Functionality

/* Upload functionality in pages like:
 * Correspondence, Workflow upload, etc...
 */
function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();

        reader.onload = function (e) {
            $('.image-upload-wrap').hide();
            $('.file-upload-image').attr('src', e.target.result);
            $('.file-upload-content').show();
            $('.image-title').html(input.files[0].name);
        };

        reader.readAsDataURL(input.files[0]);

    } else {
        removeUpload();
    }
}

function removeUpload() {
    $('.file-upload-input').replaceWith($('.file-upload-input').clone());
    $('.file-upload-content').hide();
    $('.image-upload-wrap').show();
}

$('.image-upload-wrap').bind('dragover', function () {
    $('.image-upload-wrap').addClass('image-dropping');
});

$('.image-upload-wrap').bind('dragleave', function () {
    $('.image-upload-wrap').removeClass('image-dropping');
});
//#endregion

//#region Workflow Form
// Disable/Enable Workflow fields
function ToggleAction(actionID, workflowVariableID) {
    var paramToggle;
    var togglename = 'ActionVariableToggle' + workflowVariableID;
    var IsRequiredDiv = 'IsRequiredDiv' + workflowVariableID;
    if ($('#' + togglename).is(":checked")) {
        paramToggle = true;
        $('#' + IsRequiredDiv).css('display', 'block');
    } else {
        paramToggle = false;
        $('#' + IsRequiredDiv).css('display', 'none');
    }

    AjaxControllerCaller('AdminWorkflow', 'ToggleAction', null, { 'actionID': actionID, 'workflowVariableID': workflowVariableID, 'toggle': paramToggle });
}
//#endregion

//#region Workflow Edit action screen to upload file 
function addFileData(input) {
    if (input.files && input.files[0]) {
        if (input.files[0].size >= fileUploadSize) {
            //toastr.warning('Your file size is: ' + input.files[0].size + ' and it is too large to upload! Please try to upload smaller file (1MB or less).', 'Warning');
            displayMessage('Your file size is: ' + input.files[0].size + ' and it is too large to upload! Please try to upload smaller file (1MB or less).', 'warning')
            removeUpload();
        } else if (input.files[0].name.length >= 150) {
            displayMessage('Your file name is too long', 'warning')
            removeUpload();
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                //attach file to object formdata
                for (var j = 0; j < input.files.length; j++) {
                    $('.image-upload-wrap').hide();
                    $('.file-upload-image').attr('src', e.target.result);
                    $('.file-upload-content').show();
                }
                $('.image-title').html(input.files[0].name);
            };
            reader.readAsDataURL(input.files[0]);
        }
    } else {
        removeUpload();
    }
}
//#endregion







//#region ajax begin form to upload files for users.
function SaveForm(inputFile, formID, controllerName, methodName, mainPageMethodName, targetClass, btnAction, parameters, msg) {

    $(btnAction).addClass("dirty-disabled"); // Removes 'onclick' btn if found

    var AppPath = globaljsPath + controllerName + "/" + methodName;
    //var AppPath = "/" + controllerName + "/" + methodName;

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {

        // Create FormData object  
        var fileData = new FormData();

        var fileUpload = $(inputFile).get(0);
        var files = fileUpload.files;

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append('attachedFile', files[i]); // Adding one more key to FormData object    
            fileData.append('attachedFileUIName', decodeURIComponent(fileUpload.name)); //get ui names of file
        }
        var editserialize = $(formID).serialize();
        editserialize = decodeURIComponent(editserialize) //decodeURIComponent : to get the exact value of each data which is encoded
        fileData.append('formDataValues', editserialize); //get ui form data to "formDataValues"


        var ajax = new XMLHttpRequest();//XMLHttpRequest is used to exchange data with server; it can update parts of web page without reloading the whole page
        if (mainPageMethodName != null) {
            ajax.onreadystatechange = function () {
                if (ajax.readyState == XMLHttpRequest.DONE) {
                    onSuccessToasterMsg(controllerName, mainPageMethodName, targetClass, parameters, msg) //update sidebar count before pop up menu closed
                    onComplete()
                }
            }
        }
        ajax.open("POST", AppPath, true);
        ajax.send(fileData);


        /* $.ajax({
             url: AppPath, //web service
             type: "POST",
             contentType: false, // Not to set any content header  
             processData: false, // Not to process data  
             data: fileData,
             dataType: "json",
             complete: function (response) {
                 onSuccessToasterMsg(controllerName, mainPageMethodName, targetClass, parameters, msg) //update sidebar count before pop up menu closed
                 onComplete()
             }
         });*/
    } else {
        //toastr.warning('FormData is not supported.', 'Warning');
        displayMessage('FormData is not supported.', 'warning')
    }
}
//#endregion



//#region Workflow Add/Edit screen to upload files for users.
function SaveAction(inputFile, formID, controllerName, methodName, successMethodName, targetClass, sidebarSubmenuMethod, sidebarSubmenuTargetClass, btnUpdate) {
    $(btnUpdate).addClass("dirty-disabled"); // Removes 'onclick' property if found
    // var AppPath = globaljsPath + controllerName + "/" + methodName;
    var AppPath = globaljsPath + controllerName + "/" + methodName;

    //hide upload box
    $('.image-upload-wrap').show();
    $('.file-upload-content').hide();
    //end of hide upload box

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {
        // Create FormData object  
        var fileData = new FormData();

        var fileUpload = $(inputFile).get(0);
        if (fileUpload !== undefined) {
            var files = fileUpload.files;
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append('attachedFile', files[i]); // Adding one more key to FormData object    
                fileData.append('attachedFileUIName', decodeURIComponent(fileUpload.name)); //get ui names of file
            }
        }

        var editserialize = $(formID).serialize();
        editserialize = decodeURIComponent(editserialize) //decodeURIComponent : to get the exact value of each data which is encoded
        fileData.append('formDataValues', editserialize); //get ui form data to "formDataValues"


        var ajax = new XMLHttpRequest();//XMLHttpRequest is used to exchange data with server; it can update parts of web page without reloading the whole page
        ajax.onreadystatechange = function () {
            if (ajax.readyState == XMLHttpRequest.DONE) {
                onSuccessToasterMsg(controllerName, sidebarSubmenuMethod, sidebarSubmenuTargetClass, null, null) //update sidebar count before pop up menu closed
                onCompleteWorkflow(controllerName, successMethodName, targetClass, { 'TaskID': ajax.responseText }) //update div before pop up menu closed
            }
        }

        if ((fileUpload !== undefined) && (files.length > 0)) {
            ajax.upload.addEventListener('progress', progress, false);
        }
        ajax.open("POST", AppPath, true);
        ajax.send(fileData);



        //$.ajax({
        //    url: AppPath, //web service
        //    type: "POST",
        //    contentType: false, // Not to set any content header  
        //    processData: false, // Not to process data  
        //    data: fileData,
        //    dataType: "json",
        //    xhr: function () {
        //        var myXhr = $.ajaxSettings.xhr();
        //        if (myXhr.upload && (fileUpload !== undefined) && (files.length > 0)) {
        //            myXhr.upload.addEventListener('progress', progress, false); //call progress function
        //        }
        //        return myXhr;
        //    },
        //    complete: function (response) {
        //        onSuccessToasterMsg(controllerName, sidebarSubmenuMethod, sidebarSubmenuTargetClass, null, null) //update sidebar count before pop up menu closed
        //        onCompleteWorkflow(controllerName, successMethodName, targetClass, {
        //            'TaskID': response.responseText
        //        }) //update div before pop up menu closed
        //    }
        //});



    } else {
        //toastr.warning('FormData is not supported.', 'Warning');
        displayMessage('FormData is not supported.', 'warning');
    }
}
//#endregion



//update progress bar on event (e) click
function progress(e) //e : passed to event handlers "progress event"
{
    if (e.lengthComputable) {
        var max = e.total;
        var current = e.loaded;
        var Percentage = (current * 100) / max;

        if (Percentage >= 100) {
            $('.fileProgressbar').height(Percentage + '%'); //update progressbar percent complete
            $('.fileProgressbar').html(Percentage + '%'); //update progressbar percent complete
        }
    }
}

//end of update progress bar on event (e) click



//workflow: update div n side bar close pop up 
function onCompleteWorkflow(ControllerName, MethodName, targetDiv, ObjectParameter) {
    var htmlResult = AjaxControllerCaller(ControllerName, MethodName, targetDiv, ObjectParameter);
    $('.general-panel').toggleClass('show');
    toogleOverlayHide();
    // Clear popup content
    $('.general-panel').html('');
}
//End of workflow: update div n side bar close pop up 

/***************************************/
/*   END Upload                        */
/***************************************/

// Disable/Enable Workflow fields Required value
function ToggleActionRequired(actionID, workflowVariableID) {
    var paramToggle;
    var togglename = 'ActionVariableRequiredToggle' + workflowVariableID;
    if ($('#' + togglename).is(":checked")) {
        paramToggle = true;
    } else {
        paramToggle = false;
    }

    AjaxControllerCaller('AdminWorkflow', 'ToggleActionRequired', null, { 'actionID': actionID, 'workflowVariableID': workflowVariableID, 'toggle': paramToggle });
}

// Disable/Enable Workflow fields Required value
function ToggleActionMandatory(actionID) {
    var paramToggle;
    if ($('#ActionMandatoryToggle').is(":checked")) {
        paramToggle = true;
    } else {
        paramToggle = false;
    }

    AjaxControllerCaller('AdminWorkflow', 'ToggleActionMandatory', null, { 'actionID': actionID, 'toggle': paramToggle });
}

function RemoveActionPopup(actionID, workflowTaskID, workflowID, taskID) {
    if (confirm("Are you sure you want to remove the action?") == true) {


        var AppPath = globaljsPath + "AdminWorkflow/DeleteAction";

        var fileData = new FormData();

        fileData.append('actionID', actionID);
        fileData.append('workflowTaskID', workflowTaskID);
        fileData.append('workflowID', workflowID);
        fileData.append('taskID', taskID);

        var ajax = new XMLHttpRequest(); //XMLHttpRequest is used to exchange data with server; it can update parts of web page without reloading the whole page
        ajax.onreadystatechange = function () {
            if (ajax.readyState == XMLHttpRequest.DONE) {
                //toastr.success('Action has been successfully removed', 'Successful');
                displayMessage('Action has been successfully removed.', 'success');
                onComplete();
                $('#content-wrapper').html(ajax.responseText);
            }
        }

        ajax.open("POST", AppPath, true);
        ajax.send(fileData);
    }
}


function AssignTaskToWorkflow(taskID, workflowID) {
    AjaxControllerCaller('AdminWorkflow', 'AssignTaskToWorkflow', null, { 'taskID': taskID, 'workflowID': workflowID });
}

function ChangeUserWorkflow(taskGUID) {
    AjaxControllerCaller('MyProfile', 'ChangeUserWorkflow', null, { 'taskGUID': taskGUID });
}

function UnAssignTaskFromWorkflow(taskID, workflowID) {

    AjaxControllerCaller('AdminWorkflow', 'UnassignTaskFromWorkflow', null, { 'taskID': taskID, 'workflowID': workflowID });
}

function AssignActionToTask(actionID, workflowTaskID) {

    AjaxControllerCaller('AdminWorkflow', 'AssignActionToTask', null, { 'actionID': actionID, 'workflowTaskID': workflowTaskID });
}

function UnAssignActionFromTask(actionID, workflowTaskID) {

    AjaxControllerCaller('AdminWorkflow', 'UnAssignActionFromTask', null, { 'actionID': actionID, 'workflowTaskID': workflowTaskID });
}

function AssignRoleActionToRole(actionID, roleID, isActive) {

    AjaxControllerCaller('AdminRoles', 'AssignRoleActionToRole', null, { 'actionID': actionID, 'roleID': roleID, 'isActive': isActive });
}

//Generate swift file in the path found in web.config - View Deal page
function GenerateSwiftFile() {
    if (confirm("Are you sure you want to generate a SWIFT transfer file for this deal?") == true) {

        var AppPath = globaljsPath + "Swift/CreateSwiftFile";

        var ajax = new XMLHttpRequest(); //XMLHttpRequest is used to exchange data with server; it can update parts of web page without reloading the whole page
        ajax.onreadystatechange = function () {
            if (ajax.readyState == XMLHttpRequest.DONE) {
                if (ajax.responseText == "ok") {
                    //toastr.success('SWIFT transfer request has been successfully initiated', 'Successful');
                    displayMessage('SWIFT transfer request has been successfully initiated.', 'success');
                }
                else {
                    //toastr.error('An error has occured. Please retry the action. Error:' + ajax.responseText, 'Error');
                    displayMessage('An error has occured. Please retry the action. Error:' + ajax.responseText, 'error');
                }
            }
        }

        ajax.open("POST", AppPath, true);
        ajax.send();
    }
}


//#region Date JS Store to Database

/* Store date and time formatted to db */
function FormatActionDatePS() {
    $('#DateTimeValue').val($('#datePickerEditPanel').val() + " " + $('#timePickerEditPanel').val());
};

/* Store date and time formatted to db */
function FormatFeeDatePS() {
    $('#DateTimeValue').val($('#datePickerFeePanel').val() + " " + $('#timePickerFeePanel').val());
};
//#endregion

//#region Call Controller Method
/* Call Controller Method and set result into resultDivID  */
function AjaxControllerCaller(controllerName, methodName, resultDivID, param) {
    var AppPath = globaljsPath + controllerName + "/" + methodName;
    $.ajax({
        type: "POST",
        url: AppPath,
        cache: false,
        content: "application/json; charset=utf-8",
        dataType: "html",
        data: param,
        async: true,
        //data: JSON.stringify(data),
        success: function (result) {
            if (result == "Invalidreference") { // Changing the workflow also returns Invalidreference so that the user gets redirected to the dashboard
                window.location.href = globaljsPath + "Dashboard";
            } else if (result == "WrongToken") {
                window.location.href = globaljsPath + "Login/Logout?tokenerror=true";
            } else {
                $('#' + resultDivID).html(result);

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // TODO: Show error
        },
        ajaxStart: function () {
            ShowLoader();
        },
        ajaxStop: function () {
            HideLoader();
        }

    });
}
//#endregion

//Prompts Confirmation to Return View Error Page Function from ErrorHandlingController Method
function ErrorPage(controllerName, methodName) {
    // do something where you can call an action method in controller to pass some data via AJAX() request
    toastr.warning("<br /><br /><button type='button' id='confirmationRevertYes' class='btn clear'>Contact</button>", 'Contact The Person?', {
        closeButton: false,
        allowHtml: true,
        onShown: function (toast) {
            $("#confirmationRevertYes").click(function () {
                var url = globaljsPath + controllerName + "/" + methodName;
                window.location.href = url;
            });
        }
    });
}
//End of Prompts Confirmation to Return View Error Page Function from ErrorHandlingController Method

//Admin user - Ask user to reset password if account already exists in DB
function promptResetPassword(data) {
    if (data != "no") {
        var r = confirm("The user account already exists and has been reactivated. Press 'Ok' to reset the password or 'Cancel' to use the same password.");
        if (r == true) {
            AjaxControllerCaller('AdminUser', 'ResetUserPassword', 'user-details', '{ email: data, reset: true }')
        } else {
            AjaxControllerCaller('AdminUser', 'ReturnPartialUserDetails', 'user-details', null)
        }
    } else {
        AjaxControllerCaller('AdminUser', 'ReturnPartialUserDetails', 'user-details', null)
    }
}
//end of Admin user - Ask user to reset password if account already exists in DB


// Disable/Enable Header Tabs
function ToggleTabHeader() {
    var paramToggle;
    var clearAllTabs = false;
    if ($('#HeaderTabToggle').is(":checked")) {
        paramToggle = 'on'

        //alert("ON");
    } else {
        //$("#HeaderTabToggle").prop("checked", true);
        paramToggle = 'off'
        var box = confirm("Are you sure you want to do this?");
        if (box == true) {
            clearAllTabs = true;
        } else {
            $('#HeaderTabToggle').prop('checked', true);
            return false;
        }
        //alert("OFF");
    }

    AjaxControllerCaller('HeaderTab', 'ToggleHeaderTabsActivation', null, { 'ActivationStatus': paramToggle, 'clearAllTabs': clearAllTabs });

}

// Change Alert Status
function changeAlertStatus(id, status, resultDivID, filter) {
    var msg = "";
    var toastrMsg = "";
    if (status == 2) {
        msg = "complete";
        toastrMsg = "completed";
    } else if (status == 4) {
        msg = "archive";
        toastrMsg = "archived";
    }
    if (confirm('Are you sure you want to ' + msg + ' this alert?')) {

        var AppPath = globaljsPath + "Alerts/ChangeEventStatusJavascript";
        // Create FormData object  
        var fileData = new FormData();
        fileData.append('alertsGUID', id);
        fileData.append('StatusID', status);
        fileData.append('filter', filter);

        var ajax = new XMLHttpRequest(); //XMLHttpRequest is used to exchange data with server; it can update parts of web page without reloading the whole page
        ajax.onreadystatechange = function () {
            if (ajax.readyState == XMLHttpRequest.DONE) {
                onSuccessToasterMsg('Alerts', 'RenderPartialAlertsDiaryItems', 'notifications-panel', null, null); // We update the alerts diary item panel
                onSuccessToasterMsg('Alerts', 'UpdateNotificationCount', 'notifications', null, null); // We update the alerts notification count
                $('#' + resultDivID).html(ajax.responseText);
            }
        }

        ajax.open("POST", AppPath, true);
        ajax.send(fileData);

        //toastr.success("Alert has been successfully " + toastrMsg, 'Successful');
        displayMessage("Alert has been successfully " + toastrMsg, "success");
    } else {
        return false;
    }
}

//#region Get URL Parameter
function getURLParameter(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [null, ''])[1].replace(/\+/g, '%20')) || null;
}
//#endregion

//#region Profile Validation Form
$('#NewPassword, #ConfirmPassword').on('keyup', function () {
    if ($('#NewPassword').val() == $('#ConfirmPassword').val()) {
        $('#PasswordMessage').html('Password Matched').css('color', 'green');
        if ($('#NewPassword').val().length > 0) {
            $('#UpdatePasswordBtn').removeAttr('disabled');
        }
    } else {
        $('#PasswordMessage').html('Password Not Matched').css('color', 'red');
        $('#UpdatePasswordBtn').attr('disabled', 'disabled');
    }
});
//#endregion

//#region Popup General Panel
/* popupClass:to resize popup according to content */
function CallPopupPanel(targetController, targetAction, targetPanel, paramObject, popupClass) {
    $('#' + targetPanel).html("");

    //check if the variable has a truthy value or not
    if (popupClass == undefined) {
        $('#' + targetPanel).removeClass().addClass('general-panel show general-panel-border');
    }
    else {
        $('#' + targetPanel).addClass('show' + " " + popupClass);
    }

    $('.overlay').toggleClass('show');

    // Add no scroll on body
    $('html').addClass('noscroll');

    if (targetController != null || targetAction != null) {
        AjaxControllerCaller(targetController, targetAction, targetPanel, paramObject);
    }
};
//#endregion

//#region Set Selected Tab Active 
function SetSelectedTabActive(TabName) {
    //Remove Active class for previously seleted tab
    $('.sub-tabs-section').find('a').each(function () {
        $(this).removeClass("sub-tabs-active");
    });

    //Set Selected Tab Active
    $("#" + TabName).addClass("sub-tabs-active");
}
//#endregion

//#region Set Selected Workflow Admin Tab Active
function SetSelectedWorkflowTabActive(TabName, IdName) {
    //Remove Active class for previously seleted tab
    $("#" + IdName).find('a').each(function () {
        $(this).removeClass("sub-tabs-active");
    });

    //Set Selected Tab Active
    $("#" + TabName).addClass("sub-tabs-active");
}
//#endregion

//#region Set Selected Correspondence Tab Active
function SetSelectedTabActiveCorrespondence(TabName) {
    //Remove Active class for previously seleted tab
    $('.workflow-tab-section').find('li').each(function () {
        $(this).removeClass("workflow-tab-selected");
    });

    //Set Selected Tab Active
    $("#" + TabName).addClass("workflow-tab-selected");
}
//#endregion


//load detail of deals on pop up form when click ("Edit") event from view deal 
var ajaxSuccess = function (ActionMethodName, Controller, targetClass, paramObject, popupClass) {
    var popupPanel = CallPopupPanel(Controller, ActionMethodName, targetClass, paramObject, popupClass);

}
//end of load detail of deals on pop up form when click ("Edit") event from view deal 
//ajax validation

//#region Close popup general panel after form has been saved
function onComplete() {
    $('.general-panel').toggleClass('show');
    //$('.overlay').toggleClass('show');
    //HideLoader();
    toogleOverlayHide();
    // Clear popup content
    $('.general-panel').html('');

    // Remove no scroll from body
    $('html').removeClass('noscroll');

}
//#endregion


//display toaster messages && load div after removed success 
function SuccessResult(data, ControllerName, MethodName, targetDiv, ObjectParameter, msg) {
    ////make ajax call 
    var myDiv = document.querySelector('#my-div');
    var result = "";
    if (myDiv != null) {
        result = parseInt(myDiv.dataset.info);
    }

    switch (result) {
        case 1:
            toastr.warning("Error has occur", 'Warning');
            break;
        case 9:
            if (ControllerName != null || MethodName != null) {
                var htmlResult = AjaxControllerCaller(ControllerName, MethodName, targetDiv, ObjectParameter);
                toastr.error("<br/><a href='#' id='confirmationRevertYes' class='send-error-report-button clear'>Send Email</a>", "Error has occur<br/>Please Contact Admin.",
                       {
                           closeButton: false,
                           allowHtml: true,
                           onShown: function (toast) {
                               $("#confirmationRevertYes").click(function () {
                                   var url = globaljsPath + 'ErrorHandling/ErrorForm?errorMsg=CouldnotSaveData';
                                   window.location.href = url;
                               });
                           }
                       });
            } else {
                toastr.error("<br/><a href='#' id='confirmationRevertYes' class='send-error-report-button clear'>Send Email</a>", "Error has occur<br/>Please Contact Admin.",
                       {
                           closeButton: false,
                           allowHtml: true,
                           onShown: function (toast) {
                               $("#confirmationRevertYes").click(function () {
                                   var url = globaljsPath + 'ErrorHandling/ErrorForm?errorMsg=CouldnotSaveData';
                                   window.location.href = url;
                               });
                           }
                       });
            }
            break;

        default:
            if (ControllerName != null || MethodName != null) {
                var htmlResult = AjaxControllerCaller(ControllerName, MethodName, targetDiv, ObjectParameter);
                if (msg != null) {
                    displayMessage(msg, 'success');
                }
            } else {
                if (msg != null) {
                    displayMessage(msg, 'success');
                }
            }
            break;
    }
}
//end of display toaster messages && load div after removed success 










//display toaster messages && load div after removed success 
function onSuccessToasterMsg(ControllerName, MethodName, targetDiv, ObjectParameter, msg) {
    ////make ajax call 
    //if (data.success ==true)
    //{
    if (ControllerName != null || MethodName != null) {
        var htmlResult = AjaxControllerCaller(ControllerName, MethodName, targetDiv, ObjectParameter);
        if (msg != null) {
            //toastr.success(msg, 'Successful');
            displayMessage(msg, 'success');
        }
    } else {
        if (msg != null) {
            //toastr.success(msg, 'Successful');
            displayMessage(msg, 'success');
        }
    }
    //}
    //else
    //{
    //    toastr.error("Error has occured", 'Error');
    //}
}
//end of display toaster messages && load div after removed success 

function OnSuccessDealCreated(result) {
    if (result) {
        onComplete();
        var obj = eval('(' + result.parameter + ')');
        //onSuccessToasterMsg(obj.controller, obj.methodName, null,  obj.UREF, obj.msg)
        window.location.href = obj.url;
        //toastr.success(obj.msg, 'Successful');
        displayMessage(obj.msg, 'success');
    }
}


//display toaster messages && load div after removed success 
function onSuccessForHeaderTabs(ControllerName, firstMethodName, secondMethodName, firstTargetDiv, secondTargetDiv, ObjectParameter, msg) {
    //make ajax call 
    if (ControllerName != null || firstMethodName != null) {
        var htmlResult = AjaxControllerCaller(ControllerName, firstMethodName, firstTargetDiv, ObjectParameter);
        if (msg != null) {
            //toastr.success(msg, 'Successful');
            displayMessage(msg, 'success');
        }
    }
    //make ajax call 
    if (ControllerName != null || secondMethodName != null) {
        var htmlResult = AjaxControllerCaller(ControllerName, secondMethodName, secondTargetDiv, ObjectParameter);
        if (msg != null) {
            //toastr.success(msg, 'Successful');
            displayMessage(msg, 'success');
        }
    } else {
        if (msg != null) {
            //toastr.success(msg, 'Successful');
            displayMessage(msg, 'success');
        }
    }
}
//end of display toaster messages && load div after removed success 

function toastrConfirmBox() {
    toastr.error('Are you sure you want to remove? :', 'Warning', {
        closeButton: true
    });
}
//end of ajax validation

//on failure 
function OnFailure(xhr, status) {
    //toastr.error('Error: ' + xhr.statusText);
    displayMessage('Error: ' + xhr.statusText, 'error');
}



///Validation parameter = to validate or not the form
//submit form without validation (when the submit event is an anchor tag "link")
function FormObjectSubmit(btnSubmitID, formObjectDivID, validation) {
    ///TO diasble button
    $(btnSubmitID).addClass("dirty-disabled"); // add dirty disabled

    if (validation) {
        // check if al fields are well filled
        var validationResult = reps.checkOnSubmit(formObjectDivID);
        //if success validationResult return true
        if (validationResult) {
            $(formObjectDivID).submit();
        } else {
            ///TO enable in case form not well filled (validationResult= false)
            $(btnSubmitID).removeClass("dirty-disabled"); // remove dirty disabled
        }
    } else {
        $(formObjectDivID).submit();
    }
    //toogleOverlayHide();
}
//submit form without validation (when the submit event is an anchor tag "link")


//submit form without validation (when the submit event is an anchor tag "link")
function FormExistingParticipantSubmit(resultBoxID, btnSubmitID, formObjectDivID, validation) {
    ///TO diasble button
    $(btnSubmitID).addClass("dirty-disabled"); // add dirty disabled
    var searchResult = $(resultBoxID + " li:first").val();
    if (searchResult > 0) {
        if (validation) {
            // check if al fields are well filled
            var validationResult = reps.checkOnSubmit(formObjectDivID);
            //if success validationResult return true
            if (validationResult) {
                $(formObjectDivID).submit();
            } else {
                ///TO enable in case form not well filled (validationResult= false)
                $(btnSubmitID).removeClass("dirty-disabled"); // remove dirty disabled
            }
        }
        else {
            $(formObjectDivID).submit();
        }
        //toogleOverlayHide();
    }
    else {
        //toastr.warning("Participant Not Found", 'Invalid Field');
        displayMessage('Invalid Field, Participant Not Found', 'warning');
        $(btnSubmitID).removeClass("dirty-disabled"); // add dirty disabled
    }
}
//submit form without validation (when the submit event is an anchor tag "link")







///Validation parameter = to validate or not the form
//submit form without validation (when the submit event is an anchor tag "link")
function FormObjectSubmitWithoutDirty(formObjectDivID) {
    $(formObjectDivID).submit();
}
//submit form without validation (when the submit event is an anchor tag "link")


//submit form and make ajax call 
function ReportFormObjectSubmit(formObjectDivID, ControllerName, firstMethodName, firstTargetDiv, ObjectParameter, validation) {
    if (validation) {
        var validationResult = reps.checkOnSubmit(formObjectDivID);
        if (validationResult) {
            ObjectParameter = unescape($(formObjectDivID).serialize()); //unescape : to get "/"
            var input;
            AjaxControllerCaller(ControllerName, firstMethodName, firstTargetDiv, {
                input: ObjectParameter
            });
        }
    } else {
        $(formObjectDivID).submit();
    }
    //toogleOverlayHide();
}
//end of submit form and make ajax call 


////submit form and make ajax call 
function GenerateReportFormObjectSubmit(formObjectDivID, ControllerName, firstMethodName, firstTargetDiv, ObjectParameter, validation) {
    if (validation) {
        var validationResult = reps.checkOnSubmit(formObjectDivID);
        if (validationResult) {
            ObjectParameter = unescape($(formObjectDivID).serialize()); //unescape : to get "/"
            //var input;
            var AppPath = globaljsPath + ControllerName + "/" + firstMethodName + "?input=" + encodeURIComponent(ObjectParameter);
            window.open(AppPath, "downloadiframe");
        }


    }
    //toogleOverlayHide();
}
//end of submit form and make ajax call 

/**************************************************************/
//**  End of call ajax method for editting without refresh  **//
/*************************************************************/


// Close all popups and slides when invisible overlay is clicked
//$('.invisible-overlay').on('click', function () {
//    $('.invisible-overlay').removeClass('show');
//    $('.overlay').removeClass('show');
//    $('.profile-panel').removeClass('show');

//    $('#notifications-panel').removeClass('clicked');
//    $('#notifications-arrow-top').removeClass('show');

//    $('#more-tabs-panel').removeClass('clicked');
//    $('#more-tabs-arrow-top').removeClass('clicked');

//    $('#sub-menu-slide').removeClass('clicked');
//    $('#deals-menu').removeClass('clicked');

//    $('#sidebar-profile').removeClass('clicked');
//    $('#sidebar-profile span').removeClass('clicked');
//    $('#sidebar-profile li').removeClass('clicked');

//    $('#sidebar-profile span').removeClass('clicked');
//    $('#sidebar-profile li').removeClass('clicked');

//    $('#sidebar').removeClass('clicked');
//});




// Close upload panel on click.
$('.upload-panel i').on('click', function () {
    $('.upload-panel').toggleClass('show');
    $('.overlay').toggleClass('show');
});



//reps function//
var reps = {
    // Dropdown Participant Form
    dropdownParticipantType: function (obj) {
        if ($(obj).val() == 10) {
            $('#participant-individual').show();
            $('#participant-individual-footer-btn').show();
            $('.company-form').hide();
            $('#participant-company-footer-btn').hide();
        } else if ($(obj).val() == 20) {
            $('.company-form').show();
            $('#participant-individual').hide();
            $('#participant-company-footer-btn').show();
            $('#participant-individual-footer-btn').hide();
        } else {
            //toastr.error('Please select a type');
            displayMessage('Please select a type', 'error');
        }
        return false;
    }, // End Of Dropdown Participant Form

    //display n hide participant 
    dropdownParticipantNewExist: function (obj) {
        //var select = document.getElementById("mySelect");
        //var answer = select.options[select.selectedIndex].value;

        if ($(obj).val() == 0) {
            $('#newparticpant').show();
            $('#newParticipant-footer-btn').show();
            $('#existParticipant-footer-btn').hide();
            $('#existingparticipant').hide();
            $(obj).val(-1);
        } else if ($(obj).val() == -1) {
            $('#newparticpant').hide();
            $('#existingparticipant').show();
            $('#newParticipant-footer-btn').hide();
            $('#existParticipant-footer-btn').show();
            $(obj).val(0);
        } else {
            $('#newparticpant').show();
            $('#newParticipant-footer-btn').show();
            $('#existParticipant-footer-btn').hide();
            $('#existingparticipant').hide();
            $(obj).val(-1);
        }
    },
    //end of display n hide participant 

    //check form validation login
    checkOnSubmitLogin: function (obj) {
        var cntRequired = 0;
        var errorMessage = '';
        var validationerror = false;
        //get all objects to be validated
        $(obj).find("select , input ").each(function (index, item) {
            if ($(item).is(':visible')) {
                validationerror = false;
                switch ($(item).attr('type')) {
                    case 'text':
                        if ($(item).attr("data-content") == "required") {
                            if ($(item).val().length <= 0) {
                                errorMessage = "Invalid Password or Email Address";
                                validationerror = true;
                            }
                            if ($(item).attr("format") != undefined) {
                                if ($(item).val().length > 0) {
                                    if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                        errorMessage = "Invalid Password or Email Address";
                                        validationerror = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "password":
                        if ($(item).val().trim().length > 0 && $(item).attr("data-content") == undefined) {
                            if ($(item).attr("format") != undefined) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    validationerror = true;
                                }
                            }
                        }

                        if ($(item).attr("data-content") == "required") {
                            if ($(item).val().trim().length <= 0) {
                                errorMessage = "Invalid Password or Email Address";
                                validationerror = true;
                            }
                            if (($(item).attr("format") != undefined) && ($(item).val().trim().length > 0)) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    validationerror = true;
                                }
                            }
                        }
                        break;
                }

                if (validationerror == true) {
                    reps.setWorng($(item).attr('id'));
                    // prevent default submit action    
                    //event.preventDefault();
                    cntRequired++;
                }
            }
        });
        if (cntRequired == 0) {
            return true;
        } else {
            if (errorMessage) {
                //toastr.error(errorMessage);
                displayMessage(errorMessage, 'error');
            }
            return false;
        }
    },
    //end of check form validation login

    //check form validation
    checkOnSubmit: function (obj) {
        var cntRequired = 0;
        var errorMessage = '';
        var validationerror = false;
        //get all objects to be validated
        $(obj).find("select , input , textarea ").each(function (index, item) {

            if ($(item).is(':visible')) {
                validationerror = false;
                switch ($(item).attr('type')) {

                    case 'text':
                        if ($(item).val().trim().length > 0 && $(item).attr("data-content") == undefined) {
                            if ($(item).attr("format") != undefined) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                    errorMessage = reps.validateformat($(item).attr("format"), $(item).val());
                                    validationerror = true;
                                }
                            }
                        }

                        if ($(item).attr("data-content") == "required") {

                            if ($(item).val().trim().length <= 0 && ($(item).attr("format") == undefined)) {
                                $(item).attr("placeholder", "Enter Text");
                                validationerror = true;
                            }

                            if ($(item).val().trim().length <= 0 && ($(item).attr("format") != undefined)) {
                                $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                validationerror = true;
                            }
                            if (($(item).attr("format") != undefined) && ($(item).val().trim().length > 0)) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                    errorMessage = reps.validateformat($(item).attr("format"), $(item).val());
                                    validationerror = true;
                                }
                            }
                        }
                        break;

                    case 'file':
                        if ($(item).val().trim().length > 0 && $(item).attr("data-content") == undefined) {
                            if ($(item).attr("format") != undefined) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                    errorMessage = reps.validateformat($(item).attr("format"), $(item).val());
                                    validationerror = true;
                                }
                            }
                        }

                        if ($(item).attr("data-content") == "required") {
                            if ($(item).val().trim().length <= 0 && ($(item).attr("format") == undefined)) {
                                $(item).attr("placeholder", "Upload file");
                                validationerror = true;
                            }
                            if ($(item).val().trim().length <= 0 && ($(item).attr("format") != undefined)) {
                                $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                validationerror = true;
                            }
                            if (($(item).attr("format") != undefined) && ($(item).val().trim().length > 0)) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                    errorMessage = reps.validateformat($(item).attr("format"), $(item).val());
                                    validationerror = true;
                                }
                            }
                        }
                        break;

                    case 'checkbox':
                        if ($(item).attr("data-content") == "required" && ($(item).prop('checked'))) {
                            validationerror = true;
                        }
                        break;

                    case "password":
                        if ($(item).val().trim().length > 0 && $(item).attr("data-content") == undefined) {
                            if ($(item).attr("format") != undefined) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    validationerror = true;
                                }
                            }
                        }

                        if ($(item).attr("data-content") == "required") {
                            if ($(item).val().trim().length <= 0) {
                                errorMessage = "Please fill in the required fields";
                                validationerror = true;
                            }
                            if (($(item).attr("format") != undefined) && ($(item).val().trim().length > 0)) //if the field contains the attribute "format"
                            {
                                if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                    validationerror = true;
                                }
                            }
                        }
                        break;

                    default: //may replace default by (case undefined:)
                        //if textarea 
                        if ($(item).is("textarea")) {
                            if ($(item).val().trim().length > 0 && $(item).attr("data-content") == undefined) {
                                if ($(item).attr("format") != undefined) //if the field contains the attribute "format"
                                {
                                    if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                        $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                        errorMessage = reps.validateformat($(item).attr("format"), $(item).val());
                                        validationerror = true;
                                    }
                                }
                            }

                            if ($(item).attr("data-content") == "required") {
                                if ($(item).val().trim().length <= 0 && ($(item).attr("format") == undefined)) {
                                    $(item).attr("placeholder", "Enter Value");
                                    validationerror = true;
                                }
                                if ($(item).val().trim().length <= 0 && ($(item).attr("format") != undefined)) {
                                    $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                    validationerror = true;
                                }
                                if (($(item).attr("format") != undefined) && ($(item).val().trim().length > 0)) //if the field contains the attribute "format"
                                {
                                    if (reps.validateformat($(item).attr("format"), $(item).val())) {
                                        $(item).attr("placeholder", reps.validateformat($(item).attr("format"), $(item).val()));
                                        errorMessage = reps.validateformat($(item).attr("format"), $(item).val());
                                        validationerror = true;
                                    }
                                }
                            }
                        }
                            // if select option first value is -1, show error
                        else if ($(item).is("select")) {
                            //if ($(item).attr("data-content") == "required" && ($(item).find("option:selected").index() <= 0)) {
                            if ($(item).attr("data-content") == "required" && ($(item).val().trim() == -1)) {
                                validationerror = true;
                            }
                        }

                        break;
                }

                if (validationerror == true) {
                    reps.setWorng($(item).attr('id'));
                    cntRequired++;
                }
            }
        });
        if (cntRequired == 0) {
            return true;
        } else if (cntRequired > 0) {
            if (errorMessage) {
                //toastr.error(errorMessage, 'Invalid Field');
                displayMessage('Invalid Field, ' + errorMessage, 'warning');
            } else {
                //toastr.error('Please fill in the required fields', 'Invalid Field');
                displayMessage('Invalid Field, Please fill in the required fields', 'warning');
            }
            return false;
        } else {
            return false;
        }

    },
    ///call class error
    setWorng: function (id) {
        $('.validate-field-success').removeClass();
        $('#' + id).addClass("validate-field");
    },

    setRight: function (id) {
        document.getElementById(id).style.border = "1px solid #ffffff";
    },
    //end check form validation
    //validation regex
    validateformat: function (type, fieldvalue) {
        var errorMessage = '';
        switch (type) {
            case 'email':
                var regxemail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                if ((!regxemail.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter Valid Email Address";
                    return errorMessage;
                }
                break;
            case 'multiple-emails':
                var regxemail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                var emails = fieldvalue.trim().replace(" ", "").split(";");

                for (i = 0; i < fieldvalue.trim().split(";").length; i++) {
                    if (emails[i] != "") {
                        if (!regxemail.test(emails[i])) {
                            errorMessage = "Enter Valid Email Address";
                            return errorMessage;
                        }
                    }
                }
                break;
            case 'iconName':
                var regxnumber = /^[a-z_]*$/;
                if ((!regxnumber.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter valid Icon Name";
                    return errorMessage;
                }
                break;
            case 'number':
                var regxnumber = /^[0-9]*$/;
                if ((!regxnumber.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter Valid Number";
                    return errorMessage;
                }
                break;
            case 'telnumber':
                var regxnumber = /^[0-9]{9}$/;
                if ((!regxnumber.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter Valid Number. User must enter 9 numbers";
                    return errorMessage;
                }
                break;
            case 'text':
                var regxname = /^[a-z\u00C0-\u00DC\A-Z\u00E0-\u00FC\-\s]+$/;
                if (!regxname.test(fieldvalue.trim())) {
                    errorMessage = "Enter Valid Text";
                    return errorMessage;
                }
                break;
            case 'amount':
                var regxamount = /^-?[0-9]+([,.][0-9]+)?$/g;
                if ((!regxamount.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter Valid Amount";
                    return errorMessage;
                }
                break;
            case 'alphanum':
                var regxalpha = /^[a-z\u00C0-\u00DC\A-Z0-9\u00E0-\u00FC\-\s]+$/;
                if ((!regxalpha.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter Valid Alpha Number";
                    return errorMessage;
                }
                break;
            case 'alphanumid':
                var regxalpha = /^[a-z\u00C0-\u00DC\A-Z0-9\u00E0-\u00FC\-\s]{1,20}$/;
                if ((!regxalpha.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter Valid Alpha Number. User must enter upto 20 characters";
                    return errorMessage;
                }
                break;
            case 'address':
                var regxaddress = /^[a-zA-Z0-9\\\/-\s]*([,-\\\/\s]*[a-zA-Z-\\\/\s]+)?$/g;
                if ((!regxaddress.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter valid Address";
                    return errorMessage;
                }
                break;
            case 'coordinates':
                var regxcoordinates = /([+-]?\d+\.?\d+)\s*,\s*([+-]?\d+\.?\d+)/;
                if ((!regxcoordinates.test(fieldvalue.trim())) || fieldvalue.trim() == "" || fieldvalue.trim() == null) {
                    errorMessage = "Enter valid Coordinates";
                    return errorMessage;
                }
                break;
        }
    },
    //end of validation regex
    // autosearch for participant : retrieve data 
    autosearch: function (idExistPartitext, controllerName, methodName, resultDivID) {
        var txtboxautosearchparticipant = $("#" + idExistPartitext).val().trim();
        var GivenNames = "";
        var min_length = 1;
        var result = "";
        GivenNames = txtboxautosearchparticipant;
        //call ajax function
        if ($("#" + idExistPartitext).val().trim().length >= min_length) {
            $('#' + resultDivID).removeAttr('style');
            var data = {
                'GivenName': GivenNames
            };
            result = AjaxControllerCaller(controllerName, methodName, resultDivID, data)
            if (result) {
                $('#' + resultDivID).html(data);
            }
        } else {
            $('#' + resultDivID).html("");
            $('#' + resultDivID).hide();
        }
    },
    //end autosearch for participant
    //set data in a list
    participantsNames: function (participant_detail) {
        //variables
        var split_participant_detail;
        var participantName;
        var ParticipantTypeID;
        var TypeID;
        //end of variables

        //split elements
        split_participant_detail = participant_detail.split(";");
        participantName = split_participant_detail[0];
        ParticipantTypeID = split_participant_detail[1];
        TypeID = split_participant_detail[2];
        //end of split elements 

        //set values & hide list
        $("#participant-existing-list").hide();
        $("#existParticipantName").val(participantName);
        $("#participantTypeID").val(ParticipantTypeID);
        $("#ptTypePersonCompany").val(TypeID);
        //end of set values & hide list

        //hide div box
        $("#suggestion-box").hide();
    }, // end of set data in a list

    //if country is selected 
    countrySelection: function (objectCountryElement) {
        //get element if existed in form
        var element = $('#editProvince');
        if (typeof (element) != 'editProvince' && element != null) {
            // exists.
            $(element).empty();
        }
        //get values of selected country
        var selectedValue = objectCountryElement.value.trim();
        var paramObject = {
            selectedValueID: selectedValue
        };
        if (selectedValue != null || selectedValue != "-1") {
            var htmlResult = AjaxControllerCaller('Participant', 'GetProvince', 'ProvinceFilter', paramObject);
        }
    }, //end of if country is selected 


    //if workflow is selected 
    workflowList: function (objectWorkflowElement) {

        //hide workflowtask list 
        $('#WorkflowTaskList').empty();
        //end of hide workflowtask list

        //get values of selected country
        var selectedValue = objectWorkflowElement.value.trim();
        var paramObject = { selectedValueID: selectedValue };
        if (selectedValue != null || selectedValue != "-1") {
            var htmlResult = AjaxControllerCaller('AdminDocuments', 'GetSelectWorkflowTask', 'WorkflowList', paramObject);
        }
    },//end of if workflow is selected 

    //if workflow task GUID is selected 
    workflowTaskList: function (objectWorkflowTaskGUIDElement) {

        //get values of selected country
        var selectedValue = objectWorkflowTaskGUIDElement.value.trim();
        var paramObject = { selectedValueID: selectedValue };
        if (selectedValue != null || selectedValue != "-1") {
            var htmlResult = AjaxControllerCaller('AdminDocuments', 'GetWorkflowTaskList', 'WorkflowTaskList', paramObject);
        }
    },//end of if workflow task GUID is selected 

}
//end of reps function//




/***************************************/
/*   Split Buttons functions           */
/***************************************/

/* Redundant for now... */

$(function () {
    var splitBtn = $('.x-split-button');

    $('button.x-button-drop').on('click', function () {
        if (!splitBtn.hasClass('open'))
            splitBtn.addClass('open');
    });

    $('.x-split-button').click(function (event) {
        event.stopPropagation();
    });

    $('html').on('click', function () {
        if (splitBtn.hasClass('open')) {
            splitBtn.removeClass('open');

        }

    });

    $("#GenerateFileList li").on('click', function () {
        if (splitBtn.hasClass('open'))
            splitBtn.removeClass('open');
    });

    $("#GenerateFileBtn li").on('click', function () {
        if ($('input[class=documents-checkbox]:checked').length > 0) {
            FormObjectSubmit(null, '#PrintListForm', true);
        } else {
            // Display error message   
            //toastr.error('Please select at least one checkbox.');
            displayMessage('Please select at least one checkbox.', 'error');
        }
    });
});

function GenerateDocumentExtension(obj) {
    var value = $(event.target).attr('data-dropdown-value');
    if (value == "angular") {
        if ($('input[class=documents-checkbox]:checked').length > 0) {
            $('#PL_docType').val("PDF");
            //alert("file is submit, download display");
            FormObjectSubmit(null, '#PrintListForm', true);
        } else {
            // Display error message   
            //toastr.error('Please select at least one checkbox.');
            displayMessage('Please select at least one checkbox.', 'error');
        }
    }
    if (value == "backbone") {
        if ($('input[class=documents-checkbox]:checked').length > 0) {
            $('#PL_docType').val("DOCX");
            alert("file is submit, download display");
            //FormObjectSubmit('#PrintListForm', true);
        } else {
            // Display error message   
            //toastr.error('Please select at least one checkbox.');
            displayMessage('Please select at least one checkbox.', 'error');
        }

    }
}

function ValidateChkBxDocumentExtension(obj) {
    var checkboxClassname = $(obj).find($(':checkbox')).prop("class");
    if ($('input[class=' + checkboxClassname + ']:checked').length > 0) {
        //get all check id as string 
        var notChecked = [], checked = [];
        $(":checkbox").each(function () {
            if ($(this).hasClass(checkboxClassname)) {
                id = this.id;
                this.checked ? checked.push(id) : notChecked.push(id);
            }
        });
        //end of get all check id as string 

        //call pop up message box
        CallPopupPanel('Documents', 'PopupDocumentExtention', 'general-panel', {
            'objectDocumentID': checked.toString()
        }, 'Remove-popup-footer');
        //end of call pop up message box
    } else {
        // Display error message   
        //toastr.error('Please select at least one checkbox.');
        displayMessage('Please select at least one checkbox.', 'error');
    }
}









// function display Border after document is load - ajax call
function ShowBorder() {
    $('.general-panel').addClass('general-panel-border');
}

function HideBorder() {
    animate: false;
    $('.general-panel').removeClass('general-panel-border');
}
//end of function display Border after document is load - ajax call



//call dynatable plugin search when document is ready
function DynatableSearch(dynaTableId, chartExampleId, searchTable) {
    var $table = $(dynaTableId), $chart = $(chartExampleId);

    // Create a function to update the chart with the current working set
    // of records from dynatable, after all operations have been run.
    var updateTable = function () {
        var dynatable = $table.data('dynatable')
        dynatable.$element.find('thead th[class]').each(function (idx) {
            dynatable.$element.find('tbody td:nth-child(' + ($(this).index() + 1) + ')').addClass(this.className)
        })
    }

    // Attach dynatable to our table, hide the table,
    // and trigger our update function whenever we interact with it.
    $table
        .dynatable({
            dataset: {
                perPageDefault: 7
            },
            features: {
                paginate: true,
                sort: true,
                pushState: true,
                search: false,
                recordCount: true,
                perPageSelect: false
            },
            inputs: {
                queryEvent: 'blur change keyup',
                recordCountTarget: $chart,
                paginationLinkTarget: $chart,
                searchTarget: $chart,
                perPageTarget: $chart,
                queries: $(searchTable),
            },
            table: {
                defaultColumnIdStyle: 'trimDash'
            }
        })
        .bind('dynatable:afterProcess', updateTable);

    // Run our updateChart function for the first time.
    updateTable();
}
//end of call dynatable plugin search when document is ready

//workflow step id - display form
function workflowStepTemplate(footerDocument, footerworkflowstep, validate) {
    if (validate) {
        $(footerDocument).hide();
        $(footerworkflowstep).show();
    }
    else {
        $(footerDocument).show();
        $(footerworkflowstep).hide();
    }
}

function workflowStepSaveToDocument(footerDocument, footerworkflowstep, workflowStepResult, workflowStepID) {
    //$(workflowStepResult).append(' <input  type="hidden" name="workflowStepID" id="workflowStepID" value=' + $(workflowStepID + ' :selected').val() + ' readonly /><label>' + $(workflowStepID + ' :selected').text() + '</label>');  
    var ul = $('#workflowStepTaskList ul');
    ul.find('li:first').css("display", "none")//hide the first ul
    if ($(workflowStepID + ' :selected').val() != -1 && $(workflowStepID + ' :selected').val() !== undefined) {
        $('#workflowStepTaskList').show();
        ul.append($("<li>", { id: 'documentWorkflow:' + $(workflowStepID + ' :selected').val() }).append($("<a>", { href: "#", name: $(workflowStepID + ' :selected').val(), id: $(workflowStepID + ' :selected').val() }).text($(workflowStepID + " :selected").text())).append(' <input  type="hidden" name="workflowStepID" id="workflowStepID" value=' + $(workflowStepID + ' :selected').val() + ' readonly />').append(ul.find('li a:last').clone().prop('id', 'documentWorkflow:' + $(workflowStepID + ' :selected').val()).appendTo(ul)));
    }
}

//end of workflow step id- display form

//confirmation to remove li from document workflow form
function ConfirmDelete(obj) {
    var removeDocumentWorkflow = confirm("Are you sure you want to delete?");
    if (removeDocumentWorkflow) {
        $(obj).parent().remove()
        return true;
    }
    else {
        return false;
    }
}


function ConfirmDeleteDocumentController(obj) {
    var removeDocumentWorkflow = confirm("Are you sure you want to delete?");
    if (removeDocumentWorkflow) {
        $.ajax({
            url: "/AdminDocuments/RemoveAdminDocumentWorkflow",
            type: 'POST',
            data: { documentTemplateID: null, documentWorkflowID: $(obj).attr("data-id") },
            success: function (result) {
                $('#documentWorkflowList').html(result);
            },
            error: function () {
                alert("error");
            }
        });
        return true;
    }
    else {
        return false;
    }
}
//end of confirmation to remove li from document workflow form



// Add/Remove IsRequired attribute for Document Template admin
function ToggleFileTemplateRequired(obj) {

    if ($('#TemplateESign').is(":checked") || $('#IsStaticDocument').is(":checked") || $('#IsDocFusionTemplate').is(":checked")) {
        $('#fileUploadEdit').attr("data-content", "required");
        $("#isRequiredFile").html("*");
        //$("#isRequiredFile").addClass("UploadImageFilter");
    } else {
        $('#fileUploadEdit').removeAttr("data-content");
        $("#isRequiredFile").html("");
        //$("#isRequiredFile").removeClass("UploadImageFilter");
    }

    if ($(obj).val() == "IsDocFusionTemplate" && $(obj).is(":checked")) {
        $('#IsStaticDocument').attr("disabled", "disabled");
    }
    else if ($(obj).val() == "IsStaticDocument" && $(obj).is(":checked")) {
        $('#IsDocFusionTemplate').attr("disabled", "disabled");
        $('#TemplateESign').attr("disabled", "true");
    }
    else if ($(obj).val() == "TemplateESign" && $(obj).is(":checked")) {
        $('#IsStaticDocument').attr("disabled", "disabled");
    }
    else {
        $("#IsDocFusionTemplate").removeAttr("disabled");
        $("#TemplateESign").removeAttr("disabled");
        $("#IsStaticDocument").removeAttr("disabled");
    }
}

//generate document to download filename in required format
function GenerateDocumentCall(targetController, targetAction, targetPanel, paramObject) {
    AjaxControllerCaller(targetController, targetAction, targetPanel, paramObject)
    onComplete();
};
//end of generate document to download filename in required format


//update tabs when deals has edited
function UpdateTabs(targetDealTabController, targetDealAction, targetDealPanel, targetHeaderController, targetHeaderAction, targetDiv) {
    AjaxControllerCaller(targetDealTabController, targetDealAction, targetDealPanel, null);
    AjaxControllerCaller(targetHeaderController, targetHeaderAction, targetDiv, null)
};
//end of update tabs when deals has edited


//Check if role already exists - Admin Roles
function CheckIfNameExists(item, buttonId, formID, oldValue, nameToCheck, parameter, submitForm, userRoleID) {
    if (oldValue != $(item).val()) // We check if the user is not editing the role value
    {
        var urlPath;
        var errorMsg;


        switch (nameToCheck) {
            case "AdminRoleName":
                urlPath = globaljsPath + 'AdminRoles/CheckIfRoleExists';
                errorMsg = 'role';
                break;
            case "WorkflowName":
                urlPath = globaljsPath + 'AdminWorkflow/CheckIfWorkflowNameExists';
                errorMsg = 'Workflow';
                break;
            case "WorkflowTaskName":
                urlPath = globaljsPath + 'AdminWorkflow/CheckIfWorkflowTaskNameExists';
                errorMsg = 'Task';
                break;

            default:
                alert("Invalid input");
                break;
        }

        var param = { 'name': $(item).val(), 'parameter': parameter };

        $.ajax({
            type: "POST",
            url: urlPath,
            cache: false,
            content: "application/json; charset=utf-8",
            dataType: "html",
            data: param,
            async: true,
            //data: JSON.stringify(data),
            success: function (results) {
                if (results == "ok") // If the role does not exist we submit the form
                {
                    if (errorMsg != "role") {
                        $(buttonId).removeClass("dirty-disabled");
                    }
                    $(item).removeClass("validate-field");
                    if (submitForm) {
                        FormObjectSubmit(buttonId, formID, true);
                        if (nameToCheck == "AdminRoleName") {
                            AjaxControllerCaller('AdminRoles', 'LogoutUserOnRoleChange', null, { 'roleID': parameter });
                            if (parameter == userRoleID) {
                                window.location.href = globaljsPath + "Login/Logout?tokenerror=false";
                            }
                        }
                    }
                }
                else {
                    // If the role exists, we do not submit the form and show a toastr with error message
                    $(item).addClass("validate-field");
                    //toastr.error('This ' + errorMsg + ' already exists', 'Error');
                    displayMessage('This ' + errorMsg + ' already exists', 'error');


                    if (errorMsg != "role") {
                        $(buttonId).addClass("dirty-disabled");
                    }
                    return false;
                }
            },
            error: function (response) {
                //toastr.error('An error has occured. Please retry the action. Error:' + response, 'Error');
                displayMessage('An error has occured. Please retry the action. Error:' + response, 'error');
            },
            ajaxStart: function () {
                ShowLoader();
            },
            ajaxStop: function () {
                HideLoader();
            }

        });
    }
    else // If the user is editing the form and the old value matches the new one, we submit the form
    {
        if (submitForm) {
            FormObjectSubmit(buttonId, formID, true);
            if (nameToCheck == "AdminRoleName") {
                AjaxControllerCaller('AdminRoles', 'LogoutUserOnRoleChange', null, { 'roleID': parameter });
                if (parameter == userRoleID) {
                    window.location.href = globaljsPath + "Login/Logout?tokenerror=false";
                }
            }
        }
    }
}

function CloseAdminEditActionPopup() {
    $('.general-panel').toggleClass('show');
    toogleOverlayHide();
    $('.general-panel').html('');
    //toastr.success('Changes have been successfully updated', 'Successful');
    displayMessage('Changes have been successfully updated', 'success');
}

//function to display error in actual page when it is not ajax
function ConfirmationErrorMessage() {
    toastr.warning("<button type='button' id='confirmationRevertYes' class='send-error-report-button clear'>Send Error Report</button>", 'An error has occured!',
            {
                closeButton: false,
                timeOut: 999999999,
                allowHtml: true,
                positionClass: "toast-bottom-right",
                onShown: function (toast) {
                    $("#confirmationRevertYes").click(function () {
                        var url = globaljsPath + 'ErrorHandling/ErrorForm?errorMsg=null';
                        window.location.href = url;
                    });
                }
            });
}
//end of function to display error in actual page when it is not ajax


var onSuccessRedirect = function (result) {
    if (result.url) {
        // if the server returned a JSON object containing an url 
        // property we redirect the browser to that url
        window.location.href = result.url;
    }
}

//Javascript function added to comfirm if Admin can overide existing email address. If yes is clicked, only then will overide existing email,
//new user will be added directly
function ValidateExistingEmailAndSubmit() {
    var emaill = $('#Email').val();

    $.ajax({
        url:  globaljsPath + "AdminUser/GetEmailIfExist",
        type: "POST",
        dataType: "json",
        data: { 'email': emaill },
        success: function (response) {
            if (response == 9) {
                var retVal = confirm("User with same email address already exist, do you want to continue?");
                if (retVal == true) {
                    FormObjectSubmit('#btn-saved', '#AddAdminUser', true);
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (response == 10) {
                FormObjectSubmit('#btn-saved', '#AddAdminUser', true);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });

}

////set text box green after data is filled (validation)
$(function () {
    $('form input[type=text], form input[type=email],form select').bind("keypress click", function () {
        var empty = false;
        $('form input[type=text], form input[type=email],form select').each(function () {
            //check if text box , select option, checkboxes is not null
            if ($(this).val() == '' || $(this).val() == '-1' || $(this).val() == null) {
                empty = true;
            }
        });
        if (!$('form input[type=checkbox]').is(':checked')) {
            empty = true;
        }
        //set border green after input types is filled
        if (empty) {
            $('.validate-field').removeClass();
        }
    });
});
////end of set text box green after data is filled (validation)