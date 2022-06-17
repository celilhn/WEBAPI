
namespace Application.Interfaces
{
    public interface IJwtService
    {
        string CreateAccessToken(string userID);
        string ValidateAccessToken(string token);
        string CreateRefreshToken();
    }
}
