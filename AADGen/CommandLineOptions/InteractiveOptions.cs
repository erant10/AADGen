namespace AADGen.CommandLineOptions
{
    using CommandLine;
    using Utils;

    [Verb("interactive", HelpText = "Get a user access token for a specific resource using interactive sign-in")]
    public class InteractiveOptions
    {
        [Option(HelpText = "The client app that the user needs to authorize.")]
        public string ClientId { get; set; }
        
        [Option(HelpText = "The issuing authority.", Default = Defaults.Authority)]
        public string Authority { get; set; }

        [Option(HelpText = "Scopes (Audience)")]
        public string[] Scopes { get; set; }
        
        [Option(HelpText = "Redirect Uri")]
        public string RedirectUri { get; set; }

    }
}