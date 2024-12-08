var builder = WebApplication.CreateBuilder(args);

// Thêm HttpClient vào dịch vụ
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Các route khác
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Định nghĩa route cho các trang sản phẩm
app.MapControllerRoute(
    name: "productPages",
    pattern: "category/{category}",
    defaults: new { controller = "Home", action = "Category" });

// Định nghĩa route cho các trang thông tin
app.MapControllerRoute(
    name: "infoPages",
    pattern: "thongTin/{topic}",
    defaults: new { controller = "Home", action = "ThongTin" });

app.MapControllerRoute(
    name: "khuyenMai",
    pattern: "khuyenMai",
    defaults: new { controller = "Home", action = "KhuyenMai" });

app.MapControllerRoute(
    name: "cauChuyenHanDK",
    pattern: "cauChuyenHanDK",
    defaults: new { controller = "Home", action = "CauChuyenHanDK" });

app.MapControllerRoute(
    name: "cuaHangHanDK",
    pattern: "cuaHangHanDK",
    defaults: new { controller = "Home", action = "CuaHangHanDK" });

app.Run();
