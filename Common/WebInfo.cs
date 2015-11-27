using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public static class WeChatAppInfo
    {  
        private static string _Token = System.Configuration.ConfigurationManager.AppSettings["Token"];

        private static string _sEncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["sEncodingAESKey"];


        private static string _AppID = System.Configuration.ConfigurationManager.AppSettings["AppID"];


        private static string _Secret = System.Configuration.ConfigurationManager.AppSettings["secret"];

        public static string Token
        {
            get {
                return _Token;
            }
        }

        public static string sEncodingAESKey
        {
            get
            {
                return _sEncodingAESKey;
            }
        }
        public static string AppID
        {
            get
            {
                return _AppID;
            }
        }

        public static string Secret
        {
            get
            {
                return _Secret;
            }
        }

        public static string access_token
        {
            get
            {

                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache.Get("access_token") != null)
                {
                    return cache.Get("access_token").ToString();
                }
                else
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}"
                                          , AppID, Secret);
                    string retStr = WeChatHelper.Post(url, "");

                    ResponseMode.accesstoken token = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMode.accesstoken>(retStr);

                    if (!string.IsNullOrEmpty(token.access_token))
                        cache.Insert("access_token", token.access_token, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(token.expires_in - 600)); //缓存时间依赖

                    return token.access_token;
                }



                
            }
        }

    }

    public static class WeChatHelper
    {
        /// <summary>
        /// 将微信的整形时间转换为DateTime
        /// </summary>
        /// <param name="weChatSecond"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeFromWeChatSecond(this int weChatSecond)
        {
            return new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(weChatSecond);
        }

        public static DateTime ToDateTimeFromWeChatSecond(this string secondStr)
        {
            int weChatSecond = int.Parse(secondStr);

            return new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(weChatSecond);
        }


        /// <summary>
        /// 将DateTime类型转换为整形时间
        /// </summary>
        /// <param name="weChatSecond"></param>
        /// <returns></returns>
        public static int ToWeChatSecondFromDateTime(this DateTime dtime)
        {
            return  (int)(dtime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }


        public static string Post(string url,string bodyString)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            //写入request的body部分
            byte[] buffer2 = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(bodyString);
            request.ContentLength = buffer2.Length;
            request.GetRequestStream().Write(buffer2, 0, buffer2.Length);


            WebResponse response = request.GetResponse();
            int length = (int)response.ContentLength;
            byte[] buffer = new byte[length];
            response.GetResponseStream().Read(buffer, 0, length);

            string retStr = System.Text.Encoding.UTF8.GetString(buffer);
            return retStr;
        }
    }
}
