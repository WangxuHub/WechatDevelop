using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Text;
namespace WebChatDep
{
    public partial class WeChatSDK : WebChatSDK
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
           //     InitWxConfig();
            }

            
        }

        /// <summary>
        /// 生成微信配置
        /// </summary>
        private void InitWxConfig()
        {
            timestamp = DateTime.Now.ToWeChatSecondFromDateTime().ToString();
            noncestr = WeChatHelper.GetRandomString(16);
            url = Request.Url.AbsoluteUri;
            jsapi_ticket = WeChatAppInfo.ticket;

            List<Dictionary<string, string>> sortList = new List<Dictionary<string, string>>() { 
                new Dictionary<string, string>(){{"key","jsapi_ticket"},{"value",jsapi_ticket}},
                new Dictionary<string, string>(){{"key","noncestr"},{"value",noncestr}},
                new Dictionary<string, string>(){{"key","timestamp"},{"value",timestamp}},
                new Dictionary<string, string>(){{"key","url"},{"value",url}}
            };


            StringBuilder keyValueSB = new StringBuilder();
            foreach (Dictionary<string, string> item in sortList)
            {
                keyValueSB.AppendFormat("{0}={1}&", item["key"], item["value"]);
            }
            keyValueSB.Remove(keyValueSB.Length - 1, 1);
            string str = keyValueSB.ToString();

            hash = WeChatHelper.GetSHA1EnryptStr(str);
        }
    }
}