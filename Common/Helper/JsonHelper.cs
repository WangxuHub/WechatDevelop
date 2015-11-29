using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class mJsonResult
    {
        public int currentIndex;
        public int pageSize;
        public int total;
        public string msg;
        public bool success;
        public object obj;
        public object rows;

      
    }

    public static class JsonHelper
    {
        public static string ToJson(this mJsonResult mJsonResult)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(mJsonResult);
        }
    }
}
