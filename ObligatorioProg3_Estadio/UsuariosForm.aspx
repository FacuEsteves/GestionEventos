<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosForm.aspx.cs" Inherits="ObligatorioProg3_Estadio.UsuariosForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" CssClass="Panel" Height="807px" Width="726px" style="margin-left:18%">
            <br />
        <asp:Label ID="Label1" runat="server" Text="Usuario:" Style="font-size: x-large; text-align: center; font-weight: 700"></asp:Label>
            <br />
        <br />
            <div>
            Documento
            <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNroDoc" runat="server" CssClass="textBox"></asp:TextBox>
&nbsp;
        <asp:Button ID="btnCompletar" runat="server" OnClick="btnCompletar_Click" Text="Autocompletar" class="btn btn-primary" />
            </div>
        <br />
        Nombre<br />
        <asp:TextBox ID="txtNombre" runat="server" CssClass="textBox"></asp:TextBox>
            <br />
        <br />
        Apellido<br />
        <asp:TextBox ID="txtApellido" runat="server" CssClass="textBox"></asp:TextBox>
            <br />
        <br />
        Correo<br />
        <asp:TextBox ID="txtCorreo" runat="server" CssClass="textBox"></asp:TextBox>
            <br />
        <br />
        Usuario<br />
        <asp:TextBox ID="txtUser" runat="server" CssClass="textBox"></asp:TextBox>
            <br />
        <br />
        Contraseña<br />
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="textBox"></asp:TextBox>
            <br />
        <br />
                <asp:CheckBox ID="CheckBoxAdmin" runat="server" Text="Administrador" />
        <br />
        <br />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="btn btn-success"/>
            &nbsp;<asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" class="btn btn-warning" OnClick="btnLimpiar_Click"/>
        <br />
        <asp:Label ID="mensaje" runat="server"></asp:Label>
        <br />
        </asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" CssClass="Panel" HorizontalAlign="Center" Width="974px" style="margin-left:8%">
            <br />
            <asp:Label ID="Label2" runat="server" Text="Lista Usuarios:" Style="font-size: x-large; text-align: center; font-weight: 700"></asp:Label>
            <br />
            <br />
        <asp:TextBox ID="buscarTxt" runat="server" CssClass="textBox"></asp:TextBox>
        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" class="btn btn-primary"/>
&nbsp;
            <div class="select" style="margin-right:30px">
                <asp:DropDownList ID="ddFiltroRol" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddFiltroRol_SelectedIndexChanged">
                    <asp:ListItem>Todos</asp:ListItem>
                    <asp:ListItem Value="1">Administrador</asp:ListItem>
                    <asp:ListItem Value="0">Vendedor</asp:ListItem>
                </asp:DropDownList>
                <div class="select_arrow">
                </div>
            </div>
&nbsp;<br />
            <br />
        <asp:Label ID="mensajeBusqueda" runat="server" ForeColor="Red"></asp:Label>
            <br />
        <br />
        <asp:GridView ID="gridUsers" runat="server" OnRowDeleting="gridUsers_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button">
                <ControlStyle CssClass="redButton" />
                </asp:CommandField>
                <asp:BoundField DataField="nrodoc" HeaderText="Documento" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="nombreUsuario" HeaderText="Usuario" />
                <asp:BoundField DataField="correo" HeaderText="Correo" />
                <asp:BoundField DataField="rol" HeaderText="Rol" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
