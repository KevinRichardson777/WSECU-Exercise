using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using NUnit.Framework;

namespace WSECU_Login_Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            int milliseconds = 2000;

            IWebDriver driver = new ChromeDriver(); // Start Google Chrome browser

            driver.Navigate().GoToUrl("Https://www.wsecu.org"); // Navigate to the WSECU website

            driver.Manage().Window.Maximize(); // Maximize the window

            driver.FindElement(By.Id("digital-banking-username")).Click();
            driver.FindElement(By.Id("digital-banking-username")).SendKeys("Banke"); // Enter a username
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click(); // Click on the Sign In button
            Console.WriteLine(driver.Title); // Present the page title to the console
            String pageTitle = driver.Title;

            String expectedTitle = "Sign in to Online Banking"; // The title of the Online Banking window

            // Verify the Online Banking page is displayed
            try
            {
                Assert.IsTrue(pageTitle == expectedTitle);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            Console.WriteLine("Appropriate Online Banking Sign In page was found.");

            driver.FindElement(By.XPath("/html/body")).Click();

            Thread.Sleep(milliseconds);

            driver.FindElement(By.XPath("//*[@data-role='username-input']")).Clear(); // Clear the username. Not necessary

            driver.FindElement(By.XPath("//*[@data-role='username-input']")).SendKeys("NewMember"); // Add a new username. Also not necessary

            // Use "admin" as the "NewMember" user password
            try
            {
                driver.FindElement(By.XPath("//input//following::input")).SendKeys("admin");
            }
            catch (Exception e)
            {
                Console.Write(e);
            }

            driver.FindElement(By.XPath("//button[@type='submit']")).Click(); // submit the username and password
            Thread.Sleep(milliseconds);
            
            String actual_error = driver.FindElement(By.XPath("//div[text()='Sorry, incorrect username.']")).Text;
            Console.WriteLine(actual_error);

            String expected_error = "Sorry, incorrect username.";

            try
            {
                Assert.IsTrue(actual_error == expected_error);
            }
            catch (Exception e)
            {
                Console.Write(e);
                Console.WriteLine("");
            }
            Console.WriteLine("Appropriate incorrect username error message was found.");

            driver.Quit();
        }
    }
}
