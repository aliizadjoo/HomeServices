document.addEventListener('DOMContentLoaded', function () {
 
    var carousels = document.querySelectorAll('.carousel');

    carousels.forEach(function (carousel) {
        
        new bootstrap.Carousel(carousel, {
            interval: 3000, 
            wrap: true,     
            touch: true     
        });
    });
});