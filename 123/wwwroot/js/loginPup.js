$(document).ready(function () {
    var $overlay = $("#overlay");
    var $popup = $("#popupLogin");

    // Mở popup khi nhấn vào user icon
    $(".user-icon, .shopping-cart").click(function () {
        $overlay.fadeIn();
        $popup.css("animation", "slideInRight 0.4s ease").fadeIn();
    });

    // Đóng popup khi nhấn vào overlay
    $overlay.click(function () {
        closePopup();
    });

    // Kiểm tra thông tin khi nhấn nút đăng nhập
    $("#loginButton").click(function (event) {
        event.preventDefault(); // Ngừng gửi form

        var emailOrPhone = $("#email").val();
        var password = $("#password").val();
        var isValid = true;

        // Đặt thông báo lỗi rỗng trước
        $("#email-error-message").text("").fadeOut();
        $("#password-error-message").text("").fadeOut();

        // Kiểm tra email hoặc số điện thoại
        var emailPhonePattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        var phonePattern = /^[0-9]{10,11}$/;
        var emailOrPhoneValid = emailPhonePattern.test(emailOrPhone) || phonePattern.test(emailOrPhone);
        if (!emailOrPhoneValid) {
            isValid = false;
            $("#email-error-message").text("Email hoặc số điện thoại không đúng").fadeIn(); // Hiển thị thông báo lỗi cho email/số điện thoại
        }

        // Kiểm tra mật khẩu
        if (password.length < 6) {
            isValid = false;
            $("#password-error-message").text("Mật khẩu phải từ 6 ký tự trở lên").fadeIn(); // Hiển thị thông báo lỗi cho mật khẩu
        }

        // Nếu dữ liệu hợp lệ, thực hiện đăng nhập
        if (isValid) {
            $("#email-error-message").fadeOut();
            $("#password-error-message").fadeOut(); // Ẩn thông báo lỗi nếu đúng
            console.log("Đang đăng nhập...");
        }

        // Disable/Enable login button dựa trên tính hợp lệ
        $("#loginButton").prop("disabled", !isValid);
    });

    // Hàm đóng popup
    function closePopup() {
        $popup.css("animation", "slideOutRight 0.4s ease").fadeOut();
        $overlay.fadeOut();
    }
});