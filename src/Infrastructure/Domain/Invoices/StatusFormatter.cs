using Domain.Invoices;

namespace Infrastructure.Domain.Invoices;

public class StatusFormatter
{
    public static string ToDatabaseValue(Status status)
    {
        return status switch
        {
            Status.Draft => "draft",
            Status.Send => "send",
            Status.Paid => "paid",
            Status.Deleted => "deleted",
            _ => throw new InvalidCastException("Invalid invoice status")
        };
    }

    public static Status ToEnum(string status)
    {
        return status switch
        {
            "draft" => Status.Draft,
            "send" => Status.Send,
            "paid" => Status.Paid,
            "deleted" => Status.Deleted,
            _ => throw new InvalidCastException("Invalid invoice status")
        };
    }
}