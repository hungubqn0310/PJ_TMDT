using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Identity;
using _123.Models;
using _123.Models.Momo;
using _123.Services.Momo;
var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ khác
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
//ConnectMoMo
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
// Cấu hình dịch vụ cho Identity và MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Cấu hình cổng HTTP/HTTPS
app.Urls.Add("http://localhost:5002");
app.Urls.Add("https://localhost:5003");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Bổ sung middleware xác thực
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/api/music", async context =>
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audio");
        var files = Directory.GetFiles(directoryPath, "*.mp3");
        var fileNames = files.Select(f => Path.GetFileName(f)).ToArray();
        await context.Response.WriteAsJsonAsync(fileNames);
    });
});

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