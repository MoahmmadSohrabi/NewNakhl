// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};

//function scrollTo(e, duration) {
//    $([document.documentElement, document.body]).animate({ scrollTop: e.offset().top }, duration);
//}
function handleResponse(data) {
    if (data.IsError) {
        showToast('error', data.Message);
    } else {
        showToast('success', data.Message);
    }
}
//loading overlay
var loadingOverlay = $('.overlay-loading');

function mapIconPath(id) {
    switch (id) {
        case 1:
            return '/images/marker/Sport.png';
        case 2:
            return '/images/marker/GameNet.png';
        case 3:
            return '/images/marker/GameBoard.png';
        default:
            return '/images/marker/GameNet.png';
    }
}

function showToast(type, message) {
    switch (type) {
        case 'error':
            vt.error(message, { position: "bottom-center" });
            break;
        case 'info':
            vt.info(message, { position: "bottom-center" });
            break;
        case 'warn':
            vt.warn(message, { position: "bottom-center" });
            break;
        case 'success':
            vt.success(message, { position: "bottom-center" });
            break;
        default:
            vt.info(message, { position: "bottom-center" });
            break;
    };
}
function openNav() {
    document.getElementById("mySidenav").style.right = "0";
    document.getElementById("myCanvasNav").style.width = "100%";
    document.getElementById("myCanvasNav").style.opacity = "0.8";
}

function closeNav() {
    document.getElementById("mySidenav").style.right = "-250px";
    document.getElementById("myCanvasNav").style.width = "0";
    document.getElementById("myCanvasNav").style.opacity = "0";
}


$('#topSlider').owlCarousel({
    rtl: true,
    loop: true,
    nav: true,
    autoplay: true,
    autoplayTimeout: 3500,
    autoplayHoverPause: true,
    margin: 10,
    items: 1,
});

$('.owl-prev').html('<i class="fal fa-chevron-right"></i>');
$('.owl-next').html('<i class="fal fa-chevron-left"></i>');
