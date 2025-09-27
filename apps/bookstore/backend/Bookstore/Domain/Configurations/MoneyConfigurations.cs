namespace Domain.Configurations;

public static class MoneyConfigurations
{
    public const decimal MinimumAmount = 0;
    public const decimal MaximumAmount = 999999;
    public static readonly string[] SupportedCurrencies = ["EUR", "USD"];
}
