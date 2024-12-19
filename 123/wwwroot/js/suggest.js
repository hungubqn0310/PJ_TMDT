let currentPosition = 0; // Vị trí hiện tại của slider
const slider = document.querySelector(".coTheBanSeThich-slider");
const items = document.querySelectorAll(".coTheBanSeThich-item");
const btnPrev = document.querySelector(".btn-prev");
const btnNext = document.querySelector(".btn-next");
const itemWidth = 142.5 + 20; // Kích thước mỗi ảnh + gap (20px)

function updateButtons() {
    // Ẩn nút Prev nếu đang ở đầu
    btnPrev.style.display = currentPosition === 0 ? "none" : "block";

    // Ẩn nút Next nếu đang ở cuối (tùy theo số lượng ảnh)
    const maxPosition = -(items.length - 6) * itemWidth;
    btnNext.style.display = currentPosition === maxPosition ? "none" : "block";
}

function slideLeft() {
    // Di chuyển slider sang trái
    if (currentPosition < 0) {
        currentPosition += itemWidth;
        slider.style.transform = `translateX(${currentPosition}px)`;
        updateButtons();
    }
}

function slideRight() {
    // Di chuyển slider sang phải
    const maxPosition = -(items.length - 6) * itemWidth;
    if (currentPosition > maxPosition) {
        currentPosition -= itemWidth;
        slider.style.transform = `translateX(${currentPosition}px)`;
        updateButtons();
    }
}

// Gắn sự kiện click cho nút Prev và Next
btnPrev.addEventListener("click", slideLeft);
btnNext.addEventListener("click", slideRight);

// Khởi tạo trạng thái ban đầu
updateButtons();