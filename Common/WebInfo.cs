using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Text.RegularExpressions;
namespace Common
{
    public static class WeChatAppInfo
    {  
        public static string Token
        {
            get {
                return System.Configuration.ConfigurationManager.AppSettings["Token"]; 
            }
        }

        public static string sEncodingAESKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sEncodingAESKey"];
            }
        }
        public static string AppID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppID"];
            }
        }

        public static string Secret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["secret"];
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

        #region 微信JS-SDK 配置
        /// <summary>
        /// js-SDK 中使用的ticket
        /// </summary>
        public static string ticket
        {
            get
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache.Get("ticket") != null)
                {
                    return cache.Get("ticket").ToString();
                }
                else
                {
                    string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi"
                                          , access_token);
                    string retStr = WeChatHelper.Post(url, "");

                    //{"errcode":0,"errmsg":"ok","ticket":"sM4AOVdWfPE4DxkXGEs8VBdB9iUewc7YSCGbhjD5w4sjYk-gt-BENHWmjZmazCZ6fUH254TWysS1sjiYoPDflA","expires_in":7200}
                    Newtonsoft.Json.Linq.JObject retObj = Newtonsoft.Json.JsonConvert.DeserializeObject(retStr) as Newtonsoft.Json.Linq.JObject;

                    if (retObj["errcode"] != null && retObj["errcode"].ToString() == "0")
                    {
                        CacheDependency dependency = new CacheDependency(null, new string[] { "access_token" });
                        cache.Insert("ticket", retObj["ticket"].ToString(), dependency); //缓存 键值依赖
                        return retObj["ticket"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        public static string SDKDebugger
        {
            get {
                return System.Configuration.ConfigurationManager.AppSettings["SDKDebugger"];
            }
        }

        public static string SDKjsApiList
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SDKjsApiList"];
            }
        }
        #endregion


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

        /// <summary>
        /// 从url中获取参数值
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramKey">参数名，大小写不敏感</param>
        /// <returns></returns>
        public static string GetParamValueFromUrl(this string url,string paramKey)
        {
            MatchCollection match= Regex.Matches(url,"(^|&)"+paramKey+"=([^$]*?)(&|$)",RegexOptions.IgnoreCase);
            if (match != null && match.Count > 0)
            {
                return match[0].Groups[2].Value;
            }
            return "";
        }



        public static string GetJsonValueStr(this string jsonStr, string paramKey)
        {
            MatchCollection match = Regex.Matches(jsonStr, "\"" + paramKey + "\":\"(.*?)\"");
            if (match != null && match.Count > 0)
            {
                return match[0].Groups[1].Value;
            }
            return "";
        }


        #region 生成随机字符串
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        public static  string GetRandomString(int length)
        {
            if (length <= 0) return "";

            Random rd = new Random();
            string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                result.Append(str[rd.Next(str.Length)]);
            }
            return result.ToString();
        }

        public static string GetRandomString()
        {
            return GetRandomString(5);
        }

        #endregion
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


        public static string Get(string url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            
            WebResponse response = request.GetResponse();
            int length = (int)response.ContentLength;
            byte[] buffer = new byte[length];
            response.GetResponseStream().Read(buffer, 0, length);

            string retStr = System.Text.Encoding.UTF8.GetString(buffer);
            return retStr;
        }

        public static string GetSHA1EnryptStr(string str)
        {

            System.Security.Cryptography.SHA1 sha;
            ASCIIEncoding enc;
            string hash = "";

            sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            enc = new ASCIIEncoding();
            byte[] dataToHash = enc.GetBytes(str);
            byte[] dataHashed = sha.ComputeHash(dataToHash);
            hash = BitConverter.ToString(dataHashed).Replace("-", "");
            hash = hash.ToLower();
            return hash;
        }
                    
           
    }
}
