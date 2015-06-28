<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row">
        <div class="karikatur-bg">
            <div class="col-md-8 col-lg-8 col-xs-12 no-margin">
                <div class="img-bg">
                    <div class="img-trans"></div>
                    <img id="imgKarikatur" runat="server" src="/" data-adi="" class="img-responsive" style="max-height: 450px; margin: auto !important;" />
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <nav style="text-align: center;" id="sayfalama">

                            <asp:ListView runat="server" ID="lstSayfalama" ClientIDMode="Static">
                                <LayoutTemplate>
                                    <ul class="pagination pagination-lg text-center">
                                        <li class="disabled"><a href="javascript:;" class="btnSonraki" disabled="disabled"><span aria-hidden="true">&laquo;</span><span class="sr-only">Sonraki</span></a></li>
                                        <asp:PlaceHolder ID="GroupPlaceHolder" runat="server"></asp:PlaceHolder>
                                        <li><a href="javascript:;" class="btnOnceki"><span aria-hidden="true">&raquo;</span><span class="sr-only">Önceki</span></a></li>
                                    </ul>
                                    </table>
                                </LayoutTemplate>
                                <GroupTemplate>
                                    <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <li><a href="javascript:;" class="karikatur" data-adi="<%# Eval("FotografAdi") %>" data-id="<%# Eval("IdFotograf") %>"><%# Eval("IdFotograf") %></a></li>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    Boş Data
                                </EmptyDataTemplate>
                            </asp:ListView>


                            <%--    
                            FotografAdi
                             FotografYolu
                            FotografTuru
                            FotografBoyutu
                            --%>
                        </nav>
                    </div>

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
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="Server">
</asp:Content>
