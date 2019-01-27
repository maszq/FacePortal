<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="FacePortal.MyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <link rel="stylesheet" runat="server" media="screen" href="/css/DatabaseTables.css" />

     <div class="table1">
        <asp:PlaceHolder ID = "PlaceHolder1" runat="server" /><br />
     </div>
    <div>
        <asp:Button ID="form_data" class="mySuperClass" onClick="Form_Data" runat="server" Text="Zmień dane" />
        <asp:Button ID="edit_password" class="mySuperClass" onClick="Edit_Password" runat="server" Text="Zmień hasło" />
    </div>
     <div class="container" ID="password_form" visible="false" runat="server" align="center">
        <div class="card card-container">
           <p id="profile-name" class="profile-name-card"></p>

              Wprowadź bieżące hasło:
              <br /><input type="password" id="password" class="form-control" runat="server"/><br />
              Wprowadź nowe hasło:
              <br /><input type="password" id="new_password" class="form-control" runat="server"/> <br />
              
              <asp:Button CssClass="btn btn-rg btn-register" id="change" type="submit" OnClick="Edit_Password" runat="server" text="Zmień hasło"/>
        </div>
    </div>

    <div class="container" ID="data_form" visible="false" runat="server" align="center" >
        <div class="card card-container">
           <p id="profile-name" class="profile-name-card"></p>
                    Nickname: <input type="text" id="nickname" class="form-control"  runat="server"/></br>
                    E-mail: <input type="email" id="inputEmail" class="form-control" runat="server"/></br>
                    Imię: <input type="text" id="name" class="form-control" runat="server"/></br>
                    Nazwisko: <input type="text" id="surname" class="form-control" runat="server"/></br>
                   
                    Jeżeli nie chcesz zmieniać poniższych pól, pozostaw je niewypełnione <br />
                  
                    Płeć:<br />
                    <input type="radio" id="male" name="userSex" runat="server"/>Mężczyzna<br />
                    <input type="radio" id="female" name="userSex" runat="server"/>Kobieta<br />
              
              <asp:Button CssClass="btn btn-rg btn-register" id="Button1" type="submit" OnClick="Edit_Data" runat="server" text="Potwierdź zmiany"/>
        </div>
    </div>

</asp:Content>
