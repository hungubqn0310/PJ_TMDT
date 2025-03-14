// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using OpenQA.Selenium.Interactions;
// using SeleniumExtras.WaitHelpers;
// using Xunit;
// using System.Threading;

// public class SeleniumCancelOrderTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;
//     private Actions actions;

//     public SeleniumCancelOrderTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//         options.BinaryLocation = @"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
//         driver = new ChromeDriver(options);
//         driver.Manage().Window.Maximize();
//         wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
//         actions = new Actions(driver);
//     }

//     [Fact]
//     public void TestCancelOrder()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test chức năng huỷ đơn hàng...");
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

//             // Trỏ chuột vào tên người dùng (hover) để hiển thị dropdown
//             var userDropdownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("dropdown-btn")));
//             actions.MoveToElement(userDropdownBtn).Perform();
//             Console.WriteLine("Đã trỏ chuột vào tên người dùng.");

//             // Chờ dropdown menu hiển thị
//             wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropdownn-menu")));

//             // Click vào "Lịch sử mua hàng"
//             ClickElement(By.XPath("//a[contains(text(),'Lịch sử mua hàng')]"), "Click vào Lịch sử mua hàng");

//             // Chờ trang lịch sử mua hàng tải xong
//             wait.Until(ExpectedConditions.UrlContains("transactionhistory"));
//             Console.WriteLine("Đã chuyển đến trang lịch sử mua hàng.");

//             // Click vào nút huỷ đơn hàng của đơn hàng đầu tiên (dùng class "cancel-button")
//             ClickElement(By.CssSelector(".cancel-button"), "Click vào nút Huỷ đơn hàng");

//             // Xử lý alert xác nhận huỷ đơn hàng
//             try
//             {
//                 wait.Until(ExpectedConditions.AlertIsPresent());
//                 IAlert alert = driver.SwitchTo().Alert();
//                 Console.WriteLine("Alert hiện lên với nội dung: " + alert.Text);
//                 alert.Accept(); // Chấp nhận alert (tương đương nhấn OK)
//                 Console.WriteLine("Đã chấp nhận alert xác nhận huỷ đơn hàng.");
                
//                 // Xử lý alert thông báo thành công
//                 wait.Until(ExpectedConditions.AlertIsPresent());
//                 IAlert successAlert = driver.SwitchTo().Alert();
//                 Console.WriteLine("Alert thông báo: " + successAlert.Text);
//                 successAlert.Accept();
//                 Console.WriteLine("Đã xác nhận thông báo huỷ đơn hàng thành công.");
//             }
//             catch (WebDriverTimeoutException)
//             {
//                 Console.WriteLine("Không có alert xác nhận, tiếp tục kiểm tra kết quả huỷ đơn hàng.");
//             }

//             // Đợi một chút để trang cập nhật sau khi xử lý alert
//             Thread.Sleep(2000);

            
            
//             Console.WriteLine("Test huỷ đơn hàng thành công!");
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
//         { var element = wait.Until(ExpectedConditions.ElementToBeClickable(by));
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

//     private bool IsAlertPresent()
//     {
//         try
//         {
//             driver.SwitchTo().Alert();
//             return true;
//         }
//         catch (NoAlertPresentException)
//         {
//             return false;
//         }
//     }

//     private void HandleAllAlerts()
//     {
//         // Xử lý tất cả các alert đang hiển thị
//         while (IsAlertPresent())
//         {
//             try
//             {
//                 IAlert alert = driver.SwitchTo().Alert();
//                 Console.WriteLine("Xử lý alert: " + alert.Text);
//                 alert.Accept();
//                 Thread.Sleep(500); // Đợi một chút giữa các alert
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Lỗi khi xử lý alert: " + ex.Message);
//                 break;
//             }
//         }
//     }

//     public void Dispose()
//     {
//         try
//         {
//             // Xử lý tất cả alert còn sót lại trước khi đóng browser
//             HandleAllAlerts();
//         }
//         catch (Exception)
//         {
//             // Bỏ qua lỗi khi xử lý alert trong quá trình đóng browser
//         }
        
//         driver.Quit();
//     }
// }