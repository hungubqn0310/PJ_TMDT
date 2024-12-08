document.addEventListener("DOMContentLoaded", function () {
  const tinhThanh = document.getElementById("tinhThanh");

  fetch("https://provinces.open-api.vn/api/p/")
    .then(response => response.json())
    .then(data => {
      data.forEach(province => {
        const option = document.createElement("option");
        option.value = province.code;
        option.textContent = province.name;
        tinhThanh.appendChild(option);
      });
    })
    .catch(error => console.error("Lỗi API:", error));
});