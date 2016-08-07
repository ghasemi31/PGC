<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Pages_Guest_Gallery" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">

    <%this.Title = OptionBusiness.GetText(pgc.Model.Enums.OptionKey.GalleryTitle); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_Gallery) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keyworde_Gallery) %>" />
    <link href="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/jquery.fancybox.css?v=2.1.5" rel="stylesheet" />
    <link href="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" rel="stylesheet" />
    <link href="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" rel="stylesheet" />
    <link href="/assets/css/gallery/gallery.min.css?v=2.2" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <%-- BEGIN HEADER --%>
    <header id="gallery-header" class="main-body">
        <div class="container-fluid margin-left-right-0">
            <h1>گالری عکس مستردیزی</h1>
        </div>
    </header>
    <%-- END HEADER --%>
    <%-- BEGIN CONTENT --%>
    <section id="gallary-content" class="main-body">
        <div class="container">

            <div class="row col-lg-offset-1 col-lg-10">
                <asp:ObjectDataSource ID="odsGallery"
                    runat="server"
                    EnablePaging="True"
                    SelectCountMethod="Gallery_Count"
                    SelectMethod="Gallery_List"
                    TypeName="pgc.Business.General.GalleryBusiness"
                    EnableViewState="false"></asp:ObjectDataSource>
                <asp:ListView ID="lsvGallery" runat="server" DataSourceID="odsGallery"
                    EnableViewState="false" OnItemDataBound="lsvGallery_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <a class="fancybox-thumbs thumbnail gallery" data-fancybox-group="<%#Eval("ID") %>" title="<%#Eval("Title") %>" href="<%#ResolveClientUrl(Eval("GalleryThumbImagePath").ToString()) %>?width=700">
                                <img class="width100" alt="<%#Eval("Title") %>" src="<%#ResolveClientUrl(Eval("GalleryThumbImagePath").ToString()) %>?width=300" />
                                <div class="gallery-details">
                                    <span class="gallry-name"><%#Eval("Title") %></span>
                                    <%--<span class="badge badge-danger pic-number"><%#(Eval("GalleryPics") as System.Data.Objects.DataClasses.EntityCollection<GalleryPic>).Count %></span>--%>
                                    <span class="badge badge-danger pic-number"><%#Convert.ToInt32(Eval("Count"))+1 %></span>
                                </div>
                            </a>
                            <div class="hidden">
                                <asp:ListView runat="server" ID="lsvPics">
                                    <ItemTemplate>
                                        <a class="fancybox-thumbs" data-fancybox-group="<%#((GalleryPic)Container.DataItem).Gallery_ID%>" title="<%#((GalleryPic)Container.DataItem).Description%>" href="<%#ResolveClientUrl(((GalleryPic)Container.DataItem).ImagePath)%>?width=700">
                                            <img class="width100"  alt="<%#((GalleryPic)Container.DataItem).Gallery.Title%>" src="<%#ResolveClientUrl(((GalleryPic)Container.DataItem).ImagePath)%>?width=300" />
                                        </a>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="clear"></div>
                        </div>

                    </ItemTemplate>
                </asp:ListView>
            </div>


            <%if (dprGallery.TotalRowCount > dprGallery.MaximumRows)
              {%>
                <div class="pagination">
                    <asp:DataPager ID="dprGallery" runat="server" PagedControlID="lsvGallery" PageSize="9" QueryStringField="page">
                        <Fields>
                            <asp:NextPreviousPagerField PreviousPageText="صفحه قبلی" ButtonCssClass="button prev" ShowNextPageButton="false" />
                            <asp:TemplatePagerField>
                                <PagerTemplate><span class="pages"></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NumericPagerField ButtonCount="6" NumericButtonCssClass="page" NextPreviousButtonCssClass="page" CurrentPageLabelCssClass="current" />
                            <asp:TemplatePagerField>
                                <PagerTemplate></span></PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NextPreviousPagerField NextPageText=" صفحه بعدی" ButtonCssClass="button next" ShowPreviousPageButton="false" />
                        </Fields>
                    </asp:DataPager>
                </div>
            <%} %>
        </div>
    </section>
    <%-- END CONTENT --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/lib/jquery.mousewheel-3.0.6.pack.js"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/jquery.fancybox.js?v=2.1.5"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>
    <script src="/assets/global/plugins/fancyapps-fancyBox-18d1712/source/helpers/jquery.fancybox-media.js?v=1.0.6"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.fancybox-thumbs').fancybox({
                prevEffect: 'none',
                nextEffect: 'none',
                closeBtn: true,
                nextClick: true,

                helpers: {
                    thumbs: {
                        width: 75,
                        height: 50
                    },
                    title: {
                        type: 'inside',
                        position: 'bottom',
                    },
                },
                afterLoad: function () {
                    this.title = '' + (this.index + 1) + ' از ' + this.group.length + (this.title ? ' - ' + this.title : '');
                },
            });
        })
    </script>
</asp:Content>

