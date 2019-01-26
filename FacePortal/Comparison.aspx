<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Comparison.aspx.cs" Inherits="FacePortal.Comparison" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <asp:FileUpload id="FileUploadControl" runat="server" />
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="350px" style="margin-left: 0px" Width="318px">
        <asp:CheckBox ID="włosy" runat="server" Height="55px" style="margin-left: 155px" Width="98px" />
        <asp:CheckBox ID="czoło" runat="server" Height="40px" style="margin-left: 159px" />
        <br />
        <asp:CheckBox ID="lewaBrew" runat="server" Height="28px" style="margin-left: 88px" Width="83px" />
        <asp:CheckBox ID="prawaBrew" runat="server" Height="30px" style="margin-left: 46px" Width="89px" />
        <br />
        <asp:CheckBox ID="leweUcho" runat="server" Height="57px" style="margin-left: 47px" Width="44px" />
        <asp:CheckBox ID="leweOko" runat="server" Height="58px" style="margin-left: 0px" Width="46px" />
        <asp:CheckBox ID="praweOko" runat="server" Height="56px" style="margin-left: 75px; margin-top: 0px" Width="39px" />
        <asp:CheckBox ID="praweUcho" runat="server" Height="58px" style="margin-left: 7px" Width="32px" />
        <br />
        <asp:CheckBox ID="lewyPoliczek" runat="server" Height="42px" style="margin-left: 95px" Width="44px" />
        <asp:CheckBox ID="nos" runat="server" Height="42px" style="margin-left: 12px" Width="42px" />
        <asp:CheckBox ID="prawyPoliczek" runat="server" Height="45px" style="margin-left: 26px" Width="55px" />
        <br />
        <asp:CheckBox ID="usta" runat="server" Height="38px" style="margin-left: 159px" Width="76px" />
        <br />
        <asp:CheckBox ID="broda" runat="server" Height="20px" style="margin-left: 156px" Width="80px" />
        <br />
    
    </asp:Panel>
    <asp:CheckBox ID="all" runat="server" Text="cała twarz" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />
    <br />
    <br />
    <asp:Label runat="server" id="StatusLabel" text="Upload status: " />

    
</asp:Content>
