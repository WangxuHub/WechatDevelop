<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OAuthTest.aspx.cs" Inherits="WebChatDep.OAuthTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    function qqLogin()
    {
    //以下为按钮点击事件的逻辑。注意这里要重新打开窗口
    //否则后面跳转到QQ登录，授权页面时会直接缩小当前浏览器的窗口，而不是打开新窗口
    var A=window.open("oauth/index.php","TencentLogin", 
    "width=450,height=320,menubar=0,scrollbars=1,resizable=1,status=1,titlebar=0,toolbar=0,location=1");
    }

    function qqOAuth() {
        var url = "https://graph.qq.com/oauth2.0/authorize";
        var getData = new Object();
        getData.response_type = "code";
        getData.client_id = "<%=OAuthAppID %>";
        getData.redirect_uri = location.origin + "/OAuthTest.aspx";
        getData.state = "<%=randStr %>";
        var parm = "?";
        for (var key in getData) {
            parm += key+"=" + encodeURIComponent(getData[key])+"&";
        }
        url += parm;
        //  window.open(url);
        location.href = url;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="/Resource/qqOAuth/Image/Connect_logo_5.png" onclick="qqLogin();"/>



        <a onclick="qqOAuth();return false;">
           <img src="/Resource/qqOAuth/Image/Connect_logo_4.png" />
        </a>

    </div>

    
    </form>
  <label id="lbl_1" runat="server"></label> <br />
  <label id="lbl_2" runat="server"></label><br />
  <label id="lbl_3" runat="server"></label><br />

  <p>=======================================================</p>
  <label id="lbl_4" runat="server"></label><br />
</body>
</html>
