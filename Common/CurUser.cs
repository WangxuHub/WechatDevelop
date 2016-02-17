using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace Common
{
    public class CurUserStatus : System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 用户是否已经登录
        /// </summary>
        public static bool IsLogin
        {
            get{
                Object temp= System.Web.HttpContext.Current.Session["CurUserDetailInfo"];
                if(temp!=null && temp is CurUserDetailInfo)
                   return true;

                //如果session中没有登录，则从cookie中读取
                return Login();
            }
        }

        /// <summary>
        /// 记住登录状态
        /// </summary>
        /// <param name="isRemember"></param>
        public static void RememberLoginStatusByCookie(bool isRemember)
        {
            HttpCookie cook = new HttpCookie("userInfo");
           
            CurUserDetailInfo user= ((CurUserDetailInfo)System.Web.HttpContext.Current.Session["CurUserDetailInfo"]);
            var obj = new { UserName = user.UserName, PassWord = user.PassWord };
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            string desStr = Common.Helper.Common.EnDesCode(jsonStr);
            cook.Value = desStr;
            if (isRemember)
            {
                cook.Expires = DateTime.Now.AddDays(7);
            }
            else
            {
                ;
            }
            HttpContext.Current.Response.Cookies.Add(cook);
        }

        /// <summary>
        /// 用Cookie进行登录
        /// </summary>
        /// <returns></returns>
        public static bool Login()
        {
            HttpCookie cook=HttpContext.Current.Request.Cookies.Get("userInfo");
            if(cook==null || string.IsNullOrEmpty(cook.Value))
            {
                return false;
            }

            try{
                string jsonStr=Common.Helper.Common.DeDesCode(cook.Value);
                Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr) as Newtonsoft.Json.Linq.JObject;

                string userName = Convert.ToString(obj.GetValue("UserName"));
                string passWord = Convert.ToString(obj.GetValue("PassWord"));
                List<DBHelper.Model.JCUser> list=DBHelper.BLL.BJCUser.Select("UserName=@UserName and PassWord=@PassWord",
                                            new DBHelper.Model.ParameterList("@UserName", userName, "@PassWord",passWord));

                if (list.Count > 0)
                {
                    DBHelper.Model.JCUser user=list[0];
                  
                    return Login(user);
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 用当前用户的实体模型进行登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool Login(DBHelper.Model.JCUser user)
        {
            if (user == null) return false;
            CurUserDetailInfo info = new CurUserDetailInfo();
            info.UserID = user.UserID;
            info.UserName = user.UserName;
            info.PassWord = user.PassWord;
            info.NickName = user.NickName;
            info.TrueName = user.TrueName;
            info.Email = user.Email;
            info.Phone = user.Phone;
            info.QQ = user.QQ;
            info.CreateTime = user.CreateTime;
            info.LastLoginTime = user.LastLoginTime;
            info.Birthday = user.Birthday;

            System.Web.HttpContext.Current.Session["CurUserDetailInfo"] = info;
                
                

            return true;
        }

        /// <summary>
        /// 用户登出，注销，清空session和cookie
        /// </summary>
        /// <returns></returns>
        public static bool LoginOut()
        {
            try
            {

                System.Web.HttpContext.Current.Session.RemoveAll();


                HttpCookie cook = new HttpCookie("userInfo");
                cook.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cook);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static CurUserDetailInfo UserInfo
        {
            get
            {
                return (CurUserDetailInfo)System.Web.HttpContext.Current.Session["CurUserDetailInfo"];
            }
        }
    }

    public class CurUserDetailInfo
    {
         /// <summary>
        /// 主键
        /// </summary>
		public Guid? UserID;

	    /// <summary>
        /// 账号
        /// </summary>
		public string UserName;
	    /// <summary>
        /// 密码
        /// </summary>
		public string PassWord;
	    /// <summary>
        /// 昵称
        /// </summary>
		public string NickName;
	    /// <summary>
        /// 真实名称
        /// </summary>
		public string TrueName;
	    /// <summary>
        /// 
        /// </summary>
		public string Email;
	    /// <summary>
        /// 
        /// </summary>
		public string Phone;
	    /// <summary>
        /// 
        /// </summary>
		public string QQ;
	    /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime;
	    /// <summary>
        /// 最后登录时间
        /// </summary>
		public DateTime? LastLoginTime;
	    /// <summary>
        /// 
        /// </summary>
		public DateTime? Birthday;
    }
}
