

document.addEventListener("DOMContentLoaded", function () {
    convertAllDates();
});

function convertAllDates() {
    const dateElements = document.querySelectorAll('.convert-date');

    dateElements.forEach(el => {
        const gregorianDate = el.innerText.trim();
        if (gregorianDate && gregorianDate !== "---") {
            try {
               
                const date = new Date(gregorianDate);
                const pDate = new Intl.DateTimeFormat('fa-IR').format(date);
                el.innerText = pDate;
            } catch (e) {
                console.error("خطا در تبدیل تاریخ:", e);
            }
        }
    });
}