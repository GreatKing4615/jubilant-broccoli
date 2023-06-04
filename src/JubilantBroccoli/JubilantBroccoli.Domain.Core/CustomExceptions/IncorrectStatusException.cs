namespace JubilantBroccoli.Domain.Core.CustomExceptions;

public class IncorrectStatusException: Exception
{
    public IncorrectStatusException()
    {
    }

    public IncorrectStatusException(string message)
        : base(message)
    {
    }

    public IncorrectStatusException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}