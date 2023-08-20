<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaEventosA.aspx.cs" Inherits="ObligatorioProg3_Estadio.ListaEventosA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Panel ID="Panel1" runat="server" Height="277px" HorizontalAlign="Center" CssClass="Panel" Width="724px" style="margin-left: 20%">
            <br />
            <asp:Label ID="Label1" runat="server" Text="Buscar por nombre"></asp:Label>
            <br />
        <asp:TextBox ID="TxtBuscarEvento" runat="server" Width="375px" CssClass="textBox"></asp:TextBox>
            <br />
            <br />
        <div>
            <asp:Label ID="Label2" runat="server" Text="Desde"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Hasta"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="TxtFechaDesde" runat="server" TextMode="Date" Width="147px" CssClass="textBox"></asp:TextBox>
            <asp:TextBox ID="TxtFechaHasta" runat="server" TextMode="Date" Width="149px" CssClass="textBox"></asp:TextBox>
        </div>
            <br />
            <asp:Label ID="LblAviso" runat="server" ForeColor="Red" CssClass="mensaje"></asp:Label>
            <br />
        <asp:Button ID="BtnFiltro" runat="server" OnClick="BtnFiltro_Click" Text="Filtrar" class="btn btn-primary" />
        <asp:Button ID="BtnBorrarFiltro" runat="server" OnClick="BtnBorrarFiltro_Click" Text="Borrar Filtro" Visible="False" class="btn btn-danger" />
        <asp:Button ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click" Text="Agregar" class="btn btn-success" />
            <br />
            <br />
    </asp:Panel>
        <br />
    <br />
    <br />
    <br />
    <asp:GridView ID="GridEvento" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridEvento_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="id" HeaderText="idevento" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="nombre" HeaderText="Evento" ReadOnly="True" />
            <asp:BoundField DataField="fechainicio" HeaderText="Fecha Inicio" ReadOnly="True" />
            <asp:BoundField DataField="fechafin" HeaderText="Fecha Fin" ReadOnly="True" />
            <asp:BoundField DataField="promocion" HeaderText="Promoción" ReadOnly="True" />
            <asp:BoundField DataField="disponibilidad" HeaderText="Disponibilidad" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" SelectText="Editar">
            <ControlStyle CssClass="blueButton" />
            </asp:CommandField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>
