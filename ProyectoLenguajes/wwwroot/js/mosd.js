let selectedRating = 0;

function showRate(rating) {
    selectedRating = rating;
    const stars = document.querySelectorAll('.rating-container .star');
    for (let i = 0; i < stars.length; i++) {
        if (i < rating) {
            stars[i].classList.add('checked');
        } else {
            stars[i].classList.remove('checked');
        }
    }
}

function rateMovie(rating) {
    showRate(rating);

    var input = document.getElementById("rating1");
    input.value = rating;

    var btn_rate = document.getElementById("btn_rating");
    btn_rate.click();
}

function highlightStars(rating) {
    const stars = document.querySelectorAll('.rating-container .star');
    for (let i = 0; i < rating; i++) {
        stars[i].style.color = '#FFBF00';
    }
}

function resetStars() {
    const stars = document.querySelectorAll('.rating-container .star');
    for (let i = 0; i < stars.length; i++) {
        if (i >= selectedRating) {
            stars[i].style.color = '#0B0B61';
        } else {
            stars[i].style.color = '#FFBF00';
        }
    }
}
