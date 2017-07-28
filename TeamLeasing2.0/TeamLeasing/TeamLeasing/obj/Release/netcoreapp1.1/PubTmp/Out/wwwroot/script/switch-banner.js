

var images = new Array('/image/banner.jpg', '/image/banner1.jpg', "/image/banner3.jpg", "/image/banner4.jpg");
var index = 1;

function rotateImage() {
    $('#bannerImage').fadeOut('fast', function () {
        $(this).attr('src', images[index]);

        $(this).fadeIn('fast', function () {
            console.log("dfsdf");
            if (index === images.length - 1) {
                index = 0;
            }
            else {
                index++;
            }
        });
    });
}

$(document).ready(function () {
    setInterval(rotateImage, 8000);
});