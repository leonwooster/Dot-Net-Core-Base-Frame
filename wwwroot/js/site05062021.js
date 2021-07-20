// jQuery plugin to prevent double submission of forms
jQuery.fn.preventDoubleSubmission = function () {
    $(this).on('submit', function (e) {
        var $form = $(this);

        if ($form.data('submitted') === true) {
            // Previously submitted - don't submit again
            e.preventDefault();
        } else {
            // Mark it so that the next submit can be ignored
            $form.data('submitted', true);
        }
    });

    // Keep chainability
    return this;
};

$(document).ready(function () {    

    $('form').preventDoubleSubmission();

    var country = $('#flowcountry').val();

    //Enable the tooltip for file upload fields.
    $('[data-toggle="tooltip"]').tooltip();

    //when submit button is clicked, intercept the event and make the necessary validatation before submit back to server.
    $('#submitBtn').click(function (event) {
        try {
            $('#form1').attr("action", "/Home/Submit");
            var isValid = $('#form1').valid();

            if (isValid) return true;
            else {
                event.preventDefault();
                return false;
            }
        }
        catch (err) {
            alert(err.message);
        }
    });

    //A function that merely save data back to the database and field validation is not important.
    $('#submitSave').click(function (event) {
        $('#form1').validate().cancelSubmit = true; //cancel validation.
        $('#form1').attr("action", "/Home/Save");             
        $('#form1').submit();

        return false;
    });

    /*
     * Dynamics for the form
     */
    function readOnlyCompanyReg(state) {
        if (state == true) {
            var el = $('#basic_info_company_registration_no');
            el.attr('readonly', 'readonly');
            el.data('data', el.val());
            el.val('');

            var el2 = $('#basic_info_identification_no');
            el2.removeAttr('readonly');
            el2.val(el2.data('data'));
        }
        else {
            var el2 = $('#basic_info_identification_no');
            el2.attr('readonly', 'readonly');
            el2.data('data', el2.val());
            el2.val('');

            var el = $('#basic_info_company_registration_no');
            el.val(el.data('data'));
            el.removeAttr('readonly');
        }
    }
    function assignData() {
        var el = $('#basic_info_company_registration_no');
        el.data('data', el.val());

        el = $('#basic_info_identification_no');
        el.data('data', el.val());
    }
    function hideShowBasedOnOrg() {
        var s = $('#basic_info_entity_type option:selected');

        $('#basic_info_entity_type2').text(s.val());

        if (s.val() == 'Individual') {
            $('.ent1').hide();
            $('.ent2').show();
            readOnlyCompanyReg(true);

            //disable the company reg upload
            var el = $('#businessLicenseFile');
            el.attr('disabled', 'disabled');
            el.val('');
            el.prev('div').find('a').hide();

            //enable the individual upload
            var el = $('#IdRecordFile');
            el.removeAttr('disabled');
            el.prev('div').find('a').val(el.attr('data-val-previous'));
            el.prev('div').find('a').show();
        }
        else if (s.val() == 'Company') {
            $('.ent2').hide();
            $('.ent1').show();
            readOnlyCompanyReg(false);

            //enable the business upload
            var el = $('#businessLicenseFile');
            el.removeAttr('disabled');
            el.prev('div').find('a').val(el.attr('data-val-previous'));
            el.prev('div').find('a').show();

            //disable the individual upload
            var el = $('#IdRecordFile');
            el.attr('disabled', 'disabled');
            el.val('');
            el.prev('div').find('a').hide();
        }
    }
    function readonlySetter(ddlName, affectedName, setVal) {
        var v = $('#' + ddlName + ' option:selected');
        var el = $('#' + affectedName);        

        if (v.val() == setVal) {
            if (el.is('select'))
                el.removeAttr('disabled');

            el.removeAttr('readonly');

            if(el.data('data') != null)
                el.val(el.data('data'));
        }
        else {
            if (el.is('select'))
                el.attr('disabled', 'disabled');

            el.attr('readonly', 'readonly');            
            el.data('data', el.val());
            el.val('');
        }
    }
    function setFieldFrom(from, to) {
        var tobe = $('#' + from).val();
        $('#' + to).text(tobe);
    }
    function readOnlyDiffSign() {
        var selected = $('#self_certification_2 option:selected').val();

        if (selected == "Yes") { //disable the control.
            var el = $('#cert6File');
            el.attr('disabled', 'disabled');
            el.val('');
            el.prev('div').css('display', 'none');
        }
        else { //enable
            var el = $('#cert6File');
            el.removeAttr('disabled');
            //el.prev('div').text(el.attr('data-val-previous'));   
            el.prev('div').css('display', 'block');
        }
    }

    //handle for entity type change.
    $("#basic_info_entity_type").change(function () {
        hideShowBasedOnOrg();
    });

    $('#basic_info_bu').change(function () {
        readonlySetter('basic_info_bu', 'basic_info_bu_others', 'Others');
    });

    $('#basic_info_function').change(function () {
        readonlySetter('basic_info_function', 'basic_info_function_others', 'Others');
    });

    $('#third_party_organization_type').change(function () {
        readonlySetter('third_party_organization_type', 'third_party_organization_description', 'Others (Please describe below)');
    });

    $('#entity_details_publicly_traded').change(function () {
        readonlySetter('entity_details_publicly_traded', 'entity_details_stock_exchange_name', 'Yes');
    });

    $('#entity_details_subsidiary').change(function () {
        readonlySetter('entity_details_subsidiary', 'entity_details_stock_exchange_name_parent', 'Yes');
    });

    $('#business_info_1').change(function () {
        readonlySetter('business_info_1', 'business_info_2', 'Yes')
    });

    $('#basic_info_description_services').blur(function () {
        setFieldFrom('basic_info_description_services', 'basic_info_description_services2');
    });

    $('#conflict_of_interest_1').change(function () {
        readonlySetter('conflict_of_interest_1', 'conflict_of_interest_2', 'Yes');
    });

    $('#conflict_of_interest_3').change(function () {
        readonlySetter('conflict_of_interest_3', 'conflict_of_interest_4', 'Yes');
    });

    $('#relationships_public_officials_1').change(function () {
        readonlySetter('relationships_public_officials_1', 'relationships_public_officials_2', 'Yes');
    });

    $('#relationships_public_officials_3').change(function () {
        readonlySetter('relationships_public_officials_3', 'relationships_public_officials_4', 'Yes');
    });


    $('#third_party_1').change(function () {
        readonlySetter('third_party_1', 'third_party_2', 'Yes');
        $('#third_party_2').trigger('change');
        readonlySetter('third_party_1', 'third_party_3', 'Yes');
        readonlySetter('third_party_1', 'third_party_4', 'Yes');        
        readonlySetter('third_party_1', 'third_party_5', 'Yes');
        $('#third_party_4').trigger('change');
    });

    $('#third_party_4').change(function () {
        readonlySetter('third_party_4', 'third_party_5', 'Yes');
    });

    //Need to specifically check for country.
    if (country != _malaysia) {
        $('#compliance_and_ethics_5').change(function () {
            readonlySetter('compliance_and_ethics_5', 'compliance_and_ethics_6', 'Yes');
        });
    }
    else {
        var lbl = $('label[for=compliance_and_ethics_6] span');
        lbl.hide();
    }

    $('#compliance_and_ethics_3').change(function () {
        readonlySetter('compliance_and_ethics_3', 'often_training', 'Yes');
    });


    $('#actions_1').change(function () {
        readonlySetter('actions_1', 'actions_2', 'Yes');
    });

    $('#actions_3').change(function () {
        readonlySetter('actions_3', 'actions_4', 'Yes');
    });

    $('#actions_5').change(function () {
        readonlySetter('actions_5', 'actions_6', 'Yes');
    });

    $('#actions_7').change(function () {
        readonlySetter('actions_7', 'actions_8', 'Yes');
    });

    $('#self_certification_2').change(function () {
        readonlySetter('self_certification_2', 'self_certification_5', 'No');

        //set the status for the supporting doc upload.
        readOnlyDiffSign();
    });



    //initialization
    assignData(); //must call this first before hideShowBaseOnOrg during initialization.
    hideShowBasedOnOrg();
    readonlySetter('basic_info_bu', 'basic_info_bu_others', 'Others');
    readonlySetter('basic_info_function', 'basic_info_function_others', 'Others');
    readonlySetter('third_party_organization_type', 'third_party_organization_description', 'Others (Please describe below)');
    readonlySetter('entity_details_publicly_traded', 'entity_details_stock_exchange_name', 'Yes');
    readonlySetter('entity_details_subsidiary', 'entity_details_stock_exchange_name_parent', 'Yes');
    readonlySetter('conflict_of_interest_1', 'conflict_of_interest_2', 'Yes');
    readonlySetter('conflict_of_interest_3', 'conflict_of_interest_4', 'Yes');
    readonlySetter('relationships_public_officials_1', 'relationships_public_officials_2', 'Yes');
    readonlySetter('third_party_4', 'third_party_5', 'Yes'); //this must come first compared to other third party fields.
    readonlySetter('third_party_1', 'third_party_2', 'Yes');
    readonlySetter('third_party_1', 'third_party_3', 'Yes');
    readonlySetter('third_party_1', 'third_party_4', 'Yes');
    readonlySetter('third_party_1', 'third_party_5', 'Yes');

    //Exclude checking for Malaysia
    if (country != _malaysia)
        readonlySetter('compliance_and_ethics_5', 'compliance_and_ethics_6', 'Yes');

    readonlySetter('actions_1', 'actions_2', 'Yes');
    readonlySetter('actions_3', 'actions_4', 'Yes');
    readonlySetter('actions_5', 'actions_6', 'Yes');
    readonlySetter('actions_7', 'actions_8', 'Yes');
    readonlySetter('self_certification_2', 'self_certification_5', 'No');
    readonlySetter('business_info_1', 'business_info_2', 'Yes');
    readOnlyDiffSign();
});

//function to delete previously uploaded file for upload file to use.
function deleteFile(el) {    
    $(el).parent().removeClass("undelete");
    $(el).parent().addClass("deleted");    
    $(el).parent().find("img[name='undo']").addClass('undo');
    $(el).removeClass('deleteS');
    $(el).addClass('deleteH');

    //update the delete file store.
    var h = $('#fileToRemove');
    var filesJ = h.val();
    var files = $.parseJSON(filesJ);    
    var id = $(el).parent().parent().find("input[type='hidden']").attr('id');
    
    var newOne = [];

    if (files.length > 0) {
        for (var i = 0; i < files.length; i++) {
            if (files[i] != id)
                newOne.push(files[i]);
        }
    }
    else newOne.push(id);

    h.val(JSON.stringify(newOne));
}

//function to revert file delete function fo file upload.
function restoreFile(el) {
    $(el).parent().removeClass("delete");
    $(el).parent().addClass("undelete");
    $(el).parent().find("img[name='del']").addClass('deleteS');
    $(el).removeClass('undo');
    $(el).addClass('undoH');

    //update the delete file store.
    var h = $('#fileToRemove');
    var filesJ = h.val();
    var files = $.parseJSON(filesJ);    
    var id = $(el).parent().parent().find("input[type='hidden']").attr('id');

    var newOne = [];
    for (var i = 0; i < files.length; i++) {
        if (files[i] != id)
            newOne.push(files[i]);
    }    

    h.val(JSON.stringify(newOne));
}