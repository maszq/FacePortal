<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CelebriteList.aspx.cs" Inherits="FacePortal.CelebriteList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="/css/DatabaseTables.css" />

     <div class="table1">
        <asp:PlaceHolder ID = "PlaceHolder" runat="server" /><br />
    </div>
    <asp:Button ID="addCelebrite" runat="server" Visible="false" Text="Dodaj Celebryte" OnClick="addCelebrite_Click" />
    </asp:Content>
