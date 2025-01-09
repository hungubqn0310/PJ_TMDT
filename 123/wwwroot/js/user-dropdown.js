// user-dropdown.js
window.onload = function() {
    var userId = localStorage.getItem('id');  // Lấy id người dùng từ localStorage với key là 'id'

    if (userId) {
        // Nếu có id người dùng, hiển thị dropdown và thông tin người dùng
        document.getElementById('user-dropdown').style.display = 'block';  // Hiển thị dropdown
        
    } else {
        // Nếu không có id người dùng, ẩn dropdown
        document.getElementById('user-dropdown').style.display = 'none';
    }
}

// Hàm đăng xuất
function logout() {
    // Xóa thông tin người dùng trong localStorage
    localStorage.removeItem('id');
    
    // Ẩn dropdown và đưa về trạng thái ban đầu
    document.getElementById('user-dropdown').style.display = 'none';
    
    // Cập nhật lại giao diện (có thể chuyển hướng về trang đăng nhập)
    window.location.href = '/login';  // Điều hướng đến trang đăng nhập (nếu có)
}