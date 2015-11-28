using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChatDep
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string a11 = Common.WeChatAppInfo.access_token;
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string groupName = this.TextBox1.Text;
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}",Common.WeChatAppInfo.access_token);
            string bodyStr = string.Format("{{\"group\":{{\"name\":\"{0}\"}}}}", groupName);
            string response = Common.WeChatHelper.Post(url,bodyStr);

            string asd= Common.WeChatAppInfo.ticket;
        }
    }
}