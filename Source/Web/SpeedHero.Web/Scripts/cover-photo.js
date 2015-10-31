$('#CoverPhotoUrl').on('keyup', onInstertedUrl);
$('#CoverPhotoUrl').on('paste', onInstertedUrl);

function onInstertedUrl() {
    var _this = this,
        $fileInput = $('input[id="File"]'),
        $fileInputParent = $fileInput.parent();

    setTimeout(function () {
        if ($(_this).val()) {
            $fileInput.attr('disabled', '');
            $fileInputParent.addClass('k-state-disabled');
        }
        else {
            $fileInput.removeAttr('disabled');
            $fileInputParent.removeClass('k-state-disabled');
        }
    }, 100);
}


function onKendoUploadSelect() {
    $('#CoverPhotoUrl').attr('disabled', '');
}

function onKendoUploadRemove() {
    $('#CoverPhotoUrl').removeAttr('disabled');
}