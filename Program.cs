using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://arbetsformedlingen.se/platsbanken");

        var searchBox = driver.FindElement(By.Id("search-box"));
        searchBox.SendKeys("Systemutvecklare");
        searchBox.SendKeys(Keys.Return);
        Thread.Sleep(2000);

        var ortFilter = driver.FindElement(By.XPath("//button[contains(text(), 'Ort')]"));
        ortFilter.Click();
        Thread.Sleep(1000);

        var vastGotalandsLan = driver.FindElement(By.XPath("//a[contains(text(), 'Västra Götalands län')]"));
        vastGotalandsLan.Click();
        Thread.Sleep(1000);

        var goteborgOption = driver.FindElement(By.XPath("//label[contains(text(), 'Göteborg')]"));
        goteborgOption.Click();
        Thread.Sleep(2000);

        var applyFilterButton = driver.FindElement(By.XPath("//button[contains(text(), 'Visa')]"));
        applyFilterButton.Click();
        Thread.Sleep(2000);

        var jobListings = driver.FindElements(By.ClassName("job-listing"));
        foreach (var job in jobListings)
        {
            Console.WriteLine(job.Text);
        }

        driver.Quit();
    }
}