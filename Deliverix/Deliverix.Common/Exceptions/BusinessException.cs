namespace Deliverix.Common.Exceptions;

public class BusinessException : Exception
{
    public int StatusCode { get; private set; }
    public int ErrorCode { get; private set; }

    public BusinessException(string message, int statusCode = 500, int errorCode = 0 ) : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}