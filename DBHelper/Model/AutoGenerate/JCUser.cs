using System;
using System.Collections;
using System.Collections.Generic;
using DBHelper.DAL;

namespace DBHelper.Model
{
	[Serializable]
    public partial class JCUser
	{
        private string userID; 
        private string userName; 
        private string passWord; 
        private string nickName; 
        private string trueName; 
        private string email; 
        private string phone; 
        private string qQ; 
        private DateTime? createTime; 
        private DateTime? lastLoginTime; 
        private string birthday; 
	
	
	    /// <summary>
        /// 主键
        /// </summary>
		public string UserID
		{
		    get{ return userID; }
			set{ userID=value; }
		}
	    /// <summary>
        /// 账号
        /// </summary>
		public string UserName
		{
		    get{ return userName; }
			set{ userName=value; }
		}
	    /// <summary>
        /// 密码
        /// </summary>
		public string PassWord
		{
		    get{ return passWord; }
			set{ passWord=value; }
		}
	    /// <summary>
        /// 昵称
        /// </summary>
		public string NickName
		{
		    get{ return nickName; }
			set{ nickName=value; }
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
		public string Email
		{
		    get{ return email; }
			set{ email=value; }
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
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime
		{
		    get{ return createTime; }
			set{ createTime=value; }
		}
	    /// <summary>
        /// 最后登录时间
        /// </summary>
		public DateTime? LastLoginTime
		{
		    get{ return lastLoginTime; }
			set{ lastLoginTime=value; }
		}
	    /// <summary>
        /// 
        /// </summary>
		public string Birthday
		{
		    get{ return birthday; }
			set{ birthday=value; }
		}
	}
}