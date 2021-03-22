namespace AADGen.Utils
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    public class CertificateReader
    {
        public X509Certificate2 ReadCertificate(string certThumbprint)
        {
            bool validOnly = false;

            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    certThumbprint,
                    validOnly);
                return certCollection.OfType<X509Certificate2>().First();
            }
        }
    }
}