using Domain;

namespace Infrastructure.Domain;

public static class CurrencyFormatter
{
	public static string ToDatabaseValue(Currency currency)
	{
		return currency switch
		{
			Currency.Eur => "EUR",
			Currency.Pln => "PLN",
			Currency.Usd => "USD",
			_ => throw new InvalidCastException("Invalid currency")
		};
	}

	public static Currency ToEnum(string currency)
	{
		return currency switch
		{
			"EUR" => Currency.Eur,
			"PLN" => Currency.Pln,
			"USD" => Currency.Usd,
			_ => throw new InvalidCastException("Invalid currency")
		};
	}
}