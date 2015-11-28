using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    
    //用于调用 微信js-SDK专用
    public class WebChatSDK : System.Web.UI.Page
    {
        protected string timestamp;
        protected string noncestr;
        protected string url;
        protected string jsapi_ticket;
        protected string hash;

        protected override void OnInit(EventArgs e)
        {
            string userAgent=Request.UserAgent;
            bool onlyClient = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["AllowOnlyWechatClient"]);
            if (onlyClient && userAgent.IndexOf("MicroMessenger") < 0)
            {
                Response.Write("<meta name=\"viewport\" content=\"width=device-width,initial-scale=1,maximum-scale=1,minimum-scale=1,user-scalable=no\"/>请使用微信浏览器访问");
                Response.End();
            }
            else
            {
                base.OnInit(e);
                InitWxConfig();
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

            System.Web.UI.WebControls.Literal wxConfig = new System.Web.UI.WebControls.Literal();
            wxConfig.Text=string.Format(@"
    <script src='/Resource/js/jquery-1.8.2.js'></script>
    <script src='/Resource/js/jweixin-1.0.0.js'></script>
<script defer='defer'>
wx.config({{
    debug: {0}, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
    appId: '{1}', // 必填，公众号的唯一标识
    timestamp: '{2}', // 必填，生成签名的时间戳
    nonceStr: '{3}', // 必填，生成签名的随机串
    signature: '{4}',// 必填，签名，见附录1
    jsApiList: [{5}] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
}});
</script>
",WeChatAppInfo.SDKDebugger, Common.WeChatAppInfo.AppID, timestamp, noncestr, hash,WeChatAppInfo.SDKjsApiList);

            Page.Header.Controls.AddAt(0,wxConfig);
        }
    }
}
