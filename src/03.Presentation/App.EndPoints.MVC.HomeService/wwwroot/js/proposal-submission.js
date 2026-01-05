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
    }

  
    $('#txtPrice').on('input change', function () {
        validatePrice();
    });

   
    $('#proposalForm').on('submit', function (e) {
        const isPriceValid = validatePrice();
        const dateValue = $('.p-datepicker').val();

        if (!isPriceValid) {
            e.preventDefault(); 
            return false;
        }

        if (dateValue === "" || dateValue === null) {
            e.preventDefault();
            alert("لطفاً تاریخ شروع را انتخاب کنید.");
            return false;
        }
    });

    function validatePrice() {
        const price = parseFloat($('#txtPrice').val());
        const basePrice = parseFloat($('#hdnBasePrice').val());
        const errorSpan = $('#priceError');

        if (isNaN(price) || price < basePrice) {
            errorSpan.text("قیمت پیشنهادی نباید از قیمت پایه (" + basePrice.toLocaleString() + " ریال) کمتر باشد.");
            $('#txtPrice').addClass('is-invalid');
            $('#txtPrice').removeClass('is-valid');
            return false;
        } else {
            errorSpan.text("");
            $('#txtPrice').removeClass('is-invalid');
            $('#txtPrice').addClass('is-valid');
            return true;
        }
    }
});