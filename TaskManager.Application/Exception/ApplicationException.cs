namespace TaskManager.Application.Exception;

public class AppException : System.Exception
{
    internal AppException(string businessMessage)
        : base(businessMessage)
    {
    }

    internal AppException(string message, System.Exception innerException)
        : base(message, innerException)
    {
    }
}