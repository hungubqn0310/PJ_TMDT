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

//             // Nhập thông tin đăng ký đầy đủ
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             // Tìm nút "Đăng Ký"
//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));

//             // Cuộn đến nút và click
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();

//             // Chờ chuyển hướng
//             System.Threading.Thread.Sleep(2000);
//             Console.WriteLine("Current URL after sign-up: " + driver.Url);

//             // Kiểm tra chuyển hướng: nếu đăng ký thành công, URL sẽ khác /Account/SignUp
//             Assert.That(driver.Url, Is.Not.Contains("/Account/SignUp"), "Đăng ký thành công nhưng vẫn ở trang SignUp!");
//         }

//         // Test khi bỏ trống Username
//         [Test]
//         public void Test_SignUp_MissingUsername()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Email")));

//             // Bỏ trống Username, điền các trường còn lại
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             // Nếu đăng ký thất bại, URL vẫn chứa "/Account/SignUp"
//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi Username bị bỏ trống");
//         }

//         // Test khi bỏ trống Email
//         [Test]
//         public void Test_SignUp_MissingEmail()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Username")));

//             // Điền Username và bỏ trống Email
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi Email bị bỏ trống");
//         }

//         // Test khi bỏ trống Password
//         [Test]
//         public void Test_SignUp_MissingPassword()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Username")));

//             // Điền Username, Email, và bỏ trống Password
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             // Bỏ trống Password
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi Password bị bỏ trống");
//         }

//         // Test khi bỏ trống ConfirmPassword
//         [Test]
//         public void Test_SignUp_MissingConfirmPassword()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Username")));

//             // Điền Username, Email, Password và bỏ trống ConfirmPassword
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             // Bỏ trống ConfirmPassword
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi Confirm Password bị bỏ trống");
//         }

//         // Test khi bỏ trống PhoneNumber
//         [Test]
//         public void Test_SignUp_MissingPhoneNumber()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Username")));

//             // Điền các trường còn lại, bỏ trống PhoneNumber
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             // Bỏ trống PhoneNumber
//             driver.FindElement(By.Name("Address")).SendKeys("123 Đường ABC, Hà Nội");

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi PhoneNumber bị bỏ trống");
//         }

//         // Test khi bỏ trống Address
//         [Test]
//         public void Test_SignUp_MissingAddress()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Username")));

//             // Điền các trường còn lại, bỏ trống Address
//             driver.FindElement(By.Name("Username")).SendKeys("testuser");
//             driver.FindElement(By.Name("Email")).SendKeys("test@example.com");
//             driver.FindElement(By.Name("Password")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("ConfirmPassword")).SendKeys("Test@1234");
//             driver.FindElement(By.Name("PhoneNumber")).SendKeys("0123456789");
//             // Bỏ trống Address

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi Address bị bỏ trống");
//         }

//         // Test khi không nhập gì cả
//         [Test]
//         public void Test_SignUp_AllEmpty()
//         {
//             driver.Navigate().GoToUrl("https://localhost:5003/Account/SignUp");
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Username")));
//             // Không nhập gì cả

//             IWebElement signUpButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Đăng Ký')]")));
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", signUpButton);
//             System.Threading.Thread.Sleep(500);
//             new Actions(driver).MoveToElement(signUpButton).Click().Perform();
//             System.Threading.Thread.Sleep(2000);

//             Assert.That(driver.Url, Does.Contain("/Account/SignUp"), "Đăng ký không nên thành công khi không nhập dữ liệu");
//         }

//         [TearDown] // Chạy sau mỗi test case
//         public void TearDown()
//         {
//             driver.Quit();
//         }
//     }
// }
