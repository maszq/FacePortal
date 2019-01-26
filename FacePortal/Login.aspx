<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FacePortal.Login1" %>

<!DOCTYPE html>
<link rel="stylesheet" runat="server" media="screen" href="/css/Login.css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="container">
        <div class="card card-container">
            <img id="profile-img" class="profile-img-card" src="//ssl.gstatic.com/accounts/ui/avatar_2x.png" />
            <p id="profile-name" class="profile-name-card"></p>
                <form class="form-signin" runat="server">
                    <span id="reauth-email" class="reauth-email"></span>
                    <input type="email" id="inputEmail" class="form-control" placeholder="Email" required="required" autofocus="autofocus" runat="server" />
                    <input type="password" id="inputPassword" class="form-control" placeholder="Hasło" required="required" runat="server"/>
                    <div id="remember" class="checkbox">
                        <label>
                            <input type="checkbox" value="remember-me" /> Remember me
                        </label>
                    </div>
                    <button class="btn btn-lg btn-primary btn-block btn-signin" runat="server" type="submit">Zaloguj się</button>
                </form>
                <a href="#" class="forgot-password">Zapomniałeś hasła?</a>
        </div>
    </div>
</body>
</html>
