namespace MiniCoreBanking.Application.Helpers;
public class Generate
{
    public static string GenerateAccountNumber()
    {
        return DateTime.UtcNow.Ticks.ToString().Substring(8);
    }
}