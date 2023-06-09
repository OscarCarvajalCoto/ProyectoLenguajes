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

function rateMovie(rating, app_user, ms_id) {
    showRate(rating);
    var url = "/HomePage/GetNewRating";
    $.get(url, { rating: rating, app_user: app_user, ms_id: ms_id }, function (rate_data) {
        var new_rating = JSON.parse(rate_data);
        $("#votes").text("Votes: " + new_rating.votes);
        $("#percentage").text("Percentage: " + new_rating.percentage);
        $("#average").text("Average: " + new_rating.average + " from 5");
    });
    var stars = document.getElementsByClassName("star");
    for (var i = 0; i < stars.length; i++) {
        stars[i].classList.add("disabled");
    }
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
