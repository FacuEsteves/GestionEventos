<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ObligatorioProg3_Estadio.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="~/Styles/Login.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" BackColor="White" Height="412px" HorizontalAlign="Center" style="margin-left: 35%" Width="392px" CssClass="Panel-Login">
                <br />
            <asp:Image ID="Image1" runat="server" Height="102px" ImageAlign="Top" ImageUrl="~/Images/EstadioUruguayLogo.png" Width="301px" />
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Usuario" Font-Bold="True"></asp:Label>
                <br />
                <asp:TextBox ID="txtUser" runat="server" Height="23px" Width="304px" CssClass="textBox"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Contraseña" Font-Bold="True"></asp:Label>
                <br />
                <asp:TextBox ID="txtPass" runat="server" Height="25px" Width="303px" TextMode="Password" CssClass="textBox"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Iniciar Sesión" CssClass="button-7"/>
            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
