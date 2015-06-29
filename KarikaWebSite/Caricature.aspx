<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Caricature.aspx.cs" Inherits="Caricature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row">
        <div class="karikatur-bg">
            <div class="col-md-8 col-lg-8 col-xs-12 no-margin">
                <div class="img-bg">
                    <div class="img-trans"></div>
                    <asp:Image ID="imgKarikatur" runat="server" data-adi="" class="img-responsive" style="max-height: 450px; margin: auto !important;" />
                </div>

            </div>

            <div class="col-md-4 col-lg-4 col-xs-12 detay">
                <h3 id="karikaturBaslik" class="baslik"></h3>
                <div id="karikaturAciklama"></div>
                <abbr id="karikaturEklenmeTarihi" class="label label-default text-left" style="float: right;" title=''></abbr>

                <br />
                <div class="fb-like" data-href="http://www.soneracar.net/" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>
                <a id="btnIndir" href="#" class="btn btn-xs btn-primary" download="">Karikatürü İndir</a>

                <br />
                <hr />

                <div class="fb-comments" data-href="http://www.soneracar.net/" data-width="100%" data-numposts="5" data-colorscheme="light"></div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblSonFotografId" runat="server" ClientIDMode="Static" Style="margin-left: -3000px; line-height: 0; width: 0; height: 0; opacity: 0; display: block; position: absolute;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" Runat="Server">
        <script src="/assets/plugins/jquery-timeago/jquery.timeago.js"></script>
        <script src="/assets/plugins/jquery-timeago/jquery.timeago.tr.js"></script>
        <script src="/assets/js/Caricature.js"></script>
</asp:Content>

