using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Helper
{
    public abstract class CommonAshx : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public HttpContext conn;
        public mJsonResult json = new mJsonResult();

        public abstract void ExecPostType(string PostType);

        public void ProcessRequest(HttpContext context)
        {
            conn = context;

            string PostType = conn.Request["PostType"];
            if (string.IsNullOrEmpty(PostType))
            {
                json.success = false;
                json.msg = "无效参数";
                conn.Response.Write(json.ToJson());
                conn.Response.End();
            }

            ExecPostType(PostType);

            if (json != null)
            {
                conn.Response.Write(json.ToJson());
                conn.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
