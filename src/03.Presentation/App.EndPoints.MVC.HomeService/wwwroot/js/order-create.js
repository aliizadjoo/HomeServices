$(document).ready(function () {

    if ($.fn.persianDatepicker) {

        $('.p-datepicker').persianDatepicker({
            format: 'YYYY/MM/DD',
            autoClose: true,
            observer: true,
            initialValue: false,
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
        console.error("persianDatepicker لود نشده است");
    }
});
