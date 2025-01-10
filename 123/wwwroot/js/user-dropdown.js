window.onload = function () {
    var userId = localStorage.getItem('id'); // Lấy id người dùng từ localStorage với key là 'id'
    var shoppingCartIcon = document.querySelector('.shopping-cart-icon');

    if (userId) {
        // Nếu đã đăng nhập, cho phép href link đến giỏ hàng
        shoppingCartIcon.setAttribute('href', '/home/cart');
        document.getElementById('user-dropdown').style.display = 'block'; // Hiển thị dropdown
        document.getElementById('user-icon').style.display = 'none'; // Ẩn user-icon
    } else {
        // Nếu chưa đăng nhập, xóa href để không điều hướng
        shoppingCartIcon.removeAttribute('href');
        document.getElementById('user-dropdown').style.display = 'none'; // Ẩn dropdown
        document.getElementById('user-icon').style.display = 'flex'; // Hiển thị user-icon
    }
};

// Hàm đăng xuất
function logout() {
    // Xóa thông tin người dùng trong localStorage
    localStorage.removeItem('id');

    // Cập nhật lại giao diện
    document.getElementById('user-dropdown').style.display = 'none'; // Ẩn dropdown
    document.getElementById('user-icon').style.display = 'flex'; // Hiển thị lại user-icon

    // Reset trạng thái giỏ hàng
    var shoppingCartIcon = document.querySelector('.shopping-cart-icon');
    shoppingCartIcon.removeAttribute('href'); // Xóa href khi đăng xuất

    // Điều hướng về trang đăng nhập (nếu có)
    window.location.href = '/login';
}
