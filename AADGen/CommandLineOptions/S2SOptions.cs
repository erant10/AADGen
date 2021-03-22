namespace AADGen.CommandLineOptions
{
    using CommandLine;

    [Verb("s2s", HelpText = "Generate Service to Service Token")]
    public class S2SOptions
    {
        [Option(SetName  = "custom", Required = true,
            HelpText = "Client App Id")]
        public string ClientId { get; set; }

        [Option(SetName = "custom", Required = true,
            HelpText = "Client App Certificate Thumbprint")]
        public string CertThumbprint { get; set; }

        [Option(SetName = "custom", Required = true,
            HelpText = "Scope for the token (Audience)")]
        public string Scope { get; set; }
        
        [Option(SetName = "config", Required = true,
            HelpText = "Name of S2S Config Section in appsettings")]
        public string ConfigName { get; set; }
    }
}