using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
namespace WebChatDep
{
    public partial class OAuthTest : System.Web.UI.Page
    {
        protected string OAuthAppID = System.Configuration.ConfigurationManager.AppSettings["OAuthAppID"] ?? "";
        protected string OAuthAppKey =System.Configuration.ConfigurationManager.AppSettings["OAuthAppKey"] ?? "";
        protected string randStr = Common.WeChatHelper.GetRandomString(36);
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("qqRandNum", randStr);
            cookie.Path = "csy";
            Response.Cookies.Add(cookie);
            if (!string.IsNullOrEmpty(Request.QueryString["state"]) && !string.IsNullOrEmpty(Request.QueryString["code"]))
            {

                GetQQUserInfo();
            }
            else
            {
              
               
            }
        }

        private void GetQQUserInfo()
        {
            //判断state，是否等于cookie，防止CSRF攻击
            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];
            string cookieState = Request.Cookies.Get("qqRandNum").Value;
            if (cookieState != state)
            {
                return;
            }

            string url = "https://graph.qq.com/oauth2.0/token";
            url += "?grant_type=authorization_code";
            url += "&client_id=" + OAuthAppID;
            url += "&client_secret=" + OAuthAppKey;
            url += "&code=" + code;
            url += "&redirect_uri=" + HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Host + "/OAuthTest.aspx");

            string retStr = Common.WeChatHelper.Get(url);

            string access_token = retStr.GetParamValueFromUrl("access_token");
            string expires_in = retStr.GetParamValueFromUrl("expires_in");
            string refresh_token = retStr.GetParamValueFromUrl("refresh_token");


            lbl_1.InnerHtml = "access_token: "+access_token;
            lbl_2.InnerHtml = "expires_in: "+expires_in;
            lbl_3.InnerHtml = "refresh_token: "+refresh_token;


            if (!string.IsNullOrEmpty(access_token))
            {
                string retClienStr = Common.WeChatHelper.Get("https://graph.qq.com/oauth2.0/me?access_token="+access_token);
                string openid = retClienStr.GetJsonValueStr("openid");
                lbl_4.InnerHtml = openid;
            }
        }

    }
}