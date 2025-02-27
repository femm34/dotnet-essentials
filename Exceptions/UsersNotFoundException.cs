namespace MiApi.Exceptions;

public class UsersNotFoundException : Exception
{
    public UsersNotFoundException() : base("No users found.")
    {
    }

    public UsersNotFoundException(string message) : base(message)
    {
    }

    public UsersNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}