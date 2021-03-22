namespace AADGen.Generators
{
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;

    public interface ITokenGenerator<T>
    {
        public Task<AuthenticationResult> GenerateToken(T options);
    }
}