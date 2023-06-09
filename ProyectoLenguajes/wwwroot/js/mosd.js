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
        $("#rating_info").text("(" + new_rating.votes + " votes, average: " + new_rating.average + " from 5)");
    });
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
$(document).ready(function () {
    $('#actor_carousel').slick({
        centerMode: true,
        centerPadding: '60px',
        slidesToShow: 2,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 1
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('#ten_new').slick({
        centerMode: true,
        centerPadding: '60px',
        slidesToShow: 3,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 1
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('#carousel1').slick({
        centerMode: true,
        centerPadding: '60px',
        slidesToShow: 3,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 1
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('#carousel2').slick({
        centerMode: true,
        centerPadding: '60px',
        slidesToShow: 3,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 1
                }
            }
        ]
    });
});

$(document).ready(function () {
    $('#carousel3').slick({
        centerMode: true,
        centerPadding: '60px',
        slidesToShow: 3,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: true,
                    centerPadding: '40px',
                    slidesToShow: 1
                }
            }
        ]
    });
});