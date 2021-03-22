namespace AADGen.Generators
{
    using System.Linq;
    using System.Threading.Tasks;
    using CommandLineOptions;
    using Microsoft.Identity.Client;

    public class InteractiveUserTokenGenerator : ITokenGenerator<InteractiveOptions>
    {
        public async Task<AuthenticationResult> GenerateToken(InteractiveOptions options)
        {
            var app = PublicClientApplicationBuilder.Create(options.ClientId)
                .WithAuthority(options.Authority)
                .WithRedirectUri(options.RedirectUri).Build();
            var accounts = await app.GetAccountsAsync();
            AuthenticationResult result;
            try
            {
                result = await app.AcquireTokenSilent(options.Scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch(MsalUiRequiredException)
            {
                result = await app.AcquireTokenInteractive(options.Scopes)
                    .ExecuteAsync();
            }

            return result;
        }
    }
}