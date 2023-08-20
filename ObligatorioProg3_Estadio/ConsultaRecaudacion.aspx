<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaRecaudacion.aspx.cs" Inherits="ObligatorioProg3_Estadio.ConsultaRecaudacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
        <asp:Panel ID="Panel1" runat="server" CssClass="Panel" HorizontalAlign="Center" Width="1136px" style="margin-left:1%; padding:1%">
            <br />
        <asp:Label ID="LabelTitulo" runat="server" Text="Lista de Eventos por fecha: " style="font-size: x-large; text-align: center; font-weight: 700"></asp:Label>
            <br />
        <div class="select" style="margin-right:30px">
        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="textBox"></asp:TextBox>
            <asp:DropDownList ID="ddEvento" runat="server">
                <asp:ListItem Value="dia">Dia </asp:ListItem>
                <asp:ListItem Value="mes">Mes</asp:ListItem>
                <asp:ListItem Value="año">Año</asp:ListItem>
            </asp:DropDownList>
            <div class="select_arrow">
            </div>
        </div>
            <br />
        <asp:Button ID="btnFiltroFecha" runat="server" OnClick="btnFiltroFecha_Click" Text="Filtrar Por Fecha" class="btn btn-primary"/>
        <asp:Button ID="btnDescarga" runat="server" OnClick="btnDescarga_Click" Text="Descargar PDF" class="btn btn-warning"/>
        <br />
        <br />
        <asp:Label ID="mensaje2" runat="server" ForeColor="Red"></asp:Label>
            <br />
        <br />
        <asp:GridView ID="gridEventosFecha" runat="server" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        </asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" CssClass="Panel" HorizontalAlign="Center" Width="1136px" style="margin-left:1%; padding:1%" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
            <br />
        <asp:Label ID="Label1" runat="server" Text="Recaudación Evento" style="font-size: x-large; text-align: center; font-weight: 700"></asp:Label>
        :<br />
        <br />
        <asp:TextBox ID="txtIdevento" runat="server" TextMode="Number" CssClass="textBox"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar por ID" class="btn btn-primary"/>
            <br />
            &nbsp;<br /> Desde:<asp:TextBox ID="fInicio" runat="server" TextMode="Date" CssClass="textBox"></asp:TextBox>
            &nbsp;Hasta:
        <asp:TextBox ID="fFin" runat="server" TextMode="Date" CssClass="textBox"></asp:TextBox>
&nbsp;<asp:Button ID="btnFecha" runat="server" OnClick="btnFecha_Click" Text="Buscar por Fecha" class="btn btn-primary"/>
            <br />
            <br />
        <asp:Button ID="btnPdf" runat="server" OnClick="btnPdf_Click" Text="Descargar PDF" class="btn btn-warning"/>
        <br />
        <br />
        <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblResumen" runat="server" Text="Resumén:"></asp:Label>
        <br />
        <asp:GridView ID="gridResumen" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="vendidas" HeaderText="Vendidas" />
                <asp:BoundField DataField="recaudacion" HeaderText="Recaudación" />
                <asp:BoundField DataField="lugaresLibres" HeaderText="Libres" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        <asp:Label ID="lblDetalle" runat="server" Text="Detalle:"></asp:Label>
        <asp:GridView ID="gridRecaudacion" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="evento" HeaderText="Evento" />
                <asp:BoundField DataField="puerta" HeaderText="Puerta" />
                <asp:BoundField DataField="vendidas" HeaderText="Vendidas" />
                <asp:BoundField DataField="recaudacion" HeaderText="Recaudación" />
                <asp:BoundField DataField="lugaresLibres" HeaderText="Libres" />
                <asp:BoundField DataField="inicio" HeaderText="Inicio" />
                <asp:BoundField DataField="fin" HeaderText="Fin" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <br />
        </asp:Panel>
    </div>
</asp:Content>
