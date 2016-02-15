using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Helper;
namespace WebChatDep.Ashx
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class User : CommonAshx
    {
        public override void  ExecPostType(string PostType)
        {
            switch (PostType)
            {
                case "type1":
                    funct1();
                    break;
                default:
                    break;

            }
        }

        private void funct1()
        {
            json.msg = conn.Request["PostType"];
        }

    }
}