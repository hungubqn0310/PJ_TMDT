using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EcommerceTests
{
    public class AddProductTest
    {
        private IWebDriver? driver;
        private WebDriverWait? wait;

        [SetUp] // Chạy trước mỗi test case
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); // Tăng timeout lên 30 giây
        }

        // [Test]
        // public void Test_AddProduct_Success()
        // {
        //     driver.Navigate().GoToUrl("https://localhost:5003");

        //     // Chờ user-icon xuất hiện và mở popup đăng nhập
        //     IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
        //     userIcon.Click();

        //     // Chờ popup đăng nhập hiển thị
        //     wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

        //     // Nhập thông tin đăng nhập
        //     driver.FindElement(By.Id("email")).SendKeys("admin");
        //     driver.FindElement(By.Id("password")).SendKeys("123456");

        //     // Nhấn nút đăng nhập
        //     IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
        //     loginButton.Click();

        //     // Chờ chuyển hướng sau đăng nhập
        //     wait.Until(ExpectedConditions.UrlContains("/admin"));

        //     // Truy cập trực tiếp vào trang sản phẩm
        //     driver.Navigate().GoToUrl("https://localhost:5003/admin/product");

        //     // Nhấp vào nút Thêm mới
        //     wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Thêm mới')]"))).Click();

        //     // Điền thông tin sản phẩm
        //     var productIdField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ProductId")));
        //     var productNameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ProductName")));
        //     var descriptionField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Description")));
        //     var priceField = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("Price")));

        //     // Nhập giá trị cho các trường
        //     productIdField.SendKeys("HU310");
        //     productNameField.SendKeys("Nhẫn xịn");
        //     descriptionField.SendKeys("Nhẫn cưới xịn");
        //     priceField.SendKeys("250000");

        //     // Chọn Category và Material
        //     new SelectElement(driver.FindElement(By.Name("CategoryId"))).SelectByText("Nhẫn cầu hôn");
        //     new SelectElement(driver.FindElement(By.Name("MaterialId"))).SelectByText("Gold");

        //     // Nhập đường dẫn hình ảnh
        //     IWebElement imageUrlField = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("ImageUrl")));
        //     imageUrlField.SendKeys(@"C:\Users\TGDD\Downloads\anh4x6.jpg"); // Đảm bảo rằng đường dẫn là chính xác và tệp tồn tại

        //     // Nhấn nút Lưu
        //     driver.FindElement(By.XPath("//button[contains(text(), 'Lưu')]")).Click();

        //     // Chờ cho trang cập nhật và phần tử chứa danh sách sản phẩm xuất hiện
        //     wait.Until(ExpectedConditions.ElementIsVisible(By.Id("dataTable"))); // Thay đổi ID cho đúng với cấu trúc của trang của bạn

        //     // Kiểm tra sản phẩm đã được thêm thành công
        //     var productNameInTable = driver.FindElement(By.XPath("//table[@id='dataTable']//td[contains(text(), 'Nhẫn xịn')]"));
        //     Assert.That(productNameInTable, Is.Not.Null, "Sản phẩm không được thêm thành công!");
        // }
        [Test]
public void Test_AddProduct_InvalidInput()
{
    driver.Navigate().GoToUrl("https://localhost:5003");

    // Mở popup đăng nhập
    IWebElement userIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("user-icon")));
    userIcon.Click();
    wait.Until(ExpectedConditions.ElementIsVisible(By.Id("popupLogin")));

    // Đăng nhập
    driver.FindElement(By.Id("email")).SendKeys("admin");
    driver.FindElement(By.Id("password")).SendKeys("123456");
    IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginButton")));
    loginButton.Click();
    wait.Until(ExpectedConditions.UrlContains("/admin"));

    // Truy cập trang sản phẩm
    driver.Navigate().GoToUrl("https://localhost:5003/admin/product");

    // Nhấn vào "Thêm mới"
    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Thêm mới')]"))).Click();

    // Chờ form hiển thị hoàn toàn
    wait.Until(ExpectedConditions.ElementIsVisible(By.Name("ProductId")));

    // Không nhập gì và nhấn Lưu
    var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Lưu')]")));
    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", saveButton);
    saveButton.Click();
    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Thêm mới')]"))).Click();
    // Chờ thông báo lỗi
    var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("alert-danger"))).Text;
    Assert.That(errorMessage, Is.Not.Empty, "Không thấy thông báo lỗi khi nhập thiếu thông tin!");

    // Kiểm tra danh sách sản phẩm không có sản phẩm mới
    var productNameInTable = driver.FindElements(By.XPath("//table[@id='dataTable']//td[contains(text(), 'Nhẫn xịn')]"));
    Assert.That(productNameInTable.Count, Is.EqualTo(0), "Sản phẩm không được thêm nhưng vẫn xuất hiện trong danh sách!");
}

        [TearDown] // Chạy sau mỗi test case
        public void TearDown()
        {
            driver.Quit();
        }
    }
}