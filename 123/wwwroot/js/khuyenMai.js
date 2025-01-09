document.addEventListener("DOMContentLoaded", () => {
    // Xử lý sự kiện cho tất cả các icon yêu thích
    const favoriteIcons = document.querySelectorAll(".favorite-icon");
    favoriteIcons.forEach((icon) => {
        icon.addEventListener("click", () => {
            icon.style.transform = "scale(1.2)"; // Phóng to nhẹ khi click
            setTimeout(() => (icon.style.transform = "scale(1)"), 200); // Trả về kích thước ban đầu
            if (icon.classList.contains("far")) {
                icon.classList.remove("far");
                icon.classList.add("fas"); // Đổi thành trái tim đầy
                icon.style.color = "red"; // Chuyển màu từ hồng sang đỏ
            } else {
                icon.classList.remove("fas");
                icon.classList.add("far"); // Quay lại trái tim rỗng
                icon.style.color = "pink"; // Chuyển màu từ đỏ sang hồng
            }
        });
    });
    // Xử lý sự kiện cho tất cả các icon giỏ hàng
    const cartIcons = document.querySelectorAll(".cart-icon");
    cartIcons.forEach((icon) => {
        icon.addEventListener("click", () => {
            icon.style.transform = "scale(1.2)"; // Phóng to nhẹ khi click
            setTimeout(() => (icon.style.transform = "scale(1)"), 200); // Trả về kích thước ban đầu
            if (icon.classList.contains("fa-cart-arrow-down")) {
                icon.classList.remove("fa-cart-arrow-down");
                icon.classList.add("fa-shopping-cart"); // Chuyển sang giỏ hàng đầy
                icon.style.color = "red"; // Chuyển màu từ hồng sang đỏ
            } else {
                icon.classList.remove("fa-shopping-cart");
                icon.classList.add("fa-cart-arrow-down"); // Quay lại giỏ hàng xuống
                icon.style.color = "pink"; // Chuyển màu từ đỏ sang hồng
            }
        });
    });
});