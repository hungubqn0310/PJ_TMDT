// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Support.UI;
// using OpenQA.Selenium.Interactions;
// using SeleniumExtras.WaitHelpers;
// using Xunit;
// using System.Threading;

// public class SeleniumAddToCartFromCategoryTest : IDisposable
// {
//     private IWebDriver driver;
//     private WebDriverWait wait;
//     private Actions actions;

//     public SeleniumAddToCartFromCategoryTest()
//     {
//         ChromeOptions options = new ChromeOptions();
//         options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
//         driver = new ChromeDriver(options);
//         driver.Manage().Window.Maximize();
//         wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
//         actions = new Actions(driver);
//     }

//     [Fact]
//     public void TestAddToCartFromCategory()
//     {
//         try
//         {
//             Console.WriteLine("Bat dau test dang nhap va them san pham vao gio hang...");

//             // Truy cap trang web
//             driver.Navigate().GoToUrl("https://localhost:5003");
//             Console.WriteLine("Da truy cap trang chinh.");

//             // Mo popup dang nhap
//             IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
//             userIcon.Click();
//             Console.WriteLine("Da mo popup dang nhap.");

//             // Nhap thong tin dang nhap
//             wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));
//             driver.FindElement(By.Id("email")).SendKeys("user1");
//             driver.FindElement(By.Id("password")).SendKeys("123456");
//             Console.WriteLine("Da nhap thong tin dang nhap.");

//             // Nhan nut dang nhap
//             IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
//             loginButton.Click();
//             Console.WriteLine("Da nhan nut dang nhap.");

//             // Cho username xuat hien de xac nhan dang nhap thanh cong
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
//             Console.WriteLine("Dang nhap thanh cong.");

//             // Tro chuot vao "Danh muc" de hien thi dropdown
//             IWebElement categoryMenu = wait.Until(ExpectedConditions.ElementToBeClickable(
//                 By.XPath("//a[contains(text(),'Danh mục')]")));
            
//             actions.MoveToElement(categoryMenu).Perform();
//             Console.WriteLine("Da tro chuot vao Danh muc.");
            
//             // Doi dropdown hien thi
//             Thread.Sleep(1000); // Doi animation hoan tat
            
//             // Click vao "Nhan cau hon" tu dropdown
//             IWebElement engagementRingLink = wait.Until(ExpectedConditions.ElementToBeClickable(
//                 By.XPath("//a[contains(text(),'Nhẫn cầu hôn')]")));
//             engagementRingLink.Click();
//             Console.WriteLine("Da click vao Nhan cau hon tu dropdown menu.");

//             // Cho trang danh sach san pham hien thi
//             wait.Until(ExpectedConditions.UrlContains("nhanCauHon"));
//             Console.WriteLine("Da chuyen den trang danh sach san pham Nhan cau hon.");

//             // Tim san pham dau tien trong danh sach
//             IWebElement firstProduct = wait.Until(ExpectedConditions.ElementToBeClickable(
//                 By.CssSelector(".product_box_content:first-child .product_name")));

//             // Cuon den san pham de dam bao no hien thi
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", firstProduct);
//             Thread.Sleep(500); // Doi mot chut de phan tu hien thi day du

//             // Click vao san pham de xem chi tiet
//             firstProduct.Click();
//             Console.WriteLine("Da click vao san pham dau tien de xem chi tiet.");

//             // Cho trang chi tiet san pham hien thi
//             wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("product-detail-wrapper")));
//             Console.WriteLine("Da chuyen den trang chi tiet san pham.");

//             // Tim nut "Them vao gio hang" trong trang chi tiet san pham
//             IWebElement addToCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(
//                 By.CssSelector(".btn-cart"))); // Su dung class hoac id cua nut
//             Console.WriteLine("Da tim thay nut Them vao gio hang.");

//             // Cuon den nut de dam bao no hien thi
//             ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", addToCartButton);
//             Thread.Sleep(500); // Doi mot chut de phan tu hien thi day du

//             // Click vao nut "Them vao gio hang"
//             addToCartButton.Click();
//             Console.WriteLine("Da nhan nut Them vao gio hang.");

//             // Xu ly thong bao (neu co)
//             try
//             {
//                 var alert = wait.Until(ExpectedConditions.AlertIsPresent());
//                 Console.WriteLine("Thong bao: " + alert.Text);
//                 alert.Accept(); // Confirm thong bao
//                 Console.WriteLine("Da xac nhan thong bao.");
//             }
//             catch (WebDriverTimeoutException)
//             {
//                 Console.WriteLine("Khong co thong bao alert hien len.");
//             }

//             // Kiem tra gio hang - su dung icon gio hang chinh
//             try
//             {
//                 // Tim icon gio hang chinh (o header hoac navigation)
//                 IWebElement mainCartIcon = wait.Until(ExpectedConditions.ElementToBeClickable(
//                     By.CssSelector(".shopping-cart-icon, .fa-shopping-cart, .cart-icon")));
//                 mainCartIcon.Click();
//                 Console.WriteLine("Da mo gio hang de kiem tra.");
            
//                 // Kiem tra co san pham trong gio hang
//                 var cartItems = driver.FindElements(By.CssSelector(".cart-item"));
//                 if (cartItems.Count > 0)
//                 {
//                     Console.WriteLine($"Gio hang co {cartItems.Count} san pham.");
//                 }
//                 else
//                 {
//                     // Thu tim voi selector khac
//                     cartItems = driver.FindElements(By.CssSelector(".cart-product, .cart-product-item"));
//                     Console.WriteLine($"Gio hang co {cartItems.Count} san pham.");
//                 }
                
//                 Assert.True(cartItems.Count > 0, "Gio hang khong co san pham nao.");
//             }
//             catch (WebDriverTimeoutException ex)
//             {
//                 Console.WriteLine("Khong the mo gio hang hoac khong tim thay san pham: " + ex.Message);
//                 throw;
//             }
            
//             Console.WriteLine("Test them san pham vao gio hang thanh cong!");
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("Loi xay ra: " + e.ToString());
//             throw;
//         }
//         finally
//         {
//             // Dam bao trinh duyet duoc dong ngay ca khi co loi
//             Dispose();
//         }
//     }

//     public void Dispose()
//     {
//         try
//         {
//             // Xu ly alert neu co truoc khi dong trinh duyet
//             try
//             {
//                 IAlert alert = driver.SwitchTo().Alert();
//                 alert.Accept();
//                 Console.WriteLine("Da dong alert con sot lai truoc khi thoat.");
//             }
//             catch (NoAlertPresentException)
//             {
//                 // Khong co alert, bo qua
//             }
            
//             Console.WriteLine("Ket thuc test va dong trinh duyet...");
//             driver?.Quit(); // Su dung null-conditional operator de tranh loi neu driver la null
//             driver = null;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Loi khi dong trinh duyet: " + ex.Message);
//         }
//         finally
//         {
//             // Dam bao giai phong tai nguyen
//             GC.Collect();
//             GC.WaitForPendingFinalizers();
//         }
//     }
// }