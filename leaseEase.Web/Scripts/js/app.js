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
    initializeRatings();
});
    document.addEventListener('ajaxContentLoaded', function () {
        initializeRatings();
    });
function initializeRatings() {
    const ratings = document.querySelectorAll('.rating');

    ratings.forEach(function (rating) {
        const stars = rating.querySelectorAll('.star');
        const ratingValue = parseFloat(rating.getAttribute('data-rating').replace(',', '.'));
        const fullStarsFirst = Math.floor(ratingValue);
        const halfStar = ratingValue - fullStarsFirst >= 0.25 && ratingValue - fullStarsFirst <= 0.75;
        const fullStars = fullStarsFirst + (ratingValue - fullStarsFirst > 0.75);
        stars.forEach(function (star, index) {
            star.classList.remove('filled', 'half'); // Сначала убираем классы
            if (index < fullStars) {
                star.classList.add('filled');
            } else if (index === fullStars && halfStar) {
                star.classList.add('half');
            }
        });
    });
}


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


//new office

function previewImage(event) {
    var previewWrapper = document.getElementById('image-upload-wrapper');
    var previewText = document.getElementById('image-upload-text');
    var file = event.target.files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        previewWrapper.style.backgroundImage = 'url(' + reader.result + ')';
        previewText.style.display = 'none';
    }

    if (file) {
        reader.readAsDataURL(file);
    } else {
        previewWrapper.style.backgroundImage = 'none';
        previewText.style.display = 'block';
    }
}


function validateAndSubmitCreateOff() {
    //title
    var titleInput = document.querySelector('input[name="Office.Name"]');
    var errorMessageTitle = document.getElementById('error-message-title');
    //description
    var descInput = document.querySelector('textarea[name="Office.Description"]');
    var errorMessageDesc = document.getElementById('error-message-desc');
    //type
    var typeInput = document.querySelector('select[name="Office.TypeId"]');
    var errorMessageType = document.getElementById('error-message-type');
    //price
    var priceInput = document.querySelector('input[name="Office.Price"]');
    var errorMessagePrice = document.getElementById('error-message-price');
    //location
    var locInput = document.querySelector('input[name="Office.Location"]');
    var errorMessageLoc = document.getElementById('error-message-loc');
    //team size
    var teamSizeInput = document.querySelector('input[name="Office.TeamSize"]');
    var errorMessageTeam = document.getElementById('error-message-team');
    //rooms
    var roomsInput = document.querySelector('input[name="Office.Rooms"]');
    var errorMessageRooms = document.getElementById('error-message-rooms');
    //size
    var sizeInput = document.querySelector('input[name="Office.Size"]');
    var errorMessageSize = document.getElementById('error-message-size');
    //minimal period
    var minInput = document.querySelector('select[name="Office.MinimalRentalPeriod"]');
    var errorMessageMin = document.getElementById('error-message-min');
    //image
    var imgInput = document.querySelector('input[name="Office.ImageFile"]');
    var errorMessageImg = document.getElementById('error-message-img');

    if (titleInput.value == "") {
        //title
        titleInput.style.borderColor = 'red';
        errorMessageTitle.style.display = 'block';
    } else if (descInput.value == "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';

        //description
        descInput.style.borderColor = 'red';
        errorMessageDesc.style.display = 'block';
    } else if (typeInput.value === null || typeInput.value === "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';

        //type
        typeInput.style.borderColor = 'red';
        errorMessageType.style.display = 'block';
    } else if (priceInput.value == null || priceInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';

        //price
        priceInput.style.borderColor = 'red';
        errorMessagePrice.style.display = 'block';
    } else if (locInput.value == "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';

        //location
        locInput.style.borderColor = 'red';
        errorMessageLoc.style.display = 'block';
    } else if (teamSizeInput.value == null || teamSizeInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';

        //team Size
        teamSizeInput.style.borderColor = 'red';
        errorMessageTeam.style.display = 'block';
    } else if (roomsInput.value == null || roomsInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';

        //rooms
        roomsInput.style.borderColor = 'red';
        errorMessageRooms.style.display = 'block';
    } else if (sizeInput.value == null || sizeInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';

        //size
        sizeInput.style.borderColor = 'red';
        errorMessageSize.style.display = 'block';
    } else if (minInput.value === null || minInput.value === "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';
        sizeInput.style.borderColor = '';
        errorMessageSize.style.display = 'none';

        //min period
        minInput.style.borderColor = 'red';
        errorMessageMin.style.display = 'block';
    } else if (imgInput.value == null || imgInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';
        sizeInput.style.borderColor = '';
        errorMessageSize.style.display = 'none';
        minInput.style.borderColor = '';
        errorMessageMin.style.display = 'none';

        //image
        imgInput.style.borderColor = 'red';
        errorMessageImg.style.display = 'block';
    } else {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';
        sizeInput.style.borderColor = '';
        errorMessageSize.style.display = 'none';
        minInput.style.borderColor = '';
        errorMessageMin.style.display = 'none';
        imgInput.style.borderColor = '';
        errorMessageImg.style.display = 'none';

        document.forms[0].submit();
    }
}

function validateAndSubmitEditOff() {
    //title
    var titleInput = document.querySelector('input[name="Office.Name"]');
    var errorMessageTitle = document.getElementById('error-message-title');
    //description
    var descInput = document.querySelector('textarea[name="Office.Description"]');
    var errorMessageDesc = document.getElementById('error-message-desc');
    //type
    var typeInput = document.querySelector('select[name="Office.TypeId"]');
    var errorMessageType = document.getElementById('error-message-type');
    //price
    var priceInput = document.querySelector('input[name="Office.Price"]');
    var errorMessagePrice = document.getElementById('error-message-price');
    //location
    var locInput = document.querySelector('input[name="Office.Location"]');
    var errorMessageLoc = document.getElementById('error-message-loc');
    //team size
    var teamSizeInput = document.querySelector('input[name="Office.TeamSize"]');
    var errorMessageTeam = document.getElementById('error-message-team');
    //rooms
    var roomsInput = document.querySelector('input[name="Office.Rooms"]');
    var errorMessageRooms = document.getElementById('error-message-rooms');
    //size
    var sizeInput = document.querySelector('input[name="Office.Size"]');
    var errorMessageSize = document.getElementById('error-message-size');
    //minimal period
    var minInput = document.querySelector('select[name="Office.MinimalRentalPeriod"]');
    var errorMessageMin = document.getElementById('error-message-min');
    //image
    var imgInput = document.querySelector('input[name="Office.ImageFile"]');
    var errorMessageImg = document.getElementById('error-message-img');

    var img2Input = document.querySelector('hidden[name="Office.Image"]');

    if (titleInput.value == "") {
        //title
        titleInput.style.borderColor = 'red';
        errorMessageTitle.style.display = 'block';
    } else if (descInput.value == "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';

        //description
        descInput.style.borderColor = 'red';
        errorMessageDesc.style.display = 'block';
    } else if (typeInput.value === null || typeInput.value === "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';

        //type
        typeInput.style.borderColor = 'red';
        errorMessageType.style.display = 'block';
    } else if (priceInput.value == null || priceInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';

        //price
        priceInput.style.borderColor = 'red';
        errorMessagePrice.style.display = 'block';
    } else if (locInput.value == "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';

        //location
        locInput.style.borderColor = 'red';
        errorMessageLoc.style.display = 'block';
    } else if (teamSizeInput.value == null || teamSizeInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';

        //team Size
        teamSizeInput.style.borderColor = 'red';
        errorMessageTeam.style.display = 'block';
    } else if (roomsInput.value == null || roomsInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';

        //rooms
        roomsInput.style.borderColor = 'red';
        errorMessageRooms.style.display = 'block';
    } else if (sizeInput.value == null || sizeInput.value <= 0) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';

        //size
        sizeInput.style.borderColor = 'red';
        errorMessageSize.style.display = 'block';
    } else if (minInput.value === null || minInput.value === "") {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';
        sizeInput.style.borderColor = '';
        errorMessageSize.style.display = 'none';

        //min period
        minInput.style.borderColor = 'red';
        errorMessageMin.style.display = 'block';
    } else if ((imgInput.value == null) && (img2Input.value == null)) {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';
        sizeInput.style.borderColor = '';
        errorMessageSize.style.display = 'none';
        minInput.style.borderColor = '';
        errorMessageMin.style.display = 'none';

        //image
        imgInput.style.borderColor = 'red';
        errorMessageImg.style.display = 'block';
    } else {
        //prev normal
        titleInput.style.borderColor = '';
        errorMessageTitle.style.display = 'none';
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        typeInput.style.borderColor = '';
        errorMessageType.style.display = 'none';
        priceInput.style.borderColor = '';
        errorMessagePrice.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';
        teamSizeInput.style.borderColor = '';
        errorMessageTeam.style.display = 'none';
        roomsInput.style.borderColor = '';
        errorMessageRooms.style.display = 'none';
        sizeInput.style.borderColor = '';
        errorMessageSize.style.display = 'none';
        minInput.style.borderColor = '';
        errorMessageMin.style.display = 'none';
        imgInput.style.borderColor = '';
        errorMessageImg.style.display = 'none';

        document.forms[0].submit();
    }
}

//new office
//delete office
$(document).ready(function () {
    $('.delete-link').on('click', function () {
        var officeId = $(this).data('id');

        if (confirm('Are you sure you want to delete this office?')) {
            $.ajax({
                type: 'POST',
                url: '/Office/DeleteOff',
                data: { id: officeId },
                success: function (response) {
                    if (response.success) {
                        alert('Office is deleted successfully');
                        location.reload();
                    } else {
                        alert('An error occured while deleting this office');
                    }
                },
                error: function () {
                    alert('Error while deleting office');
                }
            });
        }
    });
});

$(document).ready(function () {
    $('#locationFilter').on('keyup', function () {
        applyFilters();
    });
    $('#priceFilter').on('input', function () {
        applyFilters();
    });
    $('#sortFilter').on('change', function () {
        applyFilters();
    });
    $('.typeFilter').on('change', function () {
        applyFilters();
    });

    $('.faciFilter').on('change', function () {
        applyFilters();
    });
});

function applyFilters() {
    var locationFilter = $('#locationFilter').val();
    var priceFilter = +$('#priceFilter').val();
    var sortFilter = $('#sortFilter').val();
    var typeFilters = $('.typeFilter:checked').map(function () {
        return $(this).val();
    }).get();
    var faciFilters = $('.faciFilter:checked').map(function () {
        return $(this).val();
    }).get();

    $.ajax({
        url: '/Home/Search',
        type: 'GET',
        traditional: true,
        data: $.param({
            locationFilter: locationFilter,
            priceFilter: priceFilter,
            sortFilter: sortFilter,
            typeFilters: typeFilters,
            faciFilters: faciFilters
        }, true),
        success: function (data) {
            $('.office-list').html(data);
            initializeRatings();
        },
        error: function () {
            alert('Something went wrong.');
        }
    });
}
$(document).ready(function () {
    var selectedTypeId = null;
    $('.search-cat').on('click', function () {
        selectedTypeId = $(this).data('type-id');
        $('.search-cat').removeClass('selected');
        $(this).addClass('selected');
    });
    $('#searchLink').on('click', function (e) {
        e.preventDefault(); 
        var searchValue = $('#searchInput').val();
        var url = '/Home/Search' + '?locationFilter=' + encodeURIComponent(searchValue);
        if (selectedTypeId) {
            url += '&typeFilters=' + selectedTypeId;
        }
        window.location.href = url; 
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const locationFilter = urlParams.get('locationFilter');
    const typeFilters = urlParams.get('typeFilters');
    if (locationFilter) {
        document.getElementById('locationFilter').value = locationFilter;
    }
    if (typeFilters) {
        const checkbox = document.getElementById(`type-${typeFilters}`);
        if (checkbox) {
            checkbox.checked = true;
        }
    }

});

function toggleFilters() {
    var text = document.getElementById("filters-adv");
    var arrowc = document.getElementById("filters-closed");
    var arrowo = document.getElementById("filters-opened");
    if (text.style.display === "none") {
        text.style.display = "flex";
        arrowc.style.display = "none";
        arrowo.style.display = "block";
    } else {
        text.style.display = "none";
        arrowc.style.display = "block";
        arrowo.style.display = "none";
    }
}

//delete office

//become creator
function validateAndSubmitBecomeCreator() {
    //title
    var locInput = document.querySelector('input[name="Location"]');
    var errorMessageLoc = document.getElementById('error-message-loc');
    //description
    var descInput = document.querySelector('textarea[name="Description"]');
    var errorMessageDesc = document.getElementById('error-message-desc');

    if (locInput.value == "") {
        //title
        locInput.style.borderColor = 'red';
        errorMessageLoc.style.display = 'block';
    } else if (descInput.value == "") {
        //prev normal
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';

        //description
        descInput.style.borderColor = 'red';
        errorMessageDesc.style.display = 'block';
    }  else {
        //prev normal
        descInput.style.borderColor = '';
        errorMessageDesc.style.display = 'none';
        locInput.style.borderColor = '';
        errorMessageLoc.style.display = 'none';

        document.forms[0].submit();
    }
}
//beome creator


