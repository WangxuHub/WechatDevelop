<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatSDK.aspx.cs" Inherits="WebChatDep.WeChatSDK" %>

<%@ Register Src="~/UserControl/WeChatConfigControl.ascx" TagPrefix="uc1" TagName="WeChatConfigControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,minimum-scale=1,user-scalable=no"/>
    <title></title>
    
    <script src='/Resource/js/jquery-1.8.2.js'></script>
    <script src='/Resource/js/jweixin-1.0.0.js'></script>
    <uc1:WeChatConfigControl runat="server" ID="WeChatConfigControl" />
<script defer="defer">

    wx.ready(function () {

        // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。

        wx.checkJsApi({
            jsApiList: ['chooseImage'], // 需要检测的JS接口列表，所有JS接口列表见附录2,
            success: function (res) {
                alert(res)
                // 以键值对的形式返回，可用的api值true，不可用为false
                // 如：{"checkResult":{"chooseImage":true},"errMsg":"checkJsApi:ok"}
            }
        });



    });

    wx.error(function (res) {

        // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

    });

  

</script>
</head>
<body>
   <asp:Literal runat="server" ID="asd"></asp:Literal>
</body>
</html>
