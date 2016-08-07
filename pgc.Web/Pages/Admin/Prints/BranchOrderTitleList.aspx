<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchOrderTitleList.aspx.cs" Inherits="Pages_Admin_Prints_BranchOrderTitleList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست کالاهای سفارش شده</title>
    <link href="<%=ResolveClientUrl("~/Styles/Shared/PrintPage.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveClientUrl("~/scripts/shared/jquery-1.7.2.min.js")%>" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(new function () {
            self.print();
        });
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                    <table border="0px" class="lsttbl">
                        <tr>
                            <td style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutTitleHtml) %></td>
                        </tr>
                    </table>
              <%try
                {
                    BranchOrderTitlePattern pattern = (BranchOrderTitlePattern)Session["BranchOrderTitlePrintPattern"];
                    BranchOrderTitleBusiness business = new BranchOrderTitleBusiness();
                    IQueryable<BranchOrderTitle> OrdersList = business.Search_SelectPrint(pattern);
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Group_ID > 0 ||
                          pattern.Price.Type != RangeType.Nothing ||
                          string.IsNullOrEmpty(pattern.Title) || 
                          BasePattern.IsEnumAssigned(pattern.Status)) {%>
                                <table border="0px" class="lsttbl">
                                    <tr class="thead" style="text-align:center;">
                                        <th colspan="2">نتایج جستجو بر روی فیلد های ذیل در لیست کالاها</th>
                                    </tr>
                                    <tr>
                                        <%if (!string.IsNullOrEmpty(pattern.Title)){ %>
                                            <td class="caption"style="border:none">عبارت : <%=pattern.Title %></td>
                                        <%} %>                            
                                    
                                        <%if (pattern.Group_ID>0){ %>
                                            <td class="caption"style="border:none">عنوان گروه کالای سفارشی : <%=new BranchOrderTitleGroupBusiness().Retrieve(pattern.Group_ID).Title %></td>
                                        <%} %>
                                    </tr>
                                    <tr>
                                        <%if (BasePattern.IsEnumAssigned(pattern.Status)){ %>
                                            <td class="caption"style="border:none">وضعیت کالا : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Status) %></td>
                                        <%} %>
                                        
                                        <%if (pattern.Price.Type !=RangeType.Nothing){ %>                  
                                            <td class="caption"style="border:none">مبلغ : 
                                                <%= EnumUtil.GetEnumElementPersianTitle((RangeType)pattern.Price.Type) %>
                                                <%= pattern.Price.HasFirstNumber ? pattern.Price.FirstNumber.ToString() : "" %>
                                                <%= pattern.Price.HasSecondNumber ? "و" + pattern.Price.SecondNumber.ToString() : ""%>
                                            </td>
                                        <%} %>
                                    </tr>
                                </table>
                    <%}else{ %>
                        <table border="0px" class="lsttbl">
                            <tr class="thead" style="text-align:center;">
                                <th>لیست کامل کالاها/th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchOrderTitle item in OrdersList.OrderBy(f=>f.BranchOrderTitleGroup.DisplayOrder).ThenBy(f=>f.DisplayOrder))
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.Price;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام کالا</th>
                                    <th>نام گروه</th>
                                    <th>اولویت نمایش</th>
                                    <th>مبلغ واحد</th>
                                    <th>وضعیت</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام کالا</th>
                                    <th>نام گروه</th>
                                    <th>اولویت نمایش</th>
                                    <th>مبلغ واحد</th>
                                    <th>وضعیت</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.Title%></td>
                            <td><%=item.BranchOrderTitleGroup.Title %></td>
                            <td><%=item.DisplayOrder %></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.Price)%> ریال</td>
                            <td><%=EnumUtil.GetEnumElementPersianTitle((BranchOrderTitleStatus)item.Status)%></td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                           <%--     <tr class="ftr">
                                    <td colspan="5">جمع کل: </td>
                                    <td><%=UIUtil.GetCommaSeparatedOf(TotalAmount)%> ریال</td>
                                </tr>--%>
                            </table>
                        <%}
                    else if (RowNumber == RowPerPage)
                    {
                        RowNumber = 0; %></table><%}    
                    }
                    
                    if (!string.IsNullOrEmpty(kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutFooterHtml))){%>
                    <table border="0px" class="lsttbl">
                        <tr>
                            <td style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutFooterHtml) %></td>
                        </tr>
                    </table>
                    <%}
                }
                catch (Exception)
                {
                    Response.Redirect(GetRouteUrl("admin-branchorderedtitle",null));
                }%>
        </div>
    </form>
</body>
</html>
