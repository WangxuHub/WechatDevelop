using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Common;
using System.Net;
namespace WebChatDep
{
    /// <summary>
    /// WeChatServer 的摘要说明
    /// </summary>
    public class WeChatServer : Common.WebChatBase
    {
      //  HttpContext con;
        public override void ReceiveData(XmlDocument doc)
        {
          //  this.con = con;
            string MsgType=doc.FirstChild["MsgType"].InnerText;
            if (string.IsNullOrEmpty(MsgType))
            {
                con.Response.End();
            }

            #region 接收事件推送
            if (MsgType == "event")
            {
                string Event=doc.FirstChild["Event"].InnerText;
                XmlNode EventKey = doc.FirstChild["EventKey"];
                switch (Event.ToLower())
                {
                    case "subscribe"://订阅
                        if (EventKey==null)//普通订阅
                            Subscribe(doc);
                        break;
                    case "unsubscribe"://取消订阅
                        UnSubscribe(doc);
                        break;
                    case "location"://上报地理位置
                        Location(doc);
                        break;
                    default:break;
                }

                return;
            }
            #endregion
            switch (MsgType)
            {
                case "text": //接收文本信息
                    ReceiveDataText(doc);
                    break;
                default: return;
            }
        }

        #region 订阅 取消订阅
        //订阅,订阅事件可以接收，也能回复
        private void Subscribe(XmlDocument doc)
        {
            string retMsg = "无形公会，最为致命！！！！";

            //#region 数据库新增
            //DBModel.WXUser user = new DBModel.WXUser();
            //user.OpenID = doc.FirstChild["FromUserName"].InnerText;
            //user.CreateTime = doc.FirstChild["CreateTime"].InnerText.ToDateTimeFromWeChatSecond();
            //if (DBBLL.BWXUser.Insert(user) <= 0)
            //{
            //    return;
            //}
            //#endregion



            base.ResponseDataForText(doc, retMsg);
            return;
        }

        //取消订阅事件，可以接收，微信客户端无法接收回复消息
        private void UnSubscribe(XmlDocument doc)
        {
            string retMsg = "取消订阅!!";
            base.ResponseDataForText(doc, retMsg);
            return;
        }
        #endregion

        #region 上报地理位置
        private void Location(XmlDocument doc)
        {
            base.ResponseDataForText(doc, "上报地理位置");
            return;

        }
        #endregion


        #region ReceiveDataText 接收Text
        private void ReceiveDataText(XmlDocument doc)
        {
            string content = doc.FirstChild["Content"].InnerText;
            
            string retStr =string.Empty;
            if (content == "info")
            {
                retStr = string.Format(@"
自动回复 {0}
UserID:{1}
ServerID:{2}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), doc.FirstChild["FromUserName"].InnerText, doc.FirstChild["ToUserName"].InnerText);

            }
            else
            {
                retStr = "无形公会，最为致命!";
            }
            base.ResponseDataForText(doc, retStr);
            return;


        }
        #endregion


       

    }
}