// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using Xunit;

// public class SeleniumAddToCartFromCategoryTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;

//     public SeleniumAddToCartFromCategoryTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//             options.BinaryLocation = @"C:\Users\TGDD\AppData\Local\CocCoc\Browser\Application\browser.exe";
//             driver = new ChromeDriver(options);
//             driver.Manage().Window.Maximize();
//             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Đợi tối đa 10s
//     }

//     [Fact]
//     public void TestAddToCartFromCategory()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test đăng nhập và thêm sản phẩm vào giỏ hàng...");

//             // Truy cập trang web
//             driver.Navigate().GoToUrl("https://localhost:5003");
//             Console.WriteLine("Đã truy cập trang chính.");

//             // Mở popup đăng nhập
//             IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
//             userIcon.Click();
//             Console.WriteLine("Đã mở popup đăng nhập.");

//             // Nhập thông tin đăng nhập
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));
//             driver.FindElement(By.Id("email")).SendKeys("user1");
//             driver.FindElement(By.Id("password")).SendKeys("123456");
//             Console.WriteLine("Đã nhập thông tin đăng nhập.");

//             // Nhấn nút đăng nhập
//             IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
//             loginButton.Click();
//             Console.WriteLine("Đã nhấn nút đăng nhập.");

//             // Chờ username xuất hiện để xác nhận đăng nhập thành công
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
//             Console.WriteLine("Đăng nhập thành công.");

//             // Truy cập trang danh sách sản phẩm
//             driver.Navigate().GoToUrl("https://localhost:5003/category/nhanCauHon");
//             Console.WriteLine("Đã truy cập trang danh sách sản phẩm.");

//             // Chờ danh sách sản phẩm hiển thị và nhấn vào ảnh sản phẩm
//             var productImage = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//img[@alt='Emerald Engagement Ring']")));
//             productImage.Click();
//             Console.WriteLine("Đã nhấn vào ảnh sản phẩm để xem chi tiết.");

//             // Chờ trang chi tiết sản phẩm hiển thị
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("product-detail-wrapper")));

//             // Cuộn xuống để hiển thị nút thêm vào giỏ hàng
//             var addToCartButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cartButton")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addToCartButton);
//             Console.WriteLine("Đã cuộn xuống để hiển thị nút thêm vào giỏ hàng.");

//             // Nhấn nút thêm vào giỏ hàng
//             addToCartButton.Click();
//             Console.WriteLine("Đã nhấn nút thêm vào giỏ hàng.");

//             // Chờ thông báo thêm vào giỏ hàng
//             var alert = wait.Until(ExpectedConditions.AlertIsPresent());
//             Console.WriteLine(alert.Text); // In ra thông báo
//             alert.Accept(); // Đóng thông báo

//             Console.WriteLine("Thêm vào giỏ hàng thành công.");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Lỗi xảy ra: " + e.ToString());
//             throw;
//         }
//     }

//     public void Dispose()
//     {
//         driver.Quit();
//     }
// }