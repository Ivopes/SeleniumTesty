using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace TestProject1
{
    public class Tests
    {
        const string TEST_URL = "https://garmusic.azurewebsites.net/";
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
        }
        [Test, Order(2)]
        [TestCase("admin", "admin")]
        public void Login_ShouldLogIn(string username, string password)
        {
            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(username);
            inputs[1].SendKeys(password);
            var button = driver.FindElement(By.CssSelector("button"));
            button.Click();

            Thread.Sleep(1000);

            const string expected = "https://garmusic.azurewebsites.net/playlists";

            Assert.AreEqual(expected, driver.Url);
        }
        [Test, Order(7)]
        [TestCase("i")]
        [TestCase("noveHeslo")]
        [TestCase("qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm")]
        public void ChangePassword_ShouldChange(string newPassword)
        {
            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys("admin");
            inputs[1].SendKeys("admin");
            var button = driver.FindElement(By.CssSelector("button"));
            button.Click();

            Thread.Sleep(1000);

            var accountTab = driver.FindElement(By.XPath("//div[text()='Account']"));
            accountTab.Click();
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.XPath("//span[text()='Change password']")));

            var changeButton = driver.FindElement(By.XPath("//span[text()='Change password']"));
            changeButton.Click();

            var passInputs = driver.FindElements(By.XPath("//mat-dialog-container//input"));
            passInputs[0].SendKeys("admin");
            passInputs[1].SendKeys(newPassword);
            passInputs[2].SendKeys(newPassword);
            Thread.Sleep(250);

            var confirmButton = driver.FindElement(By.XPath("//*[text()='Change']"));
            confirmButton.Click();

            var logout = driver.FindElement(By.XPath("//mat-icon[text()='power_settings_new']"));
            logout.Click();

            Thread.Sleep(500);

            inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys("admin");
            inputs[1].SendKeys(newPassword);
            button = driver.FindElement(By.CssSelector("button"));
            button.Click();

            Thread.Sleep(1000);
            
            accountTab = driver.FindElement(By.XPath("//div[text()='Account']"));
            accountTab.Click();

            wait.Until(driver => driver.FindElement(By.XPath("//span[text()='Change password']")));

            changeButton = driver.FindElement(By.XPath("//span[text()='Change password']"));
            changeButton.Click();

            passInputs = driver.FindElements(By.XPath("//mat-dialog-container//input"));
            passInputs[0].SendKeys(newPassword);
            passInputs[1].SendKeys("admin");
            passInputs[2].SendKeys("admin");
            Thread.Sleep(250);

            confirmButton = driver.FindElement(By.XPath("//*[text()='Change']"));
            confirmButton.Click();

            logout = driver.FindElement(By.XPath("//mat-icon[text()='power_settings_new']"));
            logout.Click();

            Thread.Sleep(250);

            inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys("admin");
            inputs[1].SendKeys("admin");
            button = driver.FindElement(By.CssSelector("button"));
            button.Click();

            Thread.Sleep(1000);

            const string expected = "https://garmusic.azurewebsites.net/playlists";
            Assert.AreEqual(expected, driver.Url);
        }
        [Test, Order(8)]
        [TestCase("", "", "")]
        [TestCase("wrongPass", "", "")]
        [TestCase("", "a", "a")]
        [TestCase("",
            "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm",
            "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm")]
        [TestCase("", "Spatne", "Horsi")]
        public void ChangePassword_ShouldNotChange(string oldPassword, string newPassword, string retypePassword)
        {
            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys("admin");
            inputs[1].SendKeys("admin");
            var button = driver.FindElement(By.CssSelector("button"));
            button.Click();

            Thread.Sleep(1000);

            var accountTab = driver.FindElement(By.XPath("//div[text()='Account']"));
            accountTab.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.XPath("//span[text()='Change password']")));

            var changeButton = driver.FindElement(By.XPath("//span[text()='Change password']"));
            changeButton.Click();

            var passInputs = driver.FindElements(By.XPath("//mat-dialog-container//input"));
            passInputs[0].SendKeys(oldPassword);
            passInputs[1].SendKeys(newPassword);
            passInputs[2].SendKeys(retypePassword);
            Thread.Sleep(250);

            var confirmButton = driver.FindElement(By.XPath("//*[text()='Change']"));
            confirmButton.Click();

            var container = driver.FindElement(By.CssSelector("mat-dialog-container"));
            Assert.NotNull(container);
        }
        [Test, Order(1)]
        [TestCase("", "")]
        [TestCase("s", "")]
        [TestCase("", "q")]
        [TestCase(
            "dwadawdadacsascacwqdqwqoiguinbiugbaiugbaeuigbaegbuargbaeurgbapeirbuvabrvbaerbvaerbnae[b",
            "qifupibPUIBuebfuiwe[]qfgrwefwfvwev59wev+5we9vweveaiuabiunusvusineviuawnqwfnpawuinbaipvba")]
        [TestCase(
            "",
            "qifupibPUIBuebfuiwe[]qfgrwefwfvwev59wev+5we9vweveaiuabiunusvusineviuawnqwfnpawuinbaipvba")]
        [TestCase(
            "dwadawdadacsascacwqdqwqoiguinbiugbaiugbaeuigbaegbuargbaeurgbapeirbuvabrvbaerbvaerbnae[b",
            "")]
        [TestCase("badUser", "Password")]
        public void Login_ShouldNotLogIn(string username, string password)
        {
            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(username);
            inputs[1].SendKeys(password);
            var button = driver.FindElement(By.CssSelector("button"));
            button.Click();

            Thread.Sleep(1000);

            const string expected = "https://garmusic.azurewebsites.net/login";

            Assert.AreEqual(expected, driver.Url);
        }
        [Test, Order(3)]
        [TestCase("a")]
        [TestCase("novyPlaylist")]
        [TestCase("novyPlaylistasdadaqwdanbuisbaeibviuabvaiurvbieurvbaiuerbvaieurbvaiuerbvaervbu")]
        public void CreatePlaylist_ShouldCreate(string name)
        {
            const string username = "admin";
            const string password = "admin";

            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(username);
            inputs[1].SendKeys(password);
            var loginButton = driver.FindElement(By.CssSelector("button"));
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.CssSelector("mat-card")));

            Thread.Sleep(250);

            var addButton = driver.FindElement(By.CssSelector("mat-card"));
            addButton.Click();

            wait.Until(driver => driver.FindElement(By.XPath("//*[@formcontrolname='name']")));

            var plInput = driver.FindElement(By.XPath("//*[@formcontrolname='name']"));
            plInput.SendKeys(name);

            var createButton = driver.FindElement(By.XPath("//*[@type='submit']"));
            createButton.Click();


            wait.Until(driver => driver.FindElement(By.XPath($"//mat-card-title[text()=' {name} ']")));
            var created = driver.FindElement(By.XPath($"//mat-card-title[text()=' {name} ']"));
            Assert.Multiple(() =>
            {
                Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.CssSelector("mat-dialog-container")));
                Assert.NotNull(created);
            });
        }
        [Test, Order(4)]
        [TestCase("")]
        public void CreatePlaylist_CantCreate(string name)
        {
            const string username = "admin";
            const string password = "admin";

            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(username);
            inputs[1].SendKeys(password);
            var loginButton = driver.FindElement(By.CssSelector("button"));
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.CssSelector("mat-card")));

            Thread.Sleep(100);

            var addButton = driver.FindElement(By.CssSelector("mat-card"));
            addButton.Click();

            var plInput = driver.FindElement(By.XPath("//*[@formcontrolname='name']"));
            plInput.SendKeys(name);

            var createButton = driver.FindElement(By.XPath("//*[@type='submit']"));
            createButton.Click();

            var container = driver.FindElement(By.CssSelector("mat-dialog-container"));
            Assert.NotNull(container);
        }
        [Test, Order(6)]
        [TestCase("a")]
        [TestCase("novyPlaylist")]
        [TestCase("novyPlaylistasdadaqwdanbuisbaeibviuabvaiurvbieurvbaiuerbvaieurbvaiuerbvaervbu")]
        public void DeletePlaylist_ShouldDelete(string name)
        {
            const string username = "admin";
            const string password = "admin";

            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(username);
            inputs[1].SendKeys(password);
            var loginButton = driver.FindElement(By.CssSelector("button"));
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.CssSelector("mat-card")));

            Thread.Sleep(500);

            wait.Until(driver => driver.FindElement(By.XPath($"//mat-card-title[text()=' {name} ']")));
            var toDelete = driver.FindElement(By.XPath($"//mat-card-title[text()=' {name} ']/../mat-card-actions/button"));
            toDelete.Click();

            wait.Until(toDelete => toDelete.FindElement(By.XPath("//mat-dialog-container/app-delete-dialog/div/button")));
            Thread.Sleep(500);

            var deleteButtonDialog = toDelete.FindElement(By.XPath("//mat-dialog-container/app-delete-dialog/div/button"));
            deleteButtonDialog.Click();

            wait.Until(driver => driver.FindElement(By.CssSelector("simple-snack-bar")));
            //Thread.Sleep(2500);
        }
        [Test, Order(5)]
        [TestCase("")]
        [TestCase("NonExist")]
        [TestCase("NonExistaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaasdasda")]
        public void DeletePlaylist_DoesNotExist(string name)
        {
            const string username = "admin";
            const string password = "admin";

            driver.Url = TEST_URL;
            var inputs = driver.FindElements(By.CssSelector("input"));
            inputs[0].SendKeys(username);
            inputs[1].SendKeys(password);
            var loginButton = driver.FindElement(By.CssSelector("button"));
            loginButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.CssSelector("mat-card")));

            Thread.Sleep(500);

            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.XPath($"//mat-card-title[text()=' {name} ']")));
        }
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}