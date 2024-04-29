// INDEX
function toggleTextFAQ1() {
    var text = document.getElementById("faq-ans1");
    var arrowc = document.getElementById("faq-arr-closed1");
    var arrowo = document.getElementById("faq-arr-opened1");
    if (text.style.display === "none") {
        text.style.display = "block";
        arrowc.style.display = "none";
        arrowo.style.display = "block";
    } else {
        text.style.display = "none";
        arrowc.style.display = "block";
        arrowo.style.display = "none";
    }
}

function toggleTextFAQ2() {
    var text = document.getElementById("faq-ans2");
    var arrowc = document.getElementById("faq-arr-closed2");
    var arrowo = document.getElementById("faq-arr-opened2");
    if (text.style.display === "none") {
        text.style.display = "block";
        arrowc.style.display = "none";
        arrowo.style.display = "block";
    } else {
        text.style.display = "none";
        arrowc.style.display = "block";
        arrowo.style.display = "none";
    }
}

function toggleTextFAQ3() {
    var text = document.getElementById("faq-ans3");
    var arrowc = document.getElementById("faq-arr-closed3");
    var arrowo = document.getElementById("faq-arr-opened3");
    if (text.style.display === "none") {
        text.style.display = "block";
        arrowc.style.display = "none";
        arrowo.style.display = "block";
    } else {
        text.style.display = "none";
        arrowc.style.display = "block";
        arrowo.style.display = "none";
    }
}

function toggleTextFAQ4() {
    var text = document.getElementById("faq-ans4");
    var arrowc = document.getElementById("faq-arr-closed4");
    var arrowo = document.getElementById("faq-arr-opened4");
    if (text.style.display === "none") {
        text.style.display = "block";
        arrowc.style.display = "none";
        arrowo.style.display = "block";
    } else {
        text.style.display = "none";
        arrowc.style.display = "block";
        arrowo.style.display = "none";
    }
}

function toggleTextFAQ5() {
    var text = document.getElementById("faq-ans5");
    var arrowc = document.getElementById("faq-arr-closed5");
    var arrowo = document.getElementById("faq-arr-opened5");
    if (text.style.display === "none") {
        text.style.display = "block";
        arrowc.style.display = "none";
        arrowo.style.display = "block";
    } else {
        text.style.display = "none";
        arrowc.style.display = "block";
        arrowo.style.display = "none";
    }
}

document.addEventListener('DOMContentLoaded', function () {
    const ratings = document.querySelectorAll('.rating');

    ratings.forEach(function (rating) {
        const stars = rating.querySelectorAll('.star');
        const ratingValue = parseFloat(rating.getAttribute('data-rating').replace(',', '.'));
        const fullStarsFirst = Math.floor(ratingValue);
        const halfStar = ratingValue - fullStarsFirst >= 0.25 && ratingValue - fullStarsFirst <= 0.75;
        const fullStars = fullStarsFirst + (ratingValue - fullStarsFirst > 0.75);
        stars.forEach(function (star, index) {
            if (index < fullStars) {
                star.classList.add('filled');
            } else if (index === fullStars && halfStar) {
                star.classList.add('half');
            }
        });
    });
});


//show next/prev facilities
var currentFaciIndex = 0;
var totalFacilities = document.querySelectorAll('.faci-row').length;
var facilityPerPage = 4;

document.addEventListener("DOMContentLoaded", function () {
    showPageF(currentFaciIndex);
});

function showNextF() {
    currentFaciIndex = (currentFaciIndex + facilityPerPage) % totalFacilities;
    showPageF(currentFaciIndex);
}

function showPreviousF() {
    currentFaciIndex = (currentFaciIndex - facilityPerPage + totalFacilities) % totalFacilities;
    showPageF(currentFaciIndex);
}

function showPageF(index) {
    var facilities = document.querySelectorAll('.faci-row');
    for (var i = 0; i < totalFacilities; i++) {
        var displayIndex = (index + i) % totalFacilities;
        if (i < facilityPerPage) {
            facilities[displayIndex].style.display = 'block';
        } else {
            facilities[displayIndex].style.display = 'none';
        }
    }
}
//show next/prev facilities

//show next/prev types
var currentTypeIndex = 0;
var totalTypes = document.querySelectorAll('.type-row').length;
var typePerPage = 2;

document.addEventListener("DOMContentLoaded", function () {
    showPageT(currentTypeIndex);
});

function showNextT() {
    currentTypeIndex = (currentTypeIndex + typePerPage) % totalTypes;
    showPageT(currentTypeIndex);
}

function showPreviousT() {
    currentTypeIndex = (currentTypeIndex - typePerPage + totalTypes) % totalTypes;
    showPageT(currentTypeIndex);
}

function showPageT(index) {
    var types = document.querySelectorAll('.type-row');
    for (var i = 0; i < totalTypes; i++) {
        var displayIndex = (index + i) % totalTypes;
        if (i < typePerPage) {
            types[displayIndex].style.display = 'block';
        } else {
            types[displayIndex].style.display = 'none';
        }
    }
}
//show next/prev types

//INDEX


