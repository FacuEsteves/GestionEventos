 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;
using System.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.IO;
using BibliotecaClases;

namespace ObligatiorioProg3_Estadio
{
    public class GeneradorPDF
    {
        public byte[] GenerarPdf( List<RecaudacionEvento> resumen,List<RecaudacionEvento> detalle)
        {
            int cont = detalle.Count - 1;

            var data = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(30);
                        page.Header().ShowOnce().Row(row =>
                        {
                            /*var rutaImagen = Path.GetFullPath("/Biblioteca/Images/EstadioUruguayLogo.png");
                            byte[] imagen = System.IO.File.ReadAllBytes(rutaImagen);

                            row.RelativeItem().Image(imagen);*/


                            row.RelativeItem().PaddingTop(3).Border(0).
                            Column(col =>
                            {
                                col.Item().AlignCenter().Text(" ").Bold().FontSize(25);
                                col.Item().AlignCenter().Text("Resumén Contable").Bold().FontSize(15);
                                //col.Item().AlignCenter().Text(detalle[0].evento).FontSize(9);
                                col.Item().AlignCenter().Text("Desde: " + detalle[0].inicio).FontSize(9);
                                col.Item().AlignCenter().Text("Hasta: " + detalle[cont].fin).FontSize(9);
                            });
                        });
                        page.Content().AlignCenter().Padding(20).AlignCenter().Column(col1 =>
                        {
                          
                            col1.Item().PaddingBottom(20).Text("Resumen").FontSize(15).Bold();

                            col1.Item().Table(tabla =>
                            {

                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                    Text("Vendidas").FontSize(15).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                    Text("Recaudación").FontSize(15).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                    Text("Libres").FontSize(15).Bold().FontColor(Colors.White);
                                });
                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((resumen[0].vendidas).ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((resumen[0].recaudacion).ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((resumen[0].lugaresLibres).ToString()).FontSize(10);
                            });

                            col1.Item().Padding(20).Text("Detalle").FontSize(15).Bold();

                            col1.Item().Table(tabla2 =>
                            {

                                tabla2.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                tabla2.Header(header =>
                                {
                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Evento").FontSize(10).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Puertas").FontSize(10).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Vendidas").FontSize(10).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Recaudación").FontSize(10).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Libres").FontSize(10).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Inicio").FontSize(10).Bold().FontColor(Colors.White);

                                    header.Cell().Background("#6DAAE2").Padding(0).AlignCenter().
                                    Text("Fin").FontSize(10).Bold().FontColor(Colors.White);

                                });
                                for(int i=0;i<detalle.Count;i++)
                                {
                                    //Evento
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].evento).ToString()).FontSize(7);

                                    //Puerta
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].puerta).ToString()).FontSize(7);

                                    //Vendidas
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].vendidas).ToString()).FontSize(7);

                                    //Recaudación
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].recaudacion).ToString()).FontSize(7);

                                    //Libres
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].lugaresLibres).ToString()).FontSize(7);

                                    //Inicio
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].inicio).ToString()).FontSize(7);

                                    //Fin
                                    tabla2.Cell().BorderBottom(0.5f).
                                    BorderColor("#D9D9D9").Padding(0).AlignCenter().
                                    Text((detalle[i].fin).ToString()).FontSize(7);
                                }
                            });
                        });
                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Pagina ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                }).GeneratePdf();
            MemoryStream stream = new MemoryStream(data);
            byte[] pdf = stream.ToArray();
            return pdf;
        }
        public byte[] GenerarPdfConQR(byte[] qr, List<String> detallecompra)
        {
            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {
                        row.RelativeItem().Image(qr);
                        row.RelativeItem().PaddingTop(3).Border(0).
                        Column(col =>
                        {
                            DateTime fechaActualConHora = DateTime.Now;
                            col.Item().AlignCenter().Text(" ").Bold().FontSize(25);
                            col.Item().AlignCenter().Text("Factura de compra").Bold().FontSize(15);
                             
                           col.Item().AlignCenter().Text(fechaActualConHora.ToString()).FontSize(9);
                        });



                    });
                    page.Content().AlignCenter().Padding(20).AlignCenter().Column(col1 =>
                    {

                        col1.Item().PaddingBottom(20).Text("Resumen").FontSize(15).Bold();

                        col1.Item().Table(tabla =>
                        {

                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            tabla.Header(header =>
                            {
                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Evento").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Documento").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Nombre").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Apellido").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Correo electronico").FontSize(10).Bold().FontColor(Colors.White);

                               header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Puerta").FontSize(10).Bold().FontColor(Colors.White);
                               
                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                               Text("Tribuna").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                 Text("Precio").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                 Text("Descripcion").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                 Text("Plan").FontSize(10).Bold().FontColor(Colors.White);

                            });
                            for (int i = 0; i < detallecompra.Count; i++)
                            {
                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((detallecompra[0]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((detallecompra[1]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((detallecompra[2]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                               BorderColor("#D9D9D9").Padding(2).AlignCenter().
                               Text((detallecompra[3]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((detallecompra[4]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((detallecompra[5]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                              BorderColor("#D9D9D9").Padding(2).AlignCenter().
                              Text((detallecompra[6]).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                              BorderColor("#D9D9D9").Padding(2).AlignCenter().
                              Text(("$" + detallecompra[7]).ToString()).FontSize(9);


                                tabla.Cell().BorderBottom(0.5f).
                              BorderColor("#D9D9D9").Padding(2).AlignCenter().
                              Text((detallecompra[8]).ToString()).FontSize(9);


                                tabla.Cell().BorderBottom(0.5f).
                              BorderColor("#D9D9D9").Padding(2).AlignCenter().
                              Text((detallecompra[9]).ToString()).FontSize(9);

                                break;
                            }
                        });

                    });
                    page.Footer().AlignRight().Text(txt =>
                    {

                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });

            }).GeneratePdf();
            MemoryStream stream = new MemoryStream(data);
            byte[] pdf = stream.ToArray();
            return pdf;
        }
        public byte[] GenerarPdfEventos(List<Evento> eventos)
        {
            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {

                    });
                    page.Content().AlignCenter().Padding(20).AlignCenter().Column(col1 =>
                    {

                        col1.Item().PaddingBottom(20).Text("Eventos").FontSize(15).Bold();

                        col1.Item().Table(tabla =>
                        {

                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            tabla.Header(header =>
                            {
                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                             Text("Id").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Evento").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Inicio").FontSize(10).Bold().FontColor(Colors.White);

                                header.Cell().Background("#6DAAE2").Padding(2).AlignCenter().
                                Text("Fin").FontSize(10).Bold().FontColor(Colors.White);
                            });
                            for (int i = 0; i < eventos.Count; i++)
                            {
                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((eventos[i].idevento).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((eventos[i].nombre).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((eventos[i].fecha_hora_inicio).ToString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).
                                BorderColor("#D9D9D9").Padding(2).AlignCenter().
                                Text((eventos[i].fecha_hora_fin).ToString()).FontSize(9);
                            }
                        });

                    });
                    page.Footer().AlignRight().Text(txt =>
                    {

                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });

            }).GeneratePdf();
            MemoryStream stream = new MemoryStream(data);
            byte[] pdf = stream.ToArray();
            return pdf;
        }
    }
}
