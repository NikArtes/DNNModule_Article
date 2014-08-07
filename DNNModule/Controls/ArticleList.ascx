<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.ascx.cs" Inherits="Christoc.Modules.DNNModule.Controls.ArticleList" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<fieldset>
    <div class="dnnFormItem">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;<table class="auto-style1">
    <tr>
        <td class="auto-style3" colspan="2">
            <asp:Label ID="Label1" resourcekey="Label1" runat="server" Style="font-family:'Times New Roman'; color:black; font-size:14pt"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style2" colspan="2">
            <asp:Button ID="Button2" resourcekey="button2" runat="server" OnClick="Button2_Click" />
            <asp:Button ID="Button3" resourcekey="button3" runat="server" OnClick="Button3_Click" />
            <asp:Button ID="Button4" resourcekey="button4" runat="server" OnClick="Button4_Click" />
            <asp:Button ID="Button5" resourcekey="button5" runat="server" OnClick="Button5_Click" />
        </td>
    </tr>
    <tr>
        <td class="auto-style5"><asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" ></asp:Calendar>
        </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Image ID="Image1" ImageUrl="~/DesktopModules/DNNModule/Sourse/search.png" runat="server" Height="26px" style="margin-top: 0px" Width="25px" />
&nbsp;&nbsp;
<asp:TextBox ID="TextBox" runat="server" Width="216px" Height="16px" style="margin-top: 4px; margin-bottom: 8px;"></asp:TextBox>
<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" resourcekey="Button1" Font-Size="Small" Height="39px" Width="86px"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="auto-style2" colspan="2">
<asp:Repeater ID="rptArticleList" runat="server" OnItemCommand="RptArticleListOnItemCommand" OnItemDataBound="RptArticleListOnItemDataBound">
    <ItemTemplate>
        <asp:Panel ID="Panel1" CssClass="ArticleWrapper" runat="server">
            <asp:Panel runat="server" ID="pnlArticleTitle" Style="font-family: 'Times New Roman';  font-size: 18pt; margin-top: 10px" CssClass="HTitle">
                <asp:HyperLink ID="lnkArticle" runat="server"  CssClass="HTitle" NavigateUrl='<%# GetArticleLink(DataBinder.Eval(Container.DataItem,"ArticleId").ToString())%>'><%# System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem,"Title").ToString()) %></asp:HyperLink>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlArticleDescription" Style="color: black; font-family: 'Times New Roman'; font-size: 14pt; margin-left: 10px" CssClass="Normal ArticleDescription">
                <%# HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem, "Description").ToString())%>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAdminControls" CssClass="Normal ArticleAdmin" Visible="false">
                <asp:LinkButton ID="lnkEdit" runat="server" ResourceKey="EditArticle.Text" CommandName="Edit"
                    Visible="false" Enabled="false" />
                <asp:LinkButton ID="lnkDelete" runat="server" ResourceKey="DeleteArticle.Text" CommandName="Delete"
                    Visible="false" Enabled="false" />
            </asp:Panel>
        </asp:Panel>
    </ItemTemplate>
</asp:Repeater>
            <br />
<asp:Panel ID="pnlPaging" runat="server">
    <asp:HyperLink ID="lnkPrevious" runat="server" resourcekey="lnkPrevious" Visible="false"
        CssClass="lnkPrevious"></asp:HyperLink>
    <asp:HyperLink ID="lnkNext" runat="server" resourcekey="lnkNext" Visible="false"
        CssClass="lnkNext"></asp:HyperLink>
</asp:Panel>

        </td>
    </tr>
</table>
&nbsp;
        </div>
    </fieldset>


