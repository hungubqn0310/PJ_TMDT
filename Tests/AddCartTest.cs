using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using Xunit;
using System.Threading;

public class SeleniumAddToCartFromCategoryTest : IDisposable
{
    private IWebDriver driver;
    private WebDriverWait wait;
    private Actions actions;

    public SeleniumAddToCartFromCategoryTest()
    {
        ChromeOptions options = new ChromeOptions();
        options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        actions = new Actions(driver);
    }

    [Fact]
public void TestAddToCartFromCategory()
{
    try
    {
        Console.WriteLine("Bắt đầu test đăng nhập và thêm sản phẩm vào giỏ hàng...");

        // Truy cập trang web
        driver.Navigate().GoToUrl("https://localhost:5003");
        Console.WriteLine("Đã truy cập trang chính.");

        // Mở popup đăng nhập
        IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
        userIcon.Click();
        Console.WriteLine("Đã mở popup đăng nhập.");

        // Nhập thông tin đăng nhập
        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));
        driver.FindElement(By.Id("email")).SendKeys("user1");
        driver.FindElement(By.Id("password")).SendKeys("123456");
        Console.WriteLine("Đã nhập thông tin đăng nhập.");

        // Nhấn nút đăng nhập
        IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
        loginButton.Click();
        Console.WriteLine("Đã nhấn nút đăng nhập.");

        // Chờ username xuất hiện để xác nhận đăng nhập thành công
        wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
        Console.WriteLine("Đăng nhập thành công.");

        // Trỏ chuột vào "Danh mục" để hiển thị dropdown
        IWebElement categoryMenu = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath("//a[contains(text(),'Danh mục')]")));
        
        actions.MoveToElement(categoryMenu).Perform();
        Console.WriteLine("Đã trỏ chuột vào Danh mục.");
        
        // Đợi dropdown hiển thị
        Thread.Sleep(1000); // Đợi animation hoàn tất
        
        // Click vào "Nhẫn cầu hôn" từ dropdown
        IWebElement engagementRingLink = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath("//a[contains(text(),'Nhẫn cầu hôn')]")));
        engagementRingLink.Click();
        Console.WriteLine("Đã click vào Nhẫn cầu hôn từ dropdown menu.");

        // Chờ trang danh sách sản phẩm hiển thị
        wait.Until(ExpectedConditions.UrlContains("nhanCauHon"));
        Console.WriteLine("Đã chuyển đến trang danh sách sản phẩm Nhẫn cầu hôn.");

        // Tìm sản phẩm đầu tiên trong danh sách
        IWebElement firstProduct = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(".product_box_content:first-child .product_name")));

        // Cuộn đến sản phẩm để đảm bảo nó hiển thị
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", firstProduct);
        Thread.Sleep(500); // Đợi một chút để phần tử hiển thị đầy đủ

        // Click vào sản phẩm để xem chi tiết
        firstProduct.Click();
        Console.WriteLine("Đã click vào sản phẩm đầu tiên để xem chi tiết.");

        // Chờ trang chi tiết sản phẩm hiển thị
        wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("product-detail-wrapper")));
        Console.WriteLine("Đã chuyển đến trang chi tiết sản phẩm.");

        // Tìm nút "Thêm vào giỏ hàng" trong trang chi tiết sản phẩm
        IWebElement addToCartButton = wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector(".btn-cart"))); // Sử dụng class hoặc id của nút
        Console.WriteLine("Đã tìm thấy nút Thêm vào giỏ hàng.");

        // Cuộn đến nút để đảm bảo nó hiển thị
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", addToCartButton);
        Thread.Sleep(500); // Đợi một chút để phần tử hiển thị đầy đủ

        // Click vào nút "Thêm vào giỏ hàng"
        addToCartButton.Click();
        Console.WriteLine("Đã nhấn nút Thêm vào giỏ hàng.");

        // Xử lý thông báo (nếu có)
        try
        {
            var alert = wait.Until(ExpectedConditions.AlertIsPresent());
            Console.WriteLine("Thông báo: " + alert.Text);
            alert.Accept(); // Confirm thông báo
            Console.WriteLine("Đã xác nhận thông báo.");
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("Không có thông báo alert hiện lên.");
        }

        // Kiểm tra giỏ hàng - sử dụng icon giỏ hàng chính
        try
        {
            // Tìm icon giỏ hàng chính (ở header hoặc navigation)
            IWebElement mainCartIcon = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.CssSelector(".shopping-cart-icon, .fa-shopping-cart, .cart-icon")));
            mainCartIcon.Click();
            Console.WriteLine("Đã mở giỏ hàng để kiểm tra.");
        
          
        
            // Kiểm tra có sản phẩm trong giỏ hàng
            var cartItems = driver.FindElements(By.CssSelector(".cart-item"));
            if (cartItems.Count > 0)
            {
                Console.WriteLine($"Giỏ hàng có {cartItems.Count} sản phẩm.");
            }
            else
            {
                // Thử tìm với selector khác
                cartItems = driver.FindElements(By.CssSelector(".cart-product, .cart-product-item"));
                Console.WriteLine($"Giỏ hàng có {cartItems.Count} sản phẩm.");
            }
            
            Assert.True(cartItems.Count > 0, "Giỏ hàng không có sản phẩm nào.");
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine("Không thể mở giỏ hàng hoặc không tìm thấy sản phẩm: " + ex.Message);
            throw;
        }
        
        Console.WriteLine("Test thêm sản phẩm vào giỏ hàng thành công!");
    }
    catch (Exception e)
    {
        Console.WriteLine("Lỗi xảy ra: " + e.ToString());
        throw;
    }
    finally
    {
        // Đảm bảo trình duyệt được đóng ngay cả khi có lỗi
        Dispose();
    }
}

public void Dispose()
{
    try
    {
        // Xử lý alert nếu có trước khi đóng trình duyệt
        try
        {
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Console.WriteLine("Đã đóng alert còn sót lại trước khi thoát.");
        }
        catch (NoAlertPresentException)
        {
            // Không có alert, bỏ qua
        }
        
        Console.WriteLine("Kết thúc test và đóng trình duyệt...");
        driver?.Quit(); // Sử dụng null-conditional operator để tránh lỗi nếu driver là null
        driver = null;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Lỗi khi đóng trình duyệt: " + ex.Message);
    }
    finally
    {
        // Đảm bảo giải phóng tài nguyên
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
}