// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using OpenQA.Selenium.Interactions;
// using SeleniumExtras.WaitHelpers;
// using Xunit;
// using System.Threading;
// using System.IO; // Thêm namespace này

// public class SeleniumUserInfoTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;
//     private Actions actions;

//     public SeleniumUserInfoTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//         options.BinaryLocation = @"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
//         driver = new ChromeDriver(options);
//         driver.Manage().Window.Maximize();
//         wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
//         actions = new Actions(driver);
//     }

//     [Fact]
//     public void TestChangePassword()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test chức năng đổi mật khẩu...");
//             LoginAndNavigateToUserInfo();

//             // Click vào nút "Đổi mật khẩu"
//             ClickElement(By.XPath("//button[contains(text(),'Đổi mật khẩu')]"), "Click vào nút Đổi mật khẩu");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("changePasswordForm")));
//             Console.WriteLine("Form đổi mật khẩu hiển thị.");

//             // Nhập mật khẩu mới
//             EnterText(By.Id("newPassword"), "newpassword123");
//             Console.WriteLine("Đã nhập mật khẩu mới.");

//             // Click nút "Lưu mật khẩu"
//             ClickElement(By.XPath("//button[contains(text(),'Lưu mật khẩu')]"), "Click vào nút Lưu mật khẩu");
            
//             // Xử lý alert thông báo (nếu có)
//             HandleAlert("Đổi mật khẩu thành công");
            
//             // Kiểm tra form đã đóng
//             wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("changePasswordForm")));
//             Console.WriteLine("Form đổi mật khẩu đã đóng sau khi lưu.");
            
//             Console.WriteLine("Test đổi mật khẩu thành công!");
            
//             // Đăng xuất và đăng nhập lại với mật khẩu mới để kiểm tra
//             // (Phần này có thể bỏ qua vì cần thêm logic đăng xuất và đăng nhập lại)
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Lỗi xảy ra: " + e.ToString());
//             TakeScreenshot("change_password_error");
//             throw;
//         }
//     }
//     private void LoginAndNavigateToUserInfo()
//     {
//         driver.Navigate().GoToUrl("https://localhost:5003");
//         Console.WriteLine("Đã truy cập trang chính.");

//         // Mở popup đăng nhập
//         ClickElement(By.ClassName("user-icon"), "Mở popup đăng nhập");
//         wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

//         // Nhập thông tin đăng nhập
//         EnterText(By.Id("email"), "user1");
//         EnterText(By.Id("password"), "123456");
//         Console.WriteLine("Đã nhập thông tin đăng nhập.");

//         // Nhấn nút đăng nhập
//         ClickElement(By.Id("loginButton"), "Nhấn nút đăng nhập");
//         wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
//         Console.WriteLine("Đăng nhập thành công.");

//         // Trỏ chuột vào tên người dùng (hover) để hiển thị dropdown
//         var userDropdownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("dropdown-btn")));
//         actions.MoveToElement(userDropdownBtn).Perform();
//         Console.WriteLine("Đã trỏ chuột vào tên người dùng.");

//         // Chờ dropdown menu hiển thị
//         wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropdownn-menu")));

//         // Click vào "Thông tin cá nhân"
//         ClickElement(By.XPath("//a[contains(text(),'Thông tin cá nhân')]"), "Click vào Thông tin cá nhân");

//         // Chờ trang thông tin cá nhân tải xong
//         wait.Until(ExpectedConditions.UrlContains("UserInfo"));
//         wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("profile-info")));
//         Console.WriteLine("Đã chuyển đến trang thông tin cá nhân.");
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

//     private void ClearAndEnterText(By by, string text)
//     {
//         try
//         {
//             var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
//             ScrollToElement(by);
//             element.Clear();
//             Thread.Sleep(300); // Đợi một chút sau khi xóa
//             element.SendKeys(text);
//             Console.WriteLine($"Xóa và nhập {text} vào {by} thành công.");
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

//     private void HandleAlert(string expectedText = "")
//     {
//         try
//         {
//             wait.Until(ExpectedConditions.AlertIsPresent());
//             IAlert alert = driver.SwitchTo().Alert();
            
//             if (!string.IsNullOrEmpty(expectedText))
//             {
//                 Console.WriteLine($"Alert hiển thị: {alert.Text}");
//                 // Kiểm tra nội dung alert nếu cần
//             }
            
//             alert.Accept();
//             Console.WriteLine("Đã xác nhận alert.");
//             Thread.Sleep(1000); // Đợi sau khi xử lý alert
//         }
//         catch (WebDriverTimeoutException)
//         {
//             Console.WriteLine("Không có alert hiển thị.");
//         }
//     }

//     private void TakeScreenshot(string fileName)
//     {
//         try
//         {
//             Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
//             string path = $"{fileName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.png";
//             screenshot.SaveAsFile(path);
//             Console.WriteLine($"Đã chụp ảnh màn hình: {path}");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine($"Không thể chụp ảnh màn hình: {e.Message}");
//         }
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
            
//             Console.WriteLine("Kết thúc test và đóng trình duyệt...");
            
//             if (driver != null)
//             {
//                 driver.Quit();
//                 driver = null;
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Lỗi khi đóng trình duyệt: " + ex.Message);
//         }
        
//         // Force GC để thu gom tài nguyên
//         GC.Collect();
//         GC.WaitForPendingFinalizers();
//     }
// }