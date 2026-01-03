$(document).ready(function () {

    if ($('.p-datepicker').length > 0) {
        $('.p-datepicker').persianDatepicker({
            format: 'YYYY/MM/DD',
            autoClose: true,
            observer: true,
            initialValue: true,
            displayFormat: 'YYYY/MM/DD',
            // اضافه کردن خط زیر برای جلوگیری از انتخاب تاریخ گذشته:
            minDate: new persianDate().valueOf(),
            calendar: {
                persian: {
                    locale: 'fa',
                    showHint: true,
                    leapYearMode: 'astronomical'
                }
            }
        });
    } else {
        console.error("المان .p-datepicker یافت نشد!");
    }
});