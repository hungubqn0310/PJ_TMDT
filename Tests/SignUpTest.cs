// using NUnit.Framework;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using System;
// using OpenQA.Selenium.Interactions;

// namespace EcommerceTests
// {
//     public class SignUpTest
//     {
//         IWebDriver driver;
//         WebDriverWait wait;

//         [SetUp] // Chạy trước mỗi test case
//         public void Setup()
//         {
//             ChromeOptions options = new ChromeOptions();
//             options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

//             driver = new ChromeDriver(options);
//             driver.Manage().Window.Maximize();
//             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15)); // Tăng thời gian chờ lên 15 giây
//         }

//         [Test]
//         public void Test_SignUp_Success()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");

//             // Chờ form hiển thị
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Email")));

//             // Nhập thông tin đăng ký
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             // Tìm nút "Đăng Ký"
//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));

//             // Cuộn đến nút để tránh bị che khuất
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500); // Chờ 0.5 giây để ổn định

//             // Dùng Actions để click tránh lỗi click bị chặn
//             Actions actions = new Actions(driver);
//             actions.MoveToElement(signUpButton).Click().Perform();

//             // Chờ trang đăng ký xử lý và chuyển hướng
//             System.Threading.Thread.Sleep(2000); // Chờ 2 giây để kiểm tra URL
//             Console.WriteLine("Current URL after sign-up: " + driver.Url);

//             // Chờ trang chủ hoặc trang đích sau khi đăng ký
//             bool isRedirected = wait.Until(drv =>
//                 drv.Url.Contains("dashboard") ||
//                 drv.Url == "https://localhost:5003/" // Trang chủ
//             );

//             // Kiểm tra URL hoặc phần tử đặc trưng của trang chủ
//             Assert.That(isRedirected, Is.True, "Chuyển hướng sau đăng ký không đúng!");
//             Assert.That(driver.Url, Is.EqualTo("https://localhost:5003/").Or.Contains("dashboard"));
//         }

//         [TearDown] // Chạy sau mỗi test case
//         public void TearDown()
//         {
//             driver.Quit();
//         }
//     }
// }
