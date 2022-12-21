using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using SpecFlowMobileTest.Configuration;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;

namespace SpecFlowMobileTest.Helper
{
    public class BasePage
    {

        private BrowserHelper _browserHelper;
        private IJavaScriptExecutor javaScriptExecutor;
        public BasePage(BrowserHelper browserHelper)
        {
            _browserHelper = browserHelper;

        }

        public AppiumDriver<AppiumWebElement> getDriver()
        {
            return _browserHelper.driver;
        }
        public void clickBP(string key)
        {
            Thread.Sleep(1000);
            findElement(key).Click();
        }
        public void clickStale(string key)
        {
            Thread.Sleep(1000);
            findElementStale(key).Click();
        }
        public void clickDD(string key, string key2)
        {
            findElement(key).Click();
            findElement(key2).Click();
        }

        public void fillTextBox(string key, string text)
        {
            findElement(key).SendKeys(text);
        }

        public void checkElementIsDisplayed(string key)
        {
            Assert.IsTrue(findElement(key).Displayed, key + "'li elementi bulunamadı.");
        }
        public void checkElement(string key)
        {
            Assert.AreNotEqual(findElement(key), null + "'li elementi bulunamadı.");
        }

        public void browserMaximize()
        {
            _browserHelper.driver.Manage().Window.Maximize();
        }
        public void goBack()
        {
            _browserHelper.driver.Navigate().Back();
        }

        public void forward()
        {
            _browserHelper.driver.Navigate().Forward();
        }
        public void leftPress(string key, string text)
        {
            findElement(key).SendKeys(Keys.Home);
            fillTextBox(key, text);

        }

        public void refreshPage()
        {
            _browserHelper.driver.Navigate().Refresh();
        }
        public void switchToLastWindow()

        {
            _browserHelper.driver.SwitchTo().Window(_browserHelper.driver.WindowHandles.Last());
        }

        public void pageContainsText(string text)

        {
            Assert.IsTrue(_browserHelper.driver.PageSource.Contains(text), "Sayfa " + text + " değerini içermiyor.");
        }

        public void elementContainsText(string key, string text)

        {
            Assert.AreEqual(findElement(key).Text.ToString(), text, "iki alan eşleşmiyor..");
        }


        public By generateElementBy(string by, string value)
        {
            Console.WriteLine("generateElementBy by: " + by + " value: " + value);
            By byElement = null;
            if (_browserHelper.localAndroid)
            {
                if (by.Equals(AndroidElementType.id))
                {
                    byElement = MobileBy.Id(value);
                }
                else if (by.Equals(AndroidElementType.css))
                {
                    byElement = MobileBy.CssSelector(value);
                }
                else if (by.Equals(AndroidElementType.xpath))
                {
                    byElement = MobileBy.XPath(value);
                }
                else if (by.Equals(AndroidElementType.linkText))
                {
                    byElement = MobileBy.LinkText(value);
                }
                else if (by.Equals(AndroidElementType.className))
                {
                    byElement = MobileBy.ClassName(value);
                }
                else
                {
                    Assert.Fail("No such selector.");
                }
            }
            else
            {
                if (by.Equals(IosElementType.id))
                {
                    byElement = MobileBy.Id(value);
                }
                else if (by.Equals(IosElementType.css))
                {
                    byElement = MobileBy.CssSelector(value);
                }
                else if (by.Equals(IosElementType.xpath))
                {
                    byElement = MobileBy.XPath(value);
                }
                else if (by.Equals(IosElementType.classChain))
                {
                    byElement = MobileBy.IosClassChain(value);
                }
                else if (by.Equals(IosElementType.name))
                {
                    byElement = MobileBy.Name(value);
                }
                else if (by.Equals(IosElementType.className))
                {
                    byElement = MobileBy.ClassName(value);
                }
                else
                {
                    Assert.Fail("No such selector.");
                }
            }
            return byElement;
        }

        public AppiumWebElement findElement(string key)
        {
            By by;
            by = generateElementBy(_browserHelper.localAndroid ? _browserHelper.keyValuePairs[key].getAndroidType() : _browserHelper.keyValuePairs[key].getIosType(), _browserHelper.localAndroid ? _browserHelper.keyValuePairs[key].getAndroidValue() : _browserHelper.keyValuePairs[key].getIosValue());
            WebDriverWait wait = new WebDriverWait(_browserHelper.driver, TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout()));
            //AppiumWebElement webElement = (AppiumWebElement)wait.Until(ExpectedConditions.ElementToBeClickable(by));
            AppiumWebElement webElement = (AppiumWebElement)wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            return webElement;
        }
        public IList<AppiumWebElement> findElements(string key)
        {
            By by;
            by = generateElementBy(_browserHelper.localAndroid ? _browserHelper.keyValuePairs[key].getAndroidType() : _browserHelper.keyValuePairs[key].getIosType(), _browserHelper.localAndroid ? _browserHelper.keyValuePairs[key].getAndroidValue() : _browserHelper.keyValuePairs[key].getIosValue());
            WebDriverWait wait = new WebDriverWait(_browserHelper.driver, TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout()));
            //IList<IWebElement> webElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            IList<IWebElement> webElements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            return (IList<AppiumWebElement>)webElements;
        }

        public IWebElement findElementStale(string key)
        {
            By by;
            by = generateElementBy(_browserHelper.localAndroid ? _browserHelper.keyValuePairs[key].getAndroidType() : _browserHelper.keyValuePairs[key].getIosType(), _browserHelper.localAndroid ? _browserHelper.keyValuePairs[key].getAndroidValue() : _browserHelper.keyValuePairs[key].getIosValue());
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(_browserHelper.driver);
            wait.Timeout = TimeSpan.FromSeconds(AppConficReader.GetElementLoadTimeout());
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Message = "StaleElementReferenceException";
            //IWebElement webElement = wait.Until(ExpectedConditions.ElementToBeClickable(by));
            IWebElement webElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            return webElement;
        }


        public void scrollElement(IWebElement element)
        {
            javaScriptExecutor = (IJavaScriptExecutor)_browserHelper.driver;
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView({ behaviour: 'smooth', block: 'center', inline: 'center'});", element);
        } 

        public void clearElement(string key)
        {
            findElement(key).Clear();
        }


    }
}
