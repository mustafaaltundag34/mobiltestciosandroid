using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowMobileTest.model
{
    public class Element
    {

        public string key { get; set; }
        public string androidValue { get; set; }
        public string androidType { get; set; }
        public string iosValue { get; set; }
        public string iosType { get; set; }

        public override string ToString()
        {
            return String.Format("key : {0}, androidValue {1}, androidType {2}, iosValue {3}, iosType {4}", key, androidValue, androidType, iosValue, iosType);
        }
    }
}
