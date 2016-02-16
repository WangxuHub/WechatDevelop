using System;
using System.Collections;
using System.Collections.Generic;

namespace DBHelper.ObjectQuery
{
    internal partial class JCUserMap:IMap
	{
	    private Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public JCUserMap()
        {
        	dictionary.Add("userid", "UserID");
        	dictionary.Add("username", "UserName");
        	dictionary.Add("password", "PassWord");
        	dictionary.Add("nickname", "NickName");
        	dictionary.Add("truename", "TrueName");
        	dictionary.Add("email", "Email");
        	dictionary.Add("phone", "Phone");
        	dictionary.Add("qq", "QQ");
        	dictionary.Add("createtime", "CreateTime");
        	dictionary.Add("lastlogintime", "LastLoginTime");
        	dictionary.Add("birthday", "Birthday");
        }

        #region IMap 成员

        public string this[string propertyName]
        {
            get
			{ 
				try
				{
					return dictionary[propertyName.ToLower()]; 
				}
				catch (KeyNotFoundException)
				{
					throw new Exception(propertyName + "属性不存在");
				}
			}
        }

        #endregion
	}
}