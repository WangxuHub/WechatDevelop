<%@ Page Language="C#"   MasterPageFile="~/Master/BootStrap.Master"  AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebChatDep.Content.Index" 
    Title="WangxuHub首页"%>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server"></asp:Content>


 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">


      <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
          <!-- Indicators -->
          <ol class="carousel-indicators">
              <li data-target="#carousel-example-generic" data-slide-to="0" ></li>
              <li data-target="#carousel-example-generic" data-slide-to="1" class="active"></li>
              <li data-target="#carousel-example-generic" data-slide-to="2"></li>
              <li data-target="#carousel-example-generic" data-slide-to="3"></li>
              <li data-target="#carousel-example-generic" data-slide-to="4"></li>
          </ol>

          <!-- Wrapper for slides -->
          <div class="carousel-inner" role="listbox">
              <div class="item ">
                  <img src="../Resource/image/0d254fecf73e3f7f15532bcbce9c8f4d.jpg" />
                  <div class="carousel-caption">
                      <a class="btn btn-lg btn-primary">详细</a>
                  </div>
              </div>
              <div class="item active">
                  <img src="../Resource/image/25eb38827a598454432bf544ceb89ca3.jpg" />
                  <div class="carousel-caption">
                     111
                  </div>
              </div>
              <div class="item ">
                  <img src="../Resource/image/42155a2fa257681d24c3354fa83c0e7f.jpg" />
                  <div class="carousel-caption">
                     222
                  </div>
              </div>
              <div class="item ">
                  <img src="../Resource/image/bb5aa87cad3ad8b61099a27beca3aeeb.jpg" />
                  <div class="carousel-caption">
                     333
                  </div>
              </div>
              <div class="item ">
                  <img src="../Resource/image/da1a41df7208a8e23a1d2abb1e2dfc50.jpg" />
                  <div class="carousel-caption">
                    444
                  </div>
              </div>
          </div>

          <!-- Controls -->
          <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
              <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
              <span class="sr-only">上一页</span>
          </a>
          <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
              <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
              <span class="sr-only">下一页</span>
          </a>
      </div>

      <div class="container">
          <div class="row">
              <div class="col-md-4">col-md-4</div>
              <div class="col-md-4">col-md-4</div>
              <div class="col-md-4">col-md-4</div>
          </div>
      </div>


      <p>asdasd 123</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
      <p>asdasd</p>
</asp:Content>
