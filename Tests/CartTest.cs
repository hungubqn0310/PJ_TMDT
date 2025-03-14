// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using Xunit;

// public class SeleniumOrderPlacementTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;

//     public SeleniumOrderPlacementTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//         options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";  

//         driver = new ChromeDriver(options);
//         driver.Manage().Window.Maximize();
//         wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
//     }

//     [Fact]
//     public void TestOrderPlacement()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test chức năng đặt hàng...");

//             // 1. Truy cập trang chính và đăng nhập
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

//             // Xác nhận đăng nhập thành công
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
//             Console.WriteLine("Đăng nhập thành công.");

//             // 2. Nhấn vào biểu tượng giỏ hàng
//             IWebElement cartIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".shopping-cart-icon")));
//             cartIcon.Click();
//             Console.WriteLine("Đã mở giỏ hàng.");

//             // 3. Nhấn nút "Tiến hành thanh toán"
//             var proceedToCheckoutButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.checkout-button")));
//             proceedToCheckoutButton.Click();
//             Console.WriteLine("Đã nhấn nút Tiến hành thanh toán.");

//             // 4. Nhập thông tin vào form đặt hàng
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("checkoutForm"))); // Chờ form thanh toán hiển thị
//             driver.FindElement(By.Id("fullName")).SendKeys("Nguyễn Văn A");
//             driver.FindElement(By.Id("phone")).SendKeys("0123456789");
//             driver.FindElement(By.Id("email")).SendKeys("email@example.com");
//             driver.FindElement(By.Id("address")).SendKeys("123 Đường ABC, TP.HCM");
//             Console.WriteLine("Đã nhập thông tin đặt hàng.");

//             // 5. Cuộn xuống để tìm nút Đặt hàng
//             var checkoutButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='order-button']")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", checkoutButton);
//             wait.Until(ExpectedConditions.ElementToBeClickable(checkoutButton)); // Đợi cho nút có thể click được
            
//             // Nhấn nút Đặt hàng
//             checkoutButton.Click();
//             Console.WriteLine("Đã nhấn nút Đặt hàng.");

//             // 6. Chờ xác nhận đặt hàng thành công
//             wait.Until(ExpectedConditions.UrlContains("paymentSuccessful"));
//             Console.WriteLine("Chuyển sang trang thanh toán thành công.");

//             // 7. Kiểm tra nội dung trang thanh toán thành công
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("success-message")));
//             var successMessage = driver.FindElement(By.XPath("//h2[text()='Cảm ơn bạn. Đơn hàng của bạn đã được nhận.']"));
//             Assert.NotNull(successMessage);
//             Console.WriteLine("Xác nhận đơn hàng thành công hiển thị đúng.");
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