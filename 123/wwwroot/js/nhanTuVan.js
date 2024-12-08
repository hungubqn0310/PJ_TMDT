document.addEventListener('DOMContentLoaded', function () {
    // Các trường nhập liệu
    const hoVaTen = document.getElementById('hoVaTen');
    const sdt = document.getElementById('sdt');
    const tinhThanh = document.getElementById('tinhThanh');
    const submitButton = document.querySelector('.nhanTuVan-submit');

    // Kiểm tra hợp lệ khi nhấn nút submit
    submitButton.addEventListener('click', function (event) {
        let isValid = true;
        
        // Kiểm tra trường "Họ và tên"
        if (hoVaTen.value.trim() === "") {
            isValid = false;
            alert("Vui lòng nhập họ và tên.");
        }
        
        // Kiểm tra trường "Số điện thoại"
        const phonePattern = /^[0-9]{10,11}$/;
        if (!phonePattern.test(sdt.value.trim())) {
            isValid = false;
            alert("Vui lòng nhập số điện thoại hợp lệ.");
        }
        
        // Kiểm tra trường "Tỉnh thành"
        if (tinhThanh.value === "") {
            isValid = false;
            alert("Vui lòng chọn tỉnh thành.");
        }
        
        // Ngừng gửi form nếu không hợp lệ
        if (!isValid) {
            event.preventDefault();
        }
    });
});