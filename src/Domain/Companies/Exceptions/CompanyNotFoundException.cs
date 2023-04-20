namespace Domain.Companies.Exceptions;

public class CompanyNotFoundException : Exception
{
    private new const string Message = "Company with given ID not found";

    public CompanyNotFoundException() : base(Message)
    {
    }
}