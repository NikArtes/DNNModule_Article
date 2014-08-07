<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Christoc.Modules.DNNModule.Settings" %>


<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->
  

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:label ID="lblSetting2" runat="server" />
            <asp:TextBox ID="TextBox1" runat="server" />
            <br />
            <asp:Button ID="ButtonAddTegs" resourcekey="ButtonAddTegs" runat="server" OnClick="ButtonAddTegs_Click" />
            <br />
            <dnn:label ID="lblSettingDelete" resourcekey="lblSettingDelete" runat="server" />

            <br />
            <asp:CheckBoxList ID="CheckBoxListDeleteTeg" runat="server" DataTextField="NameTeg" DataValueField="IdTeg">
            </asp:CheckBoxList>
            <asp:Button ID="ButtonDeleteTegs" resourcekey="ButtonDeleteTegs" runat="server" OnClick="ButtonDeleteTegs_Click" />
            <br />
            <dnn:label ID="lblSettingUpdate" resourcekey="lblSettingUpdate" runat="server" />
            <br />
            <asp:TextBox ID="txtBoxUp" resourcekey="txtBoxUp" runat="server" />
            <br />
            <asp:Button ID="ButtonUpdate" resourcekey="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" />
        </div>
    </fieldset>


