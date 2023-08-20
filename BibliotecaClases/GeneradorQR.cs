using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using MessagingToolkit.QRCode.Codec;
//using iTextSharp.text.pdf;
//using QuestPDF.Helpers;
//using iTextSharp.text;
using System.Drawing;
//using iTextSharp.text.pdf.qrcode;
using QRCoder;
using QRCode = QRCoder.QRCode;
using System.Drawing.Imaging;

namespace ObligatiorioProg3_Estadio
{
    public class GeneradorQR
    {
        public byte[] GenerarQR(String  idcompra)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(idcompra, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            using (var memoryStream = new MemoryStream())
            {
                qrCodeImage.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }

        }

    }
}
 



