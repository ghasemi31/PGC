<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Template-List.aspx.cs" Inherits="Pages_Guest_Template_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title>نمونه صفحات لیست محور</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

<div class="list-container">

    <!-- آیتم نمونه در لیست -->
    <div class="listItemHolder-singlerow">
        <%--<div class="fontClass listItemHolder_text">
            شعبه ی گاندی ، اولین شعبه ی رستوران های زنجیره ای مستردیزی است. این شعبه در حال حاضر با مدیریت آقای رضایی ادره می شود و همچنان شعبه ی نمونه از دید مشتریان می باشد.<br />
            از کلیه ساکنین مناطق پونک و ولیعصر تقاضا می شود با توجه به راه اندازی شعب مستردیزی در این مناطق از این پس جهت تسریع در امر ارسال دیزی، با این شعب تماس بگیرند.
        </div>
        <div class="postImage">
            <a href="#"><img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listmehvar/img2.png")%>"  alt="توصیح لینک هر آیتم" /></a>
        </div>
        <div class="gotoPost">
            <a href="#">
                <img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listgoitem.png")%>"  alt="توصیح لینک هر آیتم" />
            </a>
        </div>--%>
        <div class="listItemHolderTitleHolder">
          <div class="BMitra listItemHolderTitle">
            <a href="#">نمونه خبر تک سطری</a>
          </div>
        </div>
    </div>


    <!-- آیتم نمونه در لیست -->
    <div class="listItemHolder-singledata">
        <div class="fontClass listItemHolder_text">
            شعبه ی گاندی ، اولین شعبه ی رستوران های زنجیره ای مستردیزی است. این شعبه در حال حاضر با مدیریت آقای رضایی ادره می شود و همچنان شعبه ی نمونه از دید مشتریان می باشد.<br />
            از کلیه ساکنین مناطق پونک و ولیعصر تقاضا می شود با توجه به راه اندازی شعب مستردیزی در این مناطق از این پس جهت تسریع در امر ارسال دیزی، با این شعب تماس بگیرند.
        </div>
        <%--<div class="postImage">
            <a href="#"><img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listmehvar/img2.png")%>"  alt="توصیح لینک هر آیتم" /></a>
        </div>--%>
        <div class="gotoPost">
            <a href="#">
                <img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listgoitem.png")%>"  alt="توصیح لینک هر آیتم" />
            </a>
        </div>
        <div class="listItemHolderTitleHolder">
          <div class="BMitra listItemHolderTitle">
            <a href="#">نمونه خبر بدون عکس</a>
          </div>
        </div>
    </div>

    <!-- آیتم نمونه در لیست -->
    <div class="listItemHolder">
        <div class="fontClass listItemHolder_text">
            شعبه ی گاندی ، اولین شعبه ی رستوران های زنجیره ای مستردیزی است. این شعبه در حال حاضر با مدیریت آقای رضایی ادره می شود و همچنان شعبه ی نمونه از دید مشتریان می باشد.<br />
            از کلیه ساکنین مناطق پونک و ولیعصر تقاضا می شود با توجه به راه اندازی شعب مستردیزی در این مناطق از این پس جهت تسریع در امر ارسال دیزی، با این شعب تماس بگیرند.
        </div>
        <div class="postImage"></div>
        <div class="gotoPost">
            <a href="#">
                <img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listgoitem.png")%>"  alt="توصیح لینک هر آیتم" />
            </a>
        </div>
        <div class="listItemHolderTitleHolder">
          <div class="BMitra listItemHolderTitle">
            <a href="#">مردي به خاطر آنکه همسرش در طبخ آبگوشت مهارت ندارد تصميم گرفت او را طلاق دهد.</a>
          </div>
        </div>
    </div>

    <!-- آیتم نمونه در لیست -->
    <div class="listItemHolder">
        <div class="fontClass listItemHolder_text">
            شعبه ی گاندی ، اولین شعبه ی رستوران های زنجیره ای مستردیزی است. این شعبه در حال حاضر با مدیریت آقای رضایی ادره می شود و همچنان شعبه ی نمونه از دید مشتریان می باشد.<br />
            از کلیه ساکنین مناطق پونک و ولیعصر تقاضا می شود با توجه به راه اندازی شعب مستردیزی در این مناطق از این پس جهت تسریع در امر ارسال دیزی، با این شعب تماس بگیرند.
        </div>
        <div class="postImage"></div>
        <div class="gotoPost">
            <a href="#">
                <img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listgoitem.png")%>"  alt="توصیح لینک هر آیتم" />
            </a>
        </div>
        <div class="listItemHolderTitleHolder">
          <div class="BMitra listItemHolderTitle">
            <a href="#">مردي به خاطر آنکه همسرش در طبخ آبگوشت مهارت ندارد تصميم گرفت او را طلاق دهد.</a>
          </div>
        </div>
    </div>

    <!-- Pager -->
    <div class="fontClass pagination">صفحات دیگر:  
        <a href="#">1</a>
        <a href="#">2</a>
    </div>
</div>

</asp:Content>

