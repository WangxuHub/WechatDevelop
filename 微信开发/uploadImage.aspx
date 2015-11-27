<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadImage.aspx.cs" Inherits="WebChatDep.uploadImage" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1,minimum-scale=1,user-scalable=no"/>
    <title></title>
    <script src="Resource/js/jquery-1.8.2.js"></script>
    <script>
        function handleFiles(files) {

            //遍历files并处理  

            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var imageType = /image.*/;

                //通过type属性进行图片格式过滤  
                if (!file.type.match(imageType)) {
                    continue;
                }

                //读入文件  
                var reader = new FileReader();   
                reader.onload = function(e){  
  
                    //e.target.result返回的即是图片的dataURI格式的内容  
  
                    var imgData = e.target.result;
  
                    var img = document.getElementById('img');  
  
                    img.src = imgData;  
  
                    //展示img  
  
                }  
                reader.readAsDataURL(file);   
            }   

        }

        window.onresize = bindData;
        $(function () {
            bindData();
        });

        function bindData()
        {

            var info = "";
            info = info + "window width:" + window.screen.width + " window.height:" + window.screen.height + "<br/>";
            info = info + "window innerWidth:" + window.innerWidth + " window.innerHeight:" + window.innerHeight + "<br/>";
            info = info + "window outerWidth:" + window.outerWidth + " window.outerHeight:" + window.outerHeight + "<br/>";
            document.getElementById("screenInfo").innerHTML = info;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div type="text" id="screenInfo" ></div>
        <input type="file" id="file" onchange="handleFiles(this.files)" accept="image/*"/>
        <img id="img"/>
    <div>
    
    </div>
    </form>
</body>
</html>