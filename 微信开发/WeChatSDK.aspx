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

<style type="text/css">
#imageList img
{
    width:100%;
}


</style>    

<script defer="defer">

wx.ready(function () {

    // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。

    //wx.checkJsApi({
    //    jsApiList: ['chooseImage'], // 需要检测的JS接口列表，所有JS接口列表见附录2,
    //    success: function (res) {
    //        // 以键值对的形式返回，可用的api值true，不可用为false
    //        // 如：{"checkResult":{"chooseImage":true},"errMsg":"checkJsApi:ok"}
    //    }
    //});

    // wx.hideOptionMenu();

});

wx.error(function (res) {

    // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

});

function chooseImage()
{
    wx.chooseImage({
        count: 9, // 默认9
        sizeType: ['compressed'], // 可以指定是原图还是压缩图，默认二者都有
        sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
        success: function (res) {
            var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
            $("#chooseImage").data("localIds", localIds);
        }
    });
}

function showImageList()
{
    var localIds = $("#chooseImage").data("localIds");
    
    localIds.forEach(function (value) {
        $("#imageList").append("<img src='"+value+"'/>");
    });
}

function viewImage()
{
    var localIds = $("#chooseImage").data("localIds");

    alert(localIds[0]);
    if (!localIds || localIds.length <= 0) {
        alert("无图片可预览");
        return;
    }
    wx.previewImage({
        current: localIds[0], // 当前显示图片的http链接
        urls: localIds // 需要预览的图片http链接列表
    });
}

function uploadImage()
{
    var localIds = $("#chooseImage").data("localIds");
    wx.uploadImage({
        localId: localIds[0], // 需要上传的图片的本地ID，由chooseImage接口获得
        isShowProgressTips: 1, // 默认为1，显示进度提示
        success: function (res) {
            var serverId = res.serverId; // 返回图片的服务器端ID
            alert("serverId:" + serverId);
            $.post("?r=" + Math.random(), { PostType:"getWechatImage" ,serverId:serverId }, function (json) {
            },'json');
        }
    });
}
</script>
</head>
<body>
    <div>
        <input type="button" id="chooseImage" onclick="chooseImage();"  value="选择图片"/>
        <input type="button" id="viewImage" onclick="viewImage();"  value="预览图片"/>
        <input type="button" id="uploadImage" onclick="uploadImage();"  value="上传图片"/>
        <input type="button" id="showImageList" onclick="showImageList();"  value="显示本地图片"/>
        
    </div>
    <div id="imageList"></div>
</body>
</html>
