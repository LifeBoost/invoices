namespace Domain.Contractors.Exceptions;

public class ContractorNotFoundException : Exception
{
    private new const string Message = "Contractor with given ID not found";

    public ContractorNotFoundException() : base(Message)
    {
    }
}