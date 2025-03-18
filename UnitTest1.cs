using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace BRITTestProject
{
    public class Tests : IDisposable
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500));
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ValidateTheSearchText()
        {
            driver.Navigate().GoToUrl("https://www.britinsurance.com/ ");
            driver.FindElement(By.XPath("//button[text()='Allow all cookies']")).Click();
            driver.FindElement(By.XPath("//button[@aria-label='Search button']")).Click();
            driver.FindElement(By.XPath("//input[@name='k']")).SendKeys("IFRS 17");
            ReadOnlyCollection<IWebElement> searchresults = driver.FindElements(By.XPath("//div[@class='header--search__results']/div"));
            int expectedCount = 5;
            Assert.AreEqual(expectedCount, searchresults.Count, $"Expected {expectedCount} results but found {searchresults.Count}.");
            string expectedresult = "Interim results for the six months ended 30 June 2022";
            foreach (IWebElement result in searchresults)
            {
                string linktext = result.Text;
                if(linktext== expectedresult)
                {
                    Assert.AreEqual(linktext, expectedresult);
                }
                
            }
            
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (driver != null)
            {
                driver.Quit();  
                driver = null;  
            }
        }
    }
}