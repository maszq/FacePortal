<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Raport.aspx.cs" Inherits="FacePortal.Raport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      <link rel="stylesheet" runat="server" media="screen" href="/css/DatabaseTables.css" />

    WYNIKI PORÓWNANIA<br />

    <div class="table1">
        <asp:PlaceHolder ID = "PlaceHolder" runat="server" /><br />
    </div>

    <br />
    <br />
      <br />
      &nbsp; &nbsp;<asp:Label ID="Label2" runat="server" Text="Włosy" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
      <asp:Label ID="wlosy" runat="server" Text="Label" Visible="False"></asp:Label>

      &nbsp;&nbsp;&nbsp;
      <br />
      &nbsp;&nbsp;
      <asp:Label ID="Label3" runat="server" Text="Czoło wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="czolo_wys" runat="server" Text="Label" Visible="False"></asp:Label>

      <br />
&nbsp;&nbsp;
      <asp:Label ID="Label4" runat="server" Text="Czoło szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="czolo_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label5" runat="server" Text="Oko lewe szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="oko_lewe_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label6" runat="server" Text="Oko lewe wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="oko_lewe_wys" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label7" runat="server" Text="Oko prawe szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="oko_prawe_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label8" runat="server" Text="Oko prawe wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="oko_prawe_wys" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label9" runat="server" Text="Twarz szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="twarz_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label10" runat="server" Text="Twarz wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="twarz_wys" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label11" runat="server" Text="Odl. oczu" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="odl_oczy" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label12" runat="server" Text="Polik lewy szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="polik_lewy_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label13" runat="server" Text="Polik prawy szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="polik_prawy_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp;
      <asp:Label ID="Label14" runat="server" Text="Nos wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="nos_wys" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label15" runat="server" Text="Broda szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="broda_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp; 
      <asp:Label ID="Label16" runat="server" Text="Broda wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="broda_wys" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp;&nbsp;
      <asp:Label ID="Label17" runat="server" Text="Usta szerokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="usta_szer" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp;&nbsp; 
      <asp:Label ID="Label18" runat="server" Text="Usta wysokość" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="usta_wys" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
&nbsp;&nbsp;&nbsp; 
      <asp:Label ID="Label19" runat="server" Text="Ogólny wynik" Visible="False"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="result" runat="server" visibile="false" Text="Label" Visible="False"></asp:Label>
      <br />
      <br />

    <asp:Button ID="generateDoc" runat="server" Visibile="false" Text="Generuj PDF" OnClick="generateDoc_Click" Visible="False" />

      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />
      <br />

</asp:Content>
