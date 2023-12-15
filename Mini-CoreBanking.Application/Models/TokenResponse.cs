namespace MiniCoreBanking.Application.Models;

public class TokenResponse
{
    public required string Token { get; set; }
    public DateTime Expiration { get; set; }

}
;
