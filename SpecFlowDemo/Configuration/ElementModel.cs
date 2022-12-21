using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowDemo.Configuration
{
    public class ElementModel 
    {
        
        public string Key { get; set; }
        private string androidType;
        private string androidValue;
        private string iosType;
        private string iosValue;

        public void setAndroidType(string andType)
        {
            androidType = andType;
        }

        public string getAndroidType()
        {
            return androidType;
        }
        public void setAndroidValue(string andValue)
        {
            androidValue = andValue;
        }

        public string getAndroidValue()
        {
            return androidValue;
        }        
        
        public void setIosType(string IosType)
        {
            iosType = IosType;
        }

        public string getIosType()
        {
            return iosType;
        }        
        
        public void setIosValue(string IosVal)
        {
            iosValue = IosVal;
        }

        public string getIosValue()
        {
            return iosValue;
        }

        public ElementModel(string Key,string androidType, string androidValue, string iosType, string iosValue)
        {
            this.Key = Key;
            setAndroidType(androidType);
            setAndroidValue(androidValue);
            setIosType(iosType);
            setIosValue(iosValue);
        }
        
    }
   
}
