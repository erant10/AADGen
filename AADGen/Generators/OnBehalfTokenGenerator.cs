namespace AADGen.Generators
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using CommandLineOptions;
    using Microsoft.Identity.Client;
    using Utils;

    public class OnBehalfTokenGenerator : ITokenGenerator<OnBehalfOptions>
    {
        private CertificateReader _certReader;

        public OnBehalfTokenGenerator()
        {
            this._certReader = new CertificateReader();
        }

        public async Task<AuthenticationResult> GenerateToken(OnBehalfOptions options)
        {
            AuthenticationResult result = null;
            try
            {
                X509Certificate2 certificate = this._certReader.ReadCertificate(options.CertThumbprint);
                IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(options.ClientId)
                    .WithAuthority(new Uri(options.Authority), true)
                    .WithCertificate(certificate)
                    .Build();
                var userAssertion = new UserAssertion(
                    options.AccessToken,
                    "urn:ietf:params:oauth:grant-type:jwt-bearer");
                result = await app.AcquireTokenOnBehalfOf(new string[] { options.Scope }, userAssertion).ExecuteAsync();
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