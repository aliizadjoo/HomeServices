$(document).ready(function () {

    $('.select2-multiple').select2({
        placeholder: "انتخاب تخصص‌ها...",
        allowClear: true,
        dir: "rtl",
        width: "100%",
        language: {
            noResults: function () {
                return "موردی یافت نشد";
            }
        }
    });

});
