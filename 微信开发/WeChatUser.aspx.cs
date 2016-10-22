using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChatDep
{
    public partial class WeChatUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = Request["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    Response.Write(code);
                    GetWeChatUser(code);
                    return;
                }

                if (Session["username"] == null)
                {
                    string appid = Common.WeChatAppInfo.AppID;
                    string retUrl = HttpUtility.UrlEncode("http://polei.picp.net/WeChatUser.aspx");
                    string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo#wechat_redirect",
                        appid, retUrl);
                    Response.Redirect(url);
                }

            }
        }


        private void GetWeChatUser(string code)
        {
            //获取code后，请求以下链接获取access_token： 
            string url1 = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                Common.WeChatAppInfo.AppID, Common.WeChatAppInfo.Secret, code);
            string res1 = Common.WeChatHelper.Get(url1);
            //{
            //   "access_token":"ACCESS_TOKEN",
            //   "expires_in":7200,
            //   "refresh_token":"REFRESH_TOKEN",
            //   "openid":"OPENID",
            //   "scope":"SCOPE"
            //}
            Dictionary<string, object> dic1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res1);
            string access_token = (string)dic1["access_token"];
            string openid = (string)dic1["openid"];

            //拉取用户信息(需scope为 snsapi_userinfo)
            string url2 = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            string res2 = Common.WeChatHelper.Get(url2);
            Dictionary<string, object> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(res2);
            //{
            //    "openid":" OPENID",
            //    "nickname": NICKNAME,
            //    "sex":"1",
            //    "province":"PROVINCE"
            //    "city":"CITY",
            //    "country":"COUNTRY",
            //    "headimgurl":    "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46", 
            //    "privilege":[
            //    "PRIVILEGE1"
            //    "PRIVILEGE2"
            //    ],
            //    "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
            //}

            Response.Write(res2);

               //

        }
    }
}