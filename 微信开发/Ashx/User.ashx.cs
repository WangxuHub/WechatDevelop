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
                    RegisterUser();
                    break;
                default:
                    break;

            }
        }

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
                if (regUserName.Length < 8)
                {
                    json.success = false;
                    json.msg = "长度不得小于8";
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
                user.PassWord= regPassword;
                user.Email = regEmail;
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

    }
}