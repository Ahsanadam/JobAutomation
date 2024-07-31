using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        
        var logFile = "job_application.log";
        using (var log = new StreamWriter(logFile, true))
        {
            try
            {
                
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://arbetsformedlingen.se/logga-in");

                
                var username = driver.FindElement(By.Id("username"));
                var password = driver.FindElement(By.Id("password"));
                username.SendKeys("ditt_användarnamn");
                password.SendKeys("ditt_lösenord");
                password.SendKeys(Keys.Return);

                
                Thread.Sleep(2000);

                
                driver.Navigate().GoToUrl("https://arbetsformedlingen.se/platsbanken");

                
                var searchBox = driver.FindElement(By.Id("search-box"));
                searchBox.SendKeys("din jobbtitel");
                searchBox.SendKeys(Keys.Return);
                Thread.Sleep(2000);

                
                var jobListings = driver.FindElements(By.ClassName("job-listing"));
                foreach (var job in jobListings)
                {
                    job.Click();
                    Thread.Sleep(1000); // Vänta på att jobbsidan ska ladda
                    var applyButton = driver.FindElement(By.ClassName("apply-button"));
                    applyButton.Click();

                    var cvUpload = driver.FindElement(By.Id("cv-upload"));
                    cvUpload.SendKeys("C:\\path\\to\\your\\cv.pdf");

                    var submitButton = driver.FindElement(By.Id("submit-button"));
                    submitButton.Click();

                    log.WriteLine($"Successfully applied to job: {job.Text} at {DateTime.Now}");
                }

                
                driver.Quit();
            }
            catch (Exception ex)
            {
                log.WriteLine($"Error: {ex.Message} at {DateTime.Now}");
            }
        }
    }
}