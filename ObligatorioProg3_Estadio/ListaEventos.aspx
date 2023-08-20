<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaEventos.aspx.cs" Inherits="ObligatorioProg3_Estadio.ListaEventos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link rel="stylesheet" href="~/Styles/General.css"/>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Panel ID="Panel1" runat="server" CssClass="Panel" Width="810px" HorizontalAlign="Center" style="margin-left:18%">
                <br />
            <asp:Label ID="LabelTitulo" runat="server" Text="Lista de Eventos" style="font-size: x-large; text-align: center; font-weight: 700"></asp:Label>
                :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" CssClass="redButton" OnClick="btnClose_Click" Text="Cerrar Sesión" />
                <br />
            <br />
                <br />
                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
            <asp:TextBox ID="TxtBuscarEvento" runat="server" CssClass="textBox"></asp:TextBox>
            <asp:TextBox ID="TxtFechaDesde" runat="server" TextMode="Date" CssClass="textBox"></asp:TextBox>
            <asp:TextBox ID="TxtFechaHasta" runat="server" TextMode="Date" CssClass="textBox"></asp:TextBox>
            <asp:Button ID="BtnFiltro" runat="server" OnClick="BtnFiltro_Click" Text="Filtrar" CssClass="blueButton"/>
            <asp:Button ID="BtnBorrarFiltro" runat="server" OnClick="BtnBorrarFiltro_Click" Text="Borrar Filtro" Visible="False" class="defaultButton" CssClass="yellowButton"/>
                    <br />
                    <br />
                    <asp:Label ID="LblAviso" runat="server"></asp:Label>
                    <br />
                </asp:Panel>
                <br />
            </asp:Panel>
            <br />
            <br />
            <asp:GridView ID="GridEvento" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridEvento_SelectedIndexChanged" HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="idevento" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="nombre" HeaderText="Evento" ReadOnly="True" />
                    <asp:BoundField DataField="fechainicio" HeaderText="Fecha Inicio" ReadOnly="True" />
                    <asp:BoundField DataField="fechafin" HeaderText="Fecha Fin" ReadOnly="True" />
                    <asp:BoundField DataField="promocion" HeaderText="Promoción" ReadOnly="True" />
                    <asp:BoundField DataField="disponibilidad" HeaderText="Disponibilidad" />
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" >
                    <ControlStyle CssClass="yellowButton" />
                    </asp:CommandField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
