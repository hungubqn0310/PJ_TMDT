// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using Xunit;

// public class SeleniumLoginPopupTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;

//     public SeleniumLoginPopupTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//         options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";  

//         driver = new ChromeDriver(options);
//         driver.Manage().Window.Maximize();
//         wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
//     }

//     [Fact]
//     public void TestLoginPopup()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test đăng nhập qua popup...");

//             // Truy cập trang web
//             driver.Navigate().GoToUrl("https://localhost:5003");

//             // Chờ user-icon xuất hiện và mở popup đăng nhập
//             IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
//             userIcon.Click();
//             Console.WriteLine("Đã mở popup đăng nhập.");

//             // Chờ popup đăng nhập hiển thị
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

//             // Nhập thông tin đăng nhập
//             driver.FindElement(By.Id("email")).SendKeys("user1");
//             driver.FindElement(By.Id("password")).SendKeys("123456");

//             Console.WriteLine("Đã nhập thông tin đăng nhập.");

//             // Nhấn nút đăng nhập
//             IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
//             loginButton.Click();
//             Console.WriteLine("Đã nhấn nút đăng nhập.");

//             // **Chờ username xuất hiện thay vì user-icon**
//             IWebElement usernameLabel = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
//             Console.WriteLine($"Đăng nhập thành công, thấy username: {usernameLabel.Text}");

//             // **Click vào username để mở menu đăng xuất**
//             usernameLabel.Click();
//             Console.WriteLine("Đã mở menu sau đăng nhập.");

//             // **Chờ dropdown xuất hiện**
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("dropdownn-item")));

//             // **Tìm và bấm vào nút "Đăng xuất"**
//             IWebElement logoutButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(text(),'Đăng xuất')]")));
//             Assert.True(logoutButton.Displayed, "Nút đăng xuất không xuất hiện sau đăng nhập!");

//             // Nhấn nút đăng xuất
//             logoutButton.Click();
//             Console.WriteLine("Đã nhấn nút đăng xuất.");

//             // **Kiểm tra đăng xuất thành công** (chờ user-icon xuất hiện lại)
//             wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
//             Console.WriteLine("Đã đăng xuất thành công.");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Lỗi xảy ra: " + e.ToString());
//             throw;
//         }
//     }
//     [Fact]
//     public void TestLoginPopup_Failed()
//     {
//         try
//         {
//             Console.WriteLine("Bắt đầu test đăng nhập thất bại...");

//             // Truy cập trang web
//             driver.Navigate().GoToUrl("https://localhost:5003");

//             // Chờ user-icon xuất hiện và mở popup đăng nhập
//             IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
//             userIcon.Click();
//             Console.WriteLine("Đã mở popup đăng nhập.");

//             // Chờ popup đăng nhập hiển thị
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

//             // Nhập thông tin đăng nhập sai
//             driver.FindElement(By.Id("email")).SendKeys("user1");
//             driver.FindElement(By.Id("password")).SendKeys("sai_mk");

//             Console.WriteLine("Đã nhập thông tin đăng nhập sai.");

//             // Nhấn nút đăng nhập
//             IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
//             loginButton.Click();
//             Console.WriteLine("Đã nhấn nút đăng nhập.");

//             // Chờ và kiểm tra thông báo lỗi
//             IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("text-error")));

//             string expectedErrorMessage = "Tài khoản hoặc mật khẩu không đúng !";
//             string actualErrorMessage = errorMessage.GetAttribute("innerHTML").Trim(); 

//             Assert.Equal(expectedErrorMessage, actualErrorMessage);
//             Console.WriteLine($"Thông báo lỗi hiển thị: {actualErrorMessage}");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Lỗi xảy ra: " + e.ToString());
//             throw;
//         }
//     }
//     [Fact]
//     public void TestLoginButton_Disabled_When_EmptyFields()
//     {
//         Console.WriteLine("Bắt đầu kiểm thử: Nút đăng nhập không hoạt động khi ô trống...");

//         // Truy cập trang web
//         driver.Navigate().GoToUrl("https://localhost:5003");

//         // Mở popup đăng nhập
//         IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
//         userIcon.Click();
//         wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

//         // Xác định nút đăng nhập
//         IWebElement loginButton = driver.FindElement(By.Id("loginButton"));

//         // Kiểm tra nếu nút bị vô hiệu hóa (disabled)
//         bool isDisabled = !loginButton.Enabled;
//         Assert.True(isDisabled, "Nút đăng nhập không bị vô hiệu hóa khi bỏ trống ô nhập!");
//         Console.WriteLine("✅ Đã xác nhận nút đăng nhập bị vô hiệu hóa khi bỏ trống.");
//     }

//     public void Dispose()
//     {
//         driver.Quit();
//     }
// }
