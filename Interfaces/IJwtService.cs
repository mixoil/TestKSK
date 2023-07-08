namespace TestKSK.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(List<(string, string)> payload);
        bool ValidateToken(string authToken);
    }
}
