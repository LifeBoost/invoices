namespace Application.Configuration.Validation;

public class InvalidCommandException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public InvalidCommandException(IDictionary<string, string[]> errors) : base(null)
    {
        Errors = errors;
    }
}