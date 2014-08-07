<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleView.ascx.cs" Inherits="Christoc.Modules.DNNModule.Controls.ArticleView" %>
<fieldset>
    <div class="dnnFormItem">
<asp:Panel ID="plArticleTitle" Style="color:brown; font-family: 'Times New Roman'; font-size: 18pt" runat="server" CssClass="StyleModule">
</asp:Panel>
<asp:Panel ID="plArticleBody" Style="color: black; font-family: 'Times New Roman'; font-size: 14pt; margin-left: 15px" runat="server" CssClass="Normal ArticleBody">
</asp:Panel>
<div class="ArticleAdmin" runat="server" id="ArticleAdmin">
    <asp:LinkButton ID="lnkEdit" runat="server" ResourceKey="lnkEdit" OnClick="lnkEdit_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkDelete" runat="server" ResourceKey="lnkDelete" OnClick="lnkDelete_Click"></asp:LinkButton>
    <br />
    <br />
    

    <asp:Panel ID="Panel1" runat="server">
        <asp:Repeater ID="rptTegsOnArticle" OnItemCommand="rptTegsOnArticle_ItemCommand" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="lnkTeg" runat="server"  Text='<%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "NameTeg")) %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
<br />
<asp:TextBox ID="TextBoxComment" runat="server" Height="18px" Width="492px"></asp:TextBox>
<asp:Button ID="ButtonEditComment" resourcekey="ButtonEditComment" runat="server" Height="35px" style="margin-top: 0px; margin-bottom: 12px" OnClick="ButtonEditComment_Click" />
</div>



<p>
    &nbsp;<asp:Repeater ID="rptArticleComments" runat="server" OnItemDataBound="rptArticleComments_ItemDataBound">
        <ItemTemplate>
            <asp:Panel ID="pnlCommentImage" runat="server" Style="color: black; font-family: 'Times New Roman'; font-size: 14pt">
                <asp:Image ID="ImageComment" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImgNameUrl") %>' runat="server" Height="40px" style="margin-top: 0px" Width="40px" />
                <%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "NameAutor").ToString()) %>
            </asp:Panel>
            <asp:panel ID="pnlComment" runat="server"  Style="color: black; font-family: 'Times New Roman'; font-size: 14pt">
                <%# HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "BodyComment").ToString()) %>
            </asp:panel> 
        </ItemTemplate>
    </asp:Repeater>
</p>
        </div>
    </fieldset>





