using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using SpecFlowMobileTest.model;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using SpecFlowDemo.Configuration;

namespace SpecFlowMobileTest.Helper
{
    [Binding]
    public class BrowserHelper
    {

        public AppiumDriver<AppiumWebElement> driver = null;
        public Boolean localAndroid = true;
        public Dictionary<string, ElementModel> keyValuePairs;
        private string BASE_EXT = "*.json";


        public void driverAwake()
        {
            var serverUri = TestContext.Parameters.Get("key") == null ? "http://127.0.0.1:4723/wd/hub" : "http://hub.testinium.io/wd/hub";
            if (TestContext.Parameters.Get("key") == null)
            {
                if (localAndroid)
                {
                    Console.WriteLine("Local cihazda Android ortamında test ayağa kalkacak");
                    driver = new AndroidDriver<AppiumWebElement>(new Uri(serverUri), androidCapabilities(true));
                }
                else
                {
                    Console.WriteLine("Local cihazda IOS ortamında test ayağa kalkacak");
                    driver = new IOSDriver<AppiumWebElement>(new Uri(serverUri), iosCapabilities(true));
                }
            }
            else
            {
                if (TestContext.Parameters.Get("platform") == "ANDROID")
                {
                    Console.WriteLine("Testinium Android ortamında test ayağa kalkacak");
                    driver = new AndroidDriver<AppiumWebElement>(new Uri(serverUri), androidCapabilities(false));
                    localAndroid = true;
                }
                else
                {
                    Console.WriteLine("Testinium IOS ortamında test ayağa kalkacak");
                    driver = new IOSDriver<AppiumWebElement>(new Uri(serverUri), iosCapabilities(false));
                    localAndroid = false;
                }
            }
            keyValuePairs = Degerver();
        }

        public AppiumOptions androidCapabilities(Boolean isLocal)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, false);
            capabilities.AddAdditionalCapability("unicodeKeyboard", false);
            capabilities.AddAdditionalCapability("resetKeyboard", false);
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.android.chrome");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "org.chromium.chrome.browser.ChromeTabbedActivity");
            if (isLocal)
            {
                capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.Android);
                capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "android");
                capabilities.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 300);
            }
            else
            {
                capabilities.AddAdditionalCapability("key", TestContext.Parameters.Get("key"));
            }
            return capabilities;
        }

        public AppiumOptions iosCapabilities(Boolean islocal)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, false);
            capabilities.AddAdditionalCapability("bundleId", "com.ozdilek.ozdilekteyim");
            if (!islocal)
            {
                capabilities.AddAdditionalCapability("key", TestContext.Parameters.Get("key"));
                capabilities.AddAdditionalCapability("waitForAppScript", "$.delay(1000);");
                capabilities.AddAdditionalCapability("usePrebuiltWDA", true);
                capabilities.AddAdditionalCapability("useNewWDA", true);
            }
            else
            {
                capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.IOS);
                capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
                capabilities.AddAdditionalCapability(MobileCapabilityType.Udid, "1e5cdbbadc4a7dc3e4389298330bad5c587904d5");
                capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone SE");
                capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "12.5");
                capabilities.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 300);
                capabilities.AddAdditionalCapability("sendKeyStrategy", "setValue");
            }

            return capabilities;

        }


        public Dictionary<string, ElementModel> Degerver() // kimseye değerinden fazla değer vermeyin
        {
            Dictionary<string, ElementModel> dic = new Dictionary<string, ElementModel>();
            var txtFiles = Directory.EnumerateFiles(Testinium.StepImplementation.BASE_PATH_CONSTANTS, BASE_EXT);
            foreach (string currentFile in txtFiles)
            {
                var json = File.ReadAllText(currentFile);
                var jzort = JsonConvert.DeserializeObject<IEnumerable<ElementModel>>(json);
                Dictionary<string, Element> d = JsonConvert.DeserializeObject<IEnumerable<Element>>(json).
                Select(p => (Id: p.key, Record: p)).
                ToDictionary(t => t.Id, t => t.Record);
                //Console.WriteLine("Okunan dosya: " + currentFile + " element sayısı: " + d.Count);
                foreach (var item in d)
                {
                    dic.Add(item.Key.ToString(), new ElementModel(item.Key.ToString(),item.Value.androidType.ToString(), item.Value.androidValue.ToString(), item.Value.iosType.ToString(), item.Value.iosValue.ToString()));
                    
                }

            }

            Console.WriteLine("Sözlükteki toplam element sayısı:" + dic.Count);
            return dic;
        }


    }
}
