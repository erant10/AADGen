namespace AADGen.Generators
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using CommandLineOptions;
    using Microsoft.Identity.Client;
    using Utils;

    class S2STokenGenerator : ITokenGenerator<S2SOptions>
    {
        private CertificateReader _certReader;

        public S2STokenGenerator()
        {
            this._certReader = new CertificateReader();
        }

        public async Task<AuthenticationResult> GenerateToken(S2SOptions options)
        {
            X509Certificate2 clientCertificate = this._certReader.ReadCertificate(options.CertThumbprint);

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(options.ClientId)
                .WithAuthority(new Uri(Defaults.Authority), true)
                .WithCertificate(clientCertificate)
                .Build();

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(new string[] { options.Scope })
                    .WithSendX5C(true)
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                var message = @"The application doesn't have sufficient permissions.
                - Did you declare enough app permissions during app creation?
                 -Did the tenant admin grant permissions to the application?";
                Console.WriteLine(message);
                Console.WriteLine(ex);
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                var message = @"Invalid scope. The scope has to be in the form 'https://resourceurl/.default'
                 Mitigation: Change the scope to be as expected.";
                Console.WriteLine(message);
                Console.WriteLine(ex);
            }

            return result;
        }
    }
}