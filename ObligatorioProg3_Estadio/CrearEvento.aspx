<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearEvento.aspx.cs" Inherits="ObligatorioProg3_Estadio.CrearEvento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divEncabezado">
            <asp:Panel ID="Panel1" runat="server" CssClass="Panel" HorizontalAlign="Center" style="margin-left:1.2%" Width="1100px">
        <h1>
        <asp:Label ID="LabelTitulo" runat="server" Text="Crear Evento: " style="font-size: x-large; text-align: center; font-weight: 700"></asp:Label>
        </h1>
        <div id="divFormulario">
            <br />
            <div class="select" style="margin-right: 3%">
                <asp:Label ID="LabelCapacidad" runat="server" Text="Capacidad: "></asp:Label>
                <asp:DropDownList ID="DropCapacidad" runat="server">
                    <asp:ListItem>Total</asp:ListItem>
                    <asp:ListItem>Parcial</asp:ListItem>
                </asp:DropDownList>
                <div class="select_arrow">
                </div>
            </div>
            <br />
            <br />
            <asp:Label ID="LabelNombre" runat="server" Text="Nombre Evento: "></asp:Label>
            <br />
            <asp:TextBox ID="TxtNombreEvento" runat="server" Width="477px" Height="115px" TextMode="MultiLine" CssClass="textBox"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LabelFechaInicio" runat="server" Text="Fecha y hora inicio: "></asp:Label>
            <asp:TextBox ID="TxtFechaHoraInicio" runat="server" TextMode="DateTimeLocal" Width="248px" CssClass="textBox"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LabelFechaFin" runat="server" Text="Fecha y hora fin: "></asp:Label>
            <asp:TextBox ID="TxtFechaHoraFin" runat="server" TextMode="DateTimeLocal" Width="248px" CssClass="textBox"></asp:TextBox>
            <br />
            <br />
            <div class="select" style="margin-right: 3%">
                <asp:Label ID="LabelPromicion" runat="server" Text="Promocion: "></asp:Label>
                <asp:DropDownList ID="ListPromociones" runat="server" AppendDataBoundItems="True" DataSourceID="PromocionSQL" DataTextField="descripcion" DataValueField="idpromocion" OnSelectedIndexChanged="ListPromociones_SelectedIndexChanged" Width="262px" AutoPostBack="True">
                    <asp:ListItem Value="0">(Seleccionar)</asp:ListItem>
                </asp:DropDownList>
                <div class="select_arrow">
                </div>
            </div>
            <asp:SqlDataSource ID="PromocionSQL" runat="server" ConnectionString="<%$ ConnectionStrings:fefabees_ESTADIOURUGUAYConnectionString %>" SelectCommand="SELECT [descripcion], [idpromocion] FROM [PROMOCION]"></asp:SqlDataSource>
            <br />
            <br />
            <asp:Label ID="LabelDescuento" runat="server" Text="Descuento :" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="TxtDescuento" runat="server" Visible="False">0</asp:TextBox>
            <br />
            <div id="divEncabezadoDetalle">
                <h2>
                <asp:Label ID="LabelDetalle" runat="server" Text="Detalles Evento" style="font-size: large"></asp:Label>

                </h2>
                <br />

                <div id="divdetalleEvento">
                    <asp:Label ID="LabelTribuna" runat="server" Text="Tribunas: "></asp:Label>
                    <br />
                    <asp:CheckBoxList ID="CheckBoxTribunas" runat="server" AutoPostBack="True" DataSourceID="TribunaSQL" DataTextField="idtribuna" DataValueField="idtribuna" OnSelectedIndexChanged="CheckBoxTribunas_SelectedIndexChanged" style="margin-left:50%">
                    </asp:CheckBoxList>
                    <asp:SqlDataSource ID="TribunaSQL" runat="server" ConnectionString="<%$ ConnectionStrings:fefabees_ESTADIOURUGUAYConnectionString %>" SelectCommand="SELECT [idtribuna] FROM [TRIBUNA]"></asp:SqlDataSource>
                    <br />
                    <br />
                    <asp:Label ID="LabelPuertas" runat="server" Text="Puertas habilitadas: "></asp:Label>
                    <br />
                    <asp:GridView ID="GridPuerta" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowEditing="GridPuerta_RowEditing" OnRowUpdating="GridPuerta_RowUpdating" Width="837px" OnRowCancelingEdit="GridPuerta_RowCancelingEdit" HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField HeaderText="Puertas" DataField="puerta" ReadOnly="True" />
                            <asp:BoundField HeaderText="Capacidad" DataField="capacidad" ReadOnly="True" />
                            <asp:BoundField HeaderText="Costo" DataField="costo" />
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" >
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
                    <br />
                </div>
            </div>
            <div id="divBotones">
                <asp:Button ID="BtnGuardar" runat="server" OnClick="BtnGuardar_Click" Text="Guardar" class="btn btn-success"/>
                <br />
                <br />
                <asp:Label ID="mensaje" runat="server"></asp:Label>
            </div>
            <br />
        </div>
       </asp:Panel>
    </div>
</asp:Content>
