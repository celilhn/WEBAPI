
namespace Domain.Constants
{
    public class Constants
    {
        public enum ProcessResults
        {
            Unknown = -1,
            Success = 0,
            ServiceError = 1,
            ServiceSystemError = 2,
            ModelValidationError = 3,
            SystemError = 4,
            Authorization = 5,
            UserNotFound = 6,
            RefreshTokenNotFound = 7
        }
    }
}
