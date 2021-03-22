namespace AADGen.CommandLineOptions
{
    using CommandLine;
    using Utils;

    [Verb("onbehalf", HelpText = "Exchange a User Token for a Client Application to call another API on behalf of the user")]
    public class OnBehalfOptions
    {
        [Option(SetName = "token", HelpText = "A user access token for the trusted client app")]
        public string AccessToken { get; set; }
        
        [Option(SetName = "token", Required = true,
            HelpText = "The AppId Of the trusted client app")]
        public string ClientId { get; set; }
        
        [Option(SetName = "token", Required = true,
            HelpText = "Certificate thumbprint for the trusted client application")]
        public string CertThumbprint { get; set; }
        
        [Option(SetName = "token", Default = Defaults.Authority,
            HelpText = "The authority of the trusted client application. (must match the authority that issued the original user token)")]
        public string Authority { get; set; }
        
        [Option(SetName = "token", Default = Defaults.Authority,
            HelpText = "The scope for the target api. e.g. https://graph.microsoft.com/application.readwrite.all")]
        public string Scope { get; set; }

    }
}