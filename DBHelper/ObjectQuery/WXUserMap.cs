using System;
using System.Collections;
using System.Collections.Generic;

namespace DBObjectQuery
{
    internal partial class WXUserMap:IMap
	{
	    private Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public WXUserMap()
        {
        	dictionary.Add("userid", "UserID");
        	dictionary.Add("openid", "OpenID");
        	dictionary.Add("wechatname", "WeChatName");
        	dictionary.Add("remarkname", "RemarkName");
        	dictionary.Add("truename", "TrueName");
        	dictionary.Add("phone", "Phone");
        	dictionary.Add("qq", "QQ");
        	dictionary.Add("createtime", "CreateTime");
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