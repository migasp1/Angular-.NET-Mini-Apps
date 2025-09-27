using Domain.Configurations;
using Domain.Exceptions;
using System.Globalization;

namespace Domain.VOs;

public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        (Amount, Currency) = ValidateAndGetMoneyFields(amount, currency);
    }

    #region Private methods

    private (decimal, string) ValidateAndGetMoneyFields(decimal amount, string currency)
    {
        var normalizedAmount = Math.Round(amount, 2, MidpointRounding.ToEven);

        if (normalizedAmount < MoneyConfigurations.MinimumAmount || normalizedAmount > MoneyConfigurations.MaximumAmount)
            throw new InvalidPriceException($"O preço do livro não pode ser negativo e tem de ser inferior a {MoneyConfigurations.MaximumAmount.ToString("C2", CultureInfo.InvariantCulture)}");

        var validCurrency = currency.Trim().ToUpperInvariant();

        if (!MoneyConfigurations.SupportedCurrencies.Contains(validCurrency)) throw new UnsupportedCurrencyException("Moeda não suportada");

        return (normalizedAmount, validCurrency);
    }

    public override string ToString() => $"{Currency} {Amount.ToString("F2", CultureInfo.InvariantCulture)}";

    #endregion
}