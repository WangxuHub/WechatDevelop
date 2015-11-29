using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Text;
using Common.Helper;
namespace WebChatDep
{
    public partial class WeChatSDK : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
          
            }
            
        }

        protected override void OnInit(EventArgs e)
        {
            string PostType = Request["PostType"];
            if (string.IsNullOrEmpty(PostType))
            {
                base.OnInit(e);
                return;
            }

            Common.Helper.mJsonResult json = new Common.Helper.mJsonResult();
            switch(PostType)
            {
                case "getWechatImage": 
                    GetWechatImage();//从微信服务器上获取图片
                    break;
                default: break;
            }
            if(json!=null)
            {
                string retStr = json.ToJson();
                Response.Write(retStr);
                Response.End();
            }
        }

        #region 从微信服务器上获取图片
        protected  void GetWechatImage()
        {
            string serverId = Request["serverId"];

            string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", WeChatAppInfo.access_token,serverId);
            string response = Common.WeChatHelper.Post(url,"");
        }
        #endregion


    }
}