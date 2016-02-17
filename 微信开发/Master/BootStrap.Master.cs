using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChatDep.Master
{
    public partial class BootStrap : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserDataBind();

            }
        }


        private void UserDataBind()
        {
            if (!Common.CurUserStatus.IsLogin)
                return;

            liLogin.Visible = false;
            liRegister.Visible = false;
            liUserInfo.Visible = true;

            aUserName.InnerText = Common.CurUserStatus.UserInfo.NickName;
            aUserName.Attributes.Add("title", Common.CurUserStatus.UserInfo.NickName);
        }
    }
}