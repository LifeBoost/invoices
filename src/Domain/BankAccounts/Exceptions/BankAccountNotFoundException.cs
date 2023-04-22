namespace Domain.BankAccounts.Exceptions;

public class BankAccountNotFoundException : Exception
{
    private new const string Message = "Bank account with given ID not found";

    public BankAccountNotFoundException() : base(Message)
    {
    }
}