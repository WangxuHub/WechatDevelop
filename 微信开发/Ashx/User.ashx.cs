using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Helper;
using DBHelper.Model;
using DBHelper.BLL;
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
                case "RegisterUser":
                    RegisterUser();//用户注册
                    break;
                case "LoginUser":
                    LoginUser();//用户登录
                    break;
                case "LoginOut":
                    LoginOut();//用户注销
                    break;
                default:
                    break;

            }
        }

        #region 用户注册
        private void RegisterUser()
        {
            try
            {
                JCUser user = new JCUser();
                #region 验证
                string regNickName = conn.Request["regNickName"];
                if (string.IsNullOrEmpty(regNickName))
                {
                    json.success = false;
                    json.msg = "请输入昵称";
                    return;
                }


                string regUserName = conn.Request["regUserName"];
                if (string.IsNullOrEmpty(regUserName))
                {
                    json.success = false;
                    json.msg = "请输入账号";
                    return;
                }
                if (regUserName.Length < 6)
                {
                    json.success = false;
                    json.msg = "长度不得小于6";
                    return;
                }
                if (regUserName.Length > 100)
                {
                    json.success = false;
                    json.msg = "长度不得大于100";
                    return;
                }


                string regPassword = conn.Request["regPassword"];
                if (string.IsNullOrEmpty(regPassword))
                {
                    json.success = false;
                    json.msg = "请输入密码";
                    return;
                }
                if (regPassword.Length < 8)
                {
                    json.success = false;
                    json.msg = "长度不得小于8";
                    return;
                }
                if (regPassword.Length > 100)
                {
                    json.success = false;
                    json.msg = "长度不得大于100";
                    return;
                }

                string regPasswordCheck = conn.Request["regPasswordCheck"];
                if (string.IsNullOrEmpty(regPasswordCheck))
                {
                    json.success = false;
                    json.msg = "请输入密码确认";
                    return;
                }

                if (regPassword != regPasswordCheck)
                {
                    json.success = false;
                    json.msg = "两次密码不一致";
                    return;
                }

                string regEmail = conn.Request["regEmail"];
                if (string.IsNullOrEmpty(regEmail))
                {
                    json.success = false;
                    json.msg = "请输入邮箱";
                    return;
                }

                if (!Common.Helper.Common.IsValidEmail(regEmail))
                {
                    json.success = false;
                    json.msg = "邮箱格式不正确";
                    return;
                }

               // BJCUser.Select();
                #endregion

                bool isExist = BJCUser.Select("UserName=@UserName", new ParameterList("@UserName", regUserName)).Count > 0;

                if(isExist)
                {
                    json.success = false;
                    json.msg = "账号已存在，请重新输入";
                    return;
                }

                user.UserName = regUserName;
                user.NickName = regNickName;
                user.PassWord= Common.Helper.Common.GetSHA1EnryptStr(regPassword);
                user.Email = regEmail;
                user.CreateTime = DateTime.Now;
                Guid tempGuid= BJCUser.Insert(user);

                if (tempGuid != new Guid())
                {
                    json.success = true;
                    json.msg = "新增成功";
                }
                else
                {
                    json.success = false;
                    json.msg = "新增失败";
                }
            }
            catch (Exception e)
            {
                json.success = false;
                json.msg = e.Message;
            }
        }
        #endregion

        #region 用户登录
        private void LoginUser()
        {
            string lgnUserName = conn.Request["lgnUserName"];
            string lgnPassWord = conn.Request["lgnPassWord"];

            #region 验证
            if (string.IsNullOrEmpty(lgnUserName))
            {
                json.success = false;
                json.msg = "请输入账号或邮箱";
                return;
            }


            if (string.IsNullOrEmpty(lgnPassWord))
            {
                json.success = false;
                json.msg = "请输入密码";
                return;
            }
            #endregion 

            List<JCUser> list = BJCUser.Select("UserName=@UserName",new ParameterList("@UserName",lgnUserName));

            if (list.Count <= 0)
            {
                json.success = false;
                json.msg = "账号不存在";
                return;
            }

            string enryptPassword= Common.Helper.Common.GetSHA1EnryptStr(lgnPassWord);
            bool isVaild = list[0].PassWord == enryptPassword;
            if (!isVaild)
            {
                json.success = false;
                json.msg = "密码错误";
                return;
            }

            list[0].LastLoginTime = DateTime.Now;
            BJCUser.Update(list[0]);

            Common.CurUserStatus.Login(list[0]);//登录操作

            if (conn.Request["autoLgn"] == "true") //用cookie记住密码
            {
                Common.CurUserStatus.RememberLoginStatusByCookie(true);
            }
            else
            {
                Common.CurUserStatus.RememberLoginStatusByCookie(false);
            }

            json.msg = "登录成功";
            json.success = true;
            return;
        }
        #endregion

        #region 用户注销
        private void LoginOut()
        {
             
            json.success= Common.CurUserStatus.LoginOut();
        }
        #endregion 
    }
}