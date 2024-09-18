$(document).ready(function () {
    $('#Cover').on('change', function () {
        $('.Cover_page').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
    });
});