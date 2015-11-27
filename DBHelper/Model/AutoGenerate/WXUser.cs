using System;
using System.Collections;
using System.Collections.Generic;
using DBDAL;

namespace DBModel
{
	[Serializable]
    public partial class WXUser
	{
        private int? userID; 
        private string openID; 
        private string weChatName; 
        private string remarkName; 
        private string trueName; 
        private string phone; 
        private string qQ; 
        private DateTime? createTime; 
	
	
	    /// <summary>
        /// 主键
        /// </summary>
		public int? UserID
		{
		    get{ return userID; }
			set{ userID=value; }
		}
	    /// <summary>
        /// 用户的OpenID
        /// </summary>
		public string OpenID
		{
		    get{ return openID; }
			set{ openID=value; }
		}
	    /// <summary>
        /// 微信昵称
        /// </summary>
		public string WeChatName
		{
		    get{ return weChatName; }
			set{ weChatName=value; }
		}
	    /// <summary>
        /// 备注名
        /// </summary>
		public string RemarkName
		{
		    get{ return remarkName; }
			set{ remarkName=value; }
		}
	    /// <summary>
        /// 真实名称
        /// </summary>
		public string TrueName
		{
		    get{ return trueName; }
			set{ trueName=value; }
		}
	    /// <summary>
        /// 
        /// </summary>
		public string Phone
		{
		    get{ return phone; }
			set{ phone=value; }
		}
	    /// <summary>
        /// 
        /// </summary>
		public string QQ
		{
		    get{ return qQ; }
			set{ qQ=value; }
		}
	    /// <summary>
        /// 
        /// </summary>
		public DateTime? CreateTime
		{
		    get{ return createTime; }
			set{ createTime=value; }
		}
	}
}