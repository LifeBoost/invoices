namespace Domain.Addresses;

public class Address
{
    public AddressId Id { get; private set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string ZipCode { get; private set; }

    private Address(string street, string city, string zipCode)
    {
        this.Id = new AddressId(Guid.NewGuid());
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public static Address Create(string street, string city, string zipCode)
    {
        return new Address(street, city, zipCode);
    }
}