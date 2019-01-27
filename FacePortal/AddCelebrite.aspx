<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddCelebrite.aspx.cs" Inherits="FacePortal.AddCelebrite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Wybierz zdjęcie:
    <asp:FileUpload id="FileUploadControl" runat="server" />
     <br />
    <br />
    Name:<br />
    <asp:TextBox ID="name" runat="server"></asp:TextBox>
    <br />
    <br />
    Surname:<br />
    <asp:TextBox ID="surname" runat="server"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Button runat="server" id="UploadButton" text="Wykonaj" onclick="UploadButton_Click" />
</asp:Content>
