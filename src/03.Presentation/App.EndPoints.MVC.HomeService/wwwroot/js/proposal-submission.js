$(document).ready(function () {

   
    if ($('.p-datepicker').length > 0) {
        $('.p-datepicker').persianDatepicker({
            format: 'YYYY/MM/DD',
            autoClose: true,
            observer: true,
            initialValue: true,
            displayFormat: 'YYYY/MM/DD',
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

   
    $('#txtPrice').on('input change', function () {
        validatePrice();
    });

 
    $('#proposalForm').on('submit', function (e) {
        const isPriceValid = validatePrice();
        const isDateSelected = $('.p-datepicker').val() !== "";

        if (!isPriceValid || !isDateSelected) {
            e.preventDefault();
            if (!isDateSelected) alert("لطفاً تاریخ شروع را انتخاب کنید.");
        }
    });

  
    function validatePrice() {
        const price = parseFloat($('#txtPrice').val());
        const basePrice = parseFloat($('#hdnBasePrice').val());
        const errorSpan = $('#priceError');

        if (isNaN(price) || price < basePrice) {
            errorSpan.text("قیمت پیشنهادی نباید از قیمت پایه (" + basePrice.toLocaleString() + " ریال) کمتر باشد.");
            $('#txtPrice').addClass('is-invalid');
            return false;
        } else {
            errorSpan.text("");
            $('#txtPrice').removeClass('is-invalid');
            $('#txtPrice').addClass('is-valid');
            return true;
        }
    }
});