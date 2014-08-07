<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Christoc.Modules.DNNModule.Edit" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<fieldset>
    <div class="dnnFormItem">
<dl>
    <dt>
        <dnn:label ID="lblTitle" ControlName="txtTitle" runat="server" ResourceKey="txtTitle" Style="color:black; font-family: 'Times New Roman'; font-size: 14pt"/>
    <dd>
        <asp:TextBox ID="txtTitle" runat="server" Columns="50" /><asp:RequiredFieldValidator
            ID="rfvTitle" runat="server" ControlToValidate="txtTitle" CssClass="NormalRed" />
    </dd>
    <dt>
        <dnn:label ID="lblDescription" ControlName="txtDescription" runat="server" ResourceKey="txtDescription" Style="color:black; font-family: 'Times New Roman'; font-size: 14pt"/>
    <dd>
        <dnn:TextEditor ID="txtDescription" runat="server" TextMode="MultiLine" Width="600px" Height="400px" />
        <%--<asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
            CssClass="NormalRed" />--%>
    </dd>
    <dt>
        <dnn:label ID="lblBody" ControlName="txtBody" runat="server" ResourceKey="txtBody" Style="color:black; font-family: 'Times New Roman'; font-size: 14pt"/>
    <dd>
        <dnn:TextEditor ID="txtBody" runat="server" Width="600px" Height="400px" />
    </dd>
    <dt>
        <dnn:label ID="lblTerms" runat="server" ControlName="tsTerms" />
    <dt>
        <asp:CheckBoxList ID="CheckBoxListTag" runat="server" DataTextField="NameTeg" DataValueField="IdTeg">
        </asp:CheckBoxList>
        
        <br />
    </dt>
    <dd>
        <asp:LinkButton ID="lbSave" runat="server" resourcekey="lbSave" OnClick="lbSave_Click" />
        <asp:LinkButton ID="lbCancel" runat="server" resourcekey="lbCancel" OnClick="lbCancel_Click"
            CausesValidation="false" />
    </dd>
</dl>
        </div>
    </fieldset>
