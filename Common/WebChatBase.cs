using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Xml;

namespace Common
{ 
    public abstract class WebChatBase : IHttpHandler
    { 
          
         
       // public static Tencent.WXBizMsgCrypt WeChatCrypt;
        public HttpContext con;

        string signature;
        string timestamp;
        string nonce;
        string echoStr;

        public void ProcessRequest(HttpContext context)
        {
            con = context;
            con.Response.ContentType = "text/plain";

            signature = context.Request.QueryString["signature"] ?? "";
            timestamp = context.Request.QueryString["timestamp"] ?? "";
            nonce = context.Request.QueryString["nonce"] ?? "";
            echoStr = context.Request.QueryString["echoStr"];
           

          //  WeChatCrypt = Tencent.WXBizMsgCrypt(WeChatAppInfo.Token, WeChatAppInfo.sEncodingAESKey, WeChatAppInfo.AppID);

            string requestContent = System.Text.Encoding.UTF8.GetString(context.Request.BinaryRead(context.Request.TotalBytes));

            int ret = 0;
            string sMsg = "";  //解析之后的明文
            ret = Tencent.WXBizMsgCrypt.DecryptMsg(signature, timestamp, nonce, requestContent, ref sMsg);
            if (ret != 0)
            {
                context.Response.Write("无效请求");
                context.Response.End();
                return;
            }
            else if (!string.IsNullOrEmpty(echoStr))
            {
                context.Response.Write(echoStr);
                context.Response.End();
                return;
            }


            XmlDocument doc = new XmlDocument();
            XmlNode root;
            doc.LoadXml(sMsg);

            if(doc.FirstChild["MsgId"]!=null)
            {
                if (!IsUniqueMsg(doc.FirstChild["MsgId"].InnerText))
                {
                    context.Response.End();
                    return;
                }
            }
            ReceiveData(doc);
        }


        public abstract void ReceiveData(XmlDocument doc);


        /// <summary>
        /// 判断是否是唯一的消息ID，放在缓存中
        /// </summary>
        /// <param name="msgID"></param>
        private bool IsUniqueMsg(string msgID)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            if (cache.Get(msgID) != null)
            {
                return false;
            }
            else
            {
                cache.Insert(msgID, msgID, null,Cache.NoAbsoluteExpiration,TimeSpan.FromSeconds(60)); //缓存时间依赖
                return true;
            }
        }


        #region  被动回复信息
        /// <summary>
        /// 被动回复信息 文本类型
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="MsgType"></param>
        /// <remarks>2015-11-8 陈双宇</remarks>
        public void ResponseDataForText(XmlDocument doc,string content)
        {
            string ToUserName = doc.FirstChild["ToUserName"].InnerText;
            string FromUserName = doc.FirstChild["FromUserName"].InnerText;
            string CreateTime = doc.FirstChild["CreateTime"].InnerText;

            string retStr = string.Format(@"
            <xml>
              <ToUserName><![CDATA[{0}]]></ToUserName>
              <FromUserName><![CDATA[{1}]]></FromUserName>
              <CreateTime>{2}</CreateTime>
              <MsgType><![CDATA[{3}]]></MsgType>
              <Content><![CDATA[{4}]]></Content>
            </xml>", FromUserName, ToUserName, DateTime.Now.ToWeChatSecondFromDateTime(), "text", content);
         

            string sEncryptMsg=string.Empty;
            int ret = Tencent.WXBizMsgCrypt.EncryptMsg(retStr, timestamp, nonce, ref sEncryptMsg);
            if (ret != 0)
            {
                con.Response.Write("加密失败");
                con.Response.End();
                return;
            }
            else
            {
                con.Response.Write(retStr);
                //con.Response.Write(sEncryptMsg);
                con.Response.End();
                return;
            }
        }
        #endregion

        /// <summary>
        /// 将文本信息放置在![CDATA[{0}]]，
        /// </summary>
        /// <param name="ClearStr"></param>
        /// <returns></returns>
        public string SetCDataTxt(string ClearStr)
        {
            return string.Format("![CDATA[{0}]]",ClearStr);
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
