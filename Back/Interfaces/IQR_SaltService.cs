using Microsoft.AspNetCore.Mvc;
using Saltarin.VM;

namespace Salt_QR.Interfaces
{
    public interface IQR_SaltService
    {
        void MandaUrl(UrlVM url);
        bool VerificarUrl(UrlVM url);
    }
}
