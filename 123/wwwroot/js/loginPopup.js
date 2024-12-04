$(document).ready(function () {
    var $overlay = $("#overlay");
    var $popup = $("#popupLogin");

    // Mở popup khi nhấn vào user icon
    $("#userIcon").click(function () {
        $overlay.fadeIn(); // Hiển thị overlay
        $popup.css("animation", "slideInRight 0.4s ease").fadeIn(); // Hiển thị popup với hiệu ứng
    });

    // Đóng popup khi nhấn vào overlay
    $overlay.click(function () {
        closePopup();
    });

    // Đóng popup khi nhấn vào nút đóng (nếu có)
    $("#closePopup").click(function () {
        closePopup();
    });

    function closePopup() {
        $popup.css("animation", "slideOutRight 0.4s ease").fadeOut(); // Ẩn popup với hiệu ứng
        $overlay.fadeOut(); // Ẩn overlay
    }
});