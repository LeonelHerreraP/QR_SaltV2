using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using Salt_QR.Interfaces;
using Saltarin.VM;

namespace Salt_QR.Services
{
    public class QR_SaltService : IQR_SaltService
    {
        public void MandaUrl(UrlVM url)
        {
            // Generando numeros aleatorios
            Random rnd = new();
            var primer = rnd.Next(100, 999);
            var segundo = rnd.Next(1000, 9999);
            var tercer = rnd.Next(100, 999);

            // Url
            var urlOriginal = url.Url;
            urlOriginal = urlOriginal.ToUpper(); // Pasando a mayusculas la Url

            // Colocando los numeros en la Url
            var particion = urlOriginal.Substring(6);
            var resultado = urlOriginal.Remove(6, particion.Length);
            resultado = primer + resultado + segundo + particion + tercer;

            QRCodeWriter.CreateQrCode(resultado, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("C:/Users/Administrador/Desktop/saltarin_qr/public/MyQR.png");
        }

        public bool VerificarUrl(UrlVM url)
        {
            try
            {
                // Extrayendo los numeros de la Url
                var cadenaOriginal = url.Url;
                var sinPrimer = cadenaOriginal.Substring(3);

                var primerNum = int.Parse(cadenaOriginal.Remove(3, sinPrimer.Length));

                var bue = sinPrimer.Substring(6);
                var sinSegundo = sinPrimer.Remove(6, bue.Length);
                var ultimaPartCon = bue.Substring(4);

                var segundoNum = int.Parse(bue.Remove(4, ultimaPartCon.Length));

                var tercerNum = int.Parse(ultimaPartCon[^3..]);

                var ultimaPartSin = ultimaPartCon.Remove(ultimaPartCon.Length - 3, 3);
                var urlFinal = sinSegundo + ultimaPartSin;
                var primer = false;
                var segundo = false;
                var tercer = false;

                // Verificando si los numeros estan dentro del rango antes definidos
                if ((primerNum >= 100) && (primerNum <= 999))
                {
                    primer = true;
                }
                if ((segundoNum >= 1000) && (segundoNum <= 9999))
                {
                    segundo = true;
                }
                if ((tercerNum >= 100) && (tercerNum <= 999))
                {
                    tercer = true;
                }

                if (primer == true && segundo == true && tercer == true)
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }
    }
}
