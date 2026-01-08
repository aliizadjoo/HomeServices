$(document).ready(function () {
    if ($('.select2-multiple').length > 0) {
        $('.select2-multiple').select2({
            placeholder: "انتخاب تخصص‌ها",
            allowClear: true,
            width: '100%',
            dir: "rtl",
            language: "fa"
        });
    }
});