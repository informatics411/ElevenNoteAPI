using System.Threading.Tasks;


public interface ITokenService
{
    Task<TokenResponse> GetTokenAsync(TokenRequest model);
}
