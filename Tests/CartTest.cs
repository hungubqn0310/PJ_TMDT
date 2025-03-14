// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using Xunit;
// using System.Threading;

// public class SeleniumOrderPlacementTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;

//     public SeleniumOrderPlacementTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//         options.BinaryLocation = @"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
//         driver = new ChromeDriver(options);
//         driver.Manage().Window.Maximize();
//         wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); // Tăng timeout lên 30 giây
//     }

//     [Fact]
//     public void TestOrderPlacement()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test chức năng đặt hàng...");
//             driver.Navigate().GoToUrl("https://localhost:5003");
//             Console.WriteLine("Đã truy cập trang chính.");

//             // Mở popup đăng nhập
//             ClickElement(By.ClassName("user-icon"), "Mở popup đăng nhập");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

//             // Nhập thông tin đăng nhập
//             EnterText(By.Id("email"), "user1");
//             EnterText(By.Id("password"), "123456");
//             Console.WriteLine("Đã nhập thông tin đăng nhập.");

//             // Nhấn nút đăng nhập
//             ClickElement(By.Id("loginButton"), "Nhấn nút đăng nhập");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
//             Console.WriteLine("Đăng nhập thành công.");

//             // Mở giỏ hàng
//             ClickElement(By.CssSelector(".shopping-cart-icon"), "Mở giỏ hàng");
            
//             // Tiến hành thanh toán
//             ClickElement(By.CssSelector("a.checkout-button"), "Nhấn nút Tiến hành thanh toán");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("checkoutForm")));
//             Console.WriteLine("Đã vào trang thanh toán.");

//             // Nhập thông tin đặt hàng
//             EnterText(By.Id("fullName"), "Nguyễn Văn A");
//             EnterText(By.Id("phone"), "0123456789");
//             EnterText(By.Id("emailInput"), "email@example.com");
//             EnterText(By.Id("addressInput"), "123 Đường ABC, TP.HCM");
//             Console.WriteLine("Đã nhập thông tin đặt hàng.");

//             // Cuộn xuống và nhấn nút đặt hàng
//             ScrollToElement(By.XPath("//button[@class='order-button']"));
//             ClickElement(By.XPath("//button[@class='order-button']"), "Nhấn nút Đặt hàng");

//             // Chờ xác nhận đặt hàng thành công
//             wait.Until(ExpectedConditions.UrlContains("paymentSuccessful"));
//             Console.WriteLine("Chuyển sang trang thanh toán thành công.");

//             // Kiểm tra nội dung xác nhận
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

//     private void EnterText(By by, string text)
//     {
//         try
//         {
//             var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
//             ScrollToElement(by);
//             element.Clear();
//             element.SendKeys(text);
//             Console.WriteLine($"Nhập {text} vào {by} thành công.");
//         }
//         catch (WebDriverTimeoutException)
//         {
//             Console.WriteLine($"Không tìm thấy phần tử {by} để nhập text.");
//             throw;
//         }
//     }

//     private void ClickElement(By by, string actionDescription)
//     {
//         try
//         {
//             var element = wait.Until(ExpectedConditions.ElementToBeClickable(by));
//             ScrollToElement(by);
//             element.Click();
//             Console.WriteLine(actionDescription + " thành công.");
//         }
//         catch (WebDriverTimeoutException)
//         {
//             Console.WriteLine($"Lỗi: Không tìm thấy phần tử {by} để thực hiện {actionDescription}.");
//             throw;
//         }
//     }

//     private void ScrollToElement(By by)
//     {
//         var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
//         ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'});", element);
//         Thread.Sleep(500); // Chờ một chút để cuộn hoàn tất
//     }

//     public void Dispose()
//     {
//         driver.Quit();
//     }
// }