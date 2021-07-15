namespace AADGen
{
    using CommandLine;
    using System;
    using CommandLineOptions;
    using Generators;

    class Program
    {

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<S2SOptions, OnBehalfOptions, InteractiveOptions>(args)
                .MapResult(
                    (S2SOptions options) => GenerateS2STokenAndExit(options),
                    (OnBehalfOptions options) => GenerateOnBehalfTokenAndExit(options),
                    (InteractiveOptions options) => GenerateTokenInteractivelyAndExit(options),
                    errors => 1
                );
        }

        private static int GenerateTokenInteractivelyAndExit(InteractiveOptions options)
        {
            InteractiveUserTokenGenerator generator = new InteractiveUserTokenGenerator();
            var authResult = generator.GenerateToken(options).GetAwaiter().GetResult();
            return ExitWithResult(authResult.AccessToken);
        }

        private static int GenerateS2STokenAndExit(S2SOptions options)
        {
            if (!String.IsNullOrEmpty(options.ConfigName))
            {
                // TODO: get options from predefined config
            }
            S2STokenGenerator generator = new S2STokenGenerator();
            var authResult = generator.GenerateToken(options).GetAwaiter().GetResult();
            return ExitWithResult(authResult.AccessToken);
        }

        private static int GenerateOnBehalfTokenAndExit(OnBehalfOptions options)
        {
            OnBehalfTokenGenerator generator = new OnBehalfTokenGenerator();
            var authResult = generator.GenerateToken(options).GetAwaiter().GetResult();
            return ExitWithResult(authResult.AccessToken);
        }

        private static int ExitWithResult(string result)
        {
            Console.WriteLine(result);
            return 1;
        }
    }
    
    
}