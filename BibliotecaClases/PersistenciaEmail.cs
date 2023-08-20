using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Drawing;
using System.Net.Mime;

namespace ObligatorioProg3_Estadio
{
    public class PersistenciaEmail
    {
        public static string enviarEmail(string asunto, byte[] pdf,string htmlString = "", string emailDestinatario = "")
        {
            string resultado = "";
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("Obligatorioestadio2023@gmail.com");

                message.To.Add(new MailAddress(emailDestinatario));

                message.Subject = asunto;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                Stream st = new MemoryStream(pdf);

                Attachment a = new Attachment(st,"archivo.pdf");
               
                message.Attachments.Add(a);

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("Obligatorioestadio2023@gmail.com", "hhbzbdyaagazhmjr");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }
    }
}
