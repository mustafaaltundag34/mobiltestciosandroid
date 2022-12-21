using System;
using System.Threading;
using TechTalk.SpecFlow;

using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

using System.Collections.Generic;
using System.IO;

using System.Xml;
using SpecFlowMobileTest;
using System.Text;

using System.Reflection;
using System.Linq;
using SpecFlowMobileTest.Helper;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Testinium
{

    [Binding]
    public class StepImplementation
    {
        Dictionary<string, object> hashMap = new Dictionary<string, object>();
        
        public static string BASE_PATH_CONSTANTS = "D:\\Agile\\VSStudio\\TestOtomasyon\\MobilTestDefault\\SpecFlowDemo\\Constants";
        private BrowserHelper _browserHelper;
        private BasePage _basePage;
        
        public StepImplementation()
        {

        }
        public StepImplementation(BrowserHelper browserHelper)
        {
            _browserHelper = browserHelper;
            _basePage = new BasePage(_browserHelper);
        }
        
        
        [BeforeScenario]
        [Obsolete]
        public void setUp()
        {
            _browserHelper.driverAwake();
            if (TestContext.Parameters.Get("key") == null)
            {
                Console.WriteLine("Test localde ayağa kalkacak");
                BASE_PATH_CONSTANTS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../") + "Constants";
            }
            else
            {
                Console.WriteLine("Test Testiniumda ayağa kalkacak");
                BASE_PATH_CONSTANTS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../Testinium/SpecFlowDemo/") + "Constants";
     
            }


            Console.WriteLine("======================= Test Setup Before Scenario =======================");

        }


        [AfterScenario]
        public void afterScenario()
        {
            try
            {
                if (null != _browserHelper.driver)
                {
                    _browserHelper.driver.Quit();
                    Console.WriteLine("Driver Quited After Scenario");
                }
            }
            catch
            {
                Console.WriteLine("Driver önceden kapatılmış");
            }
        }



        //==================================================================== Driver ==============================================================================




        [Given(@"(.*) elementine tıkla")]
        public void clickElement(string key)
        {
            _basePage.clickBP(key);
        }

        [Given(@"(.*) elementine tıkla stale")]
        public void clickElementStale(string key)
        {
            _basePage.clickStale(key);
        }
        [Given(@"(.*),(.*) elementlerine sırayla tıkla")]
        public void clickElement(string key, string key2)
        {
            _basePage.clickDD(key, key2);
        }

        [Given(@"(.*) elementinin görünürlüğü kontrol edilir")]
        public void checkElementIsDisplayed(string key)
        {
            _basePage.checkElementIsDisplayed(key);
        }

        [Given(@"(.*) elementinin varlığı kontrol edilir")]
        public void checkElementIsNull(string key)
        {
            _basePage.checkElement(key);
        }

        [Given(@"(.*) elementine (.*) textini yaz")]
        public void fillTextBox(string key, string text)
        {
            _basePage.fillTextBox(key, text);
        }        
        
        [Given(@"ENTER yolla Android")]
        public void androidSentEnter()
        {
            //_basePage.getDriver().Keyboard.PressKey(Keys.Enter);
            var actions = new Actions(_basePage.getDriver());
            actions.SendKeys(Keys.Enter);
        }




        [Given(@"(.*) saniye bekle")]
        public void waitBySecond(int second)
        {
            Thread.Sleep(second * 1000);
        }

        [Given(@"sayfa (.*) değerini içeriyor mu")]
        public void pageContainsText(string text)
        {
            _basePage.pageContainsText(text);
        }

        [Given(@"(.*) elementi (.*) değerini içeriyor mu")]
        public void elementContainsText(string key, string text)
        {
            _basePage.elementContainsText(key, text);
        }


        [Given(@"Driver'i kapat")]
        public void driverClose()
        {
            _browserHelper.driver.Quit();
            Console.WriteLine("Driver Quited");
        }

        [Given(@"(.*) elementinin text'ini (.*) keyi ile hashmap'e ekle")]
        [Obsolete]
        public void driverGetElementText(string element, string key)
        {
            hashMap.Add(key, (_basePage.findElement(element).Text).ToString());
            Console.WriteLine("Hashmape eklenen text: " + hashMap[key]);
        }


        [Given(@"(.*) elementine hashmapteki (.*) değerini gir")]
        [Obsolete]
        public void driverSetElementText(string element, string key)
        {
            _basePage.fillTextBox(element, hashMap[key].ToString());
            Console.WriteLine("yazılan değer şu: " + hashMap[key].ToString());
        }

        [Given(@"(.*) elementine (.*) tarihini gir")]
        public void leftpress(string key, string text)
        {
            _basePage.leftPress(key, text);
            Console.WriteLine(key + "'ine " + text + " tarihi eklendi.");
        }

        [Given(@"(.*) elementini temizle")]
        public void clearElement(string key)
        {
            _basePage.clearElement(key);
            Console.WriteLine(key + "'inin text'i temizlendi");
        }


        //-------------------------------------------------------------------- Endof Driver -----------------------------------------------------------------------------------


        //==================================================================== Random Generation ==============================================================================
  
        [Given(@"Rastgele username yarat ve hashMap'a ""(.*)"" keyi ile ekle")]
        public void generateRandomUsername(string key)
        {
            Random rnd = new Random();
            int endOfNumber = rnd.Next(000001, 999999);
            hashMap.Add(key, "userr" + endOfNumber);
            Console.WriteLine(hashMap[key].ToString());
        }



        //---------------------------------------------------------------------- Endof Random Generation ----------------------------------------------------------------------




        //==================================================================== Others ==============================================================================

        [Given(@"Wait ""(.*)"" seconds")]
        public void waitSeconds(int i)
        {
            Console.WriteLine(i + " saniye bekleniyor");
            Thread.Sleep(TimeSpan.FromSeconds(i));
        }




    

        [Given(@"""(.*)"" keyli ""(.*)"" değeri hashmap'e ekle")]
        public void addHashmapManuel(string key, string value)
        {
            hashMap.Add(key, value);
            Console.WriteLine(key + " keyli " + value + " değeri manuel olarak hashmap'e eklendi");
        }


    }

}


