using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.VOs;

public record Isbn
{
    public string Value { get; }

    public Isbn(string value)
    {
        Value = ValidateAndGetIsbnValue(value);
    }

    #region Private methods

    private string ValidateAndGetIsbnValue(string value)
    {
        var normalizedValue = Regex.Replace(value.ToUpperInvariant(), @"\s|-", "");
        Regex regEx = new("^(?:\\d{9}[\\dX]|97[89]\\d{10})$");

        if (!regEx.IsMatch(normalizedValue)) throw new InvalidIsbnException("ISBN inválido");

        if (normalizedValue.Length == 10)
        {
            Isbn10CheckSum(normalizedValue);
        }
        else
        {
            Isbn13CheckSum([.. normalizedValue.Select(c => (int)char.GetNumericValue(c))]);
        }

        return normalizedValue;
    }

    private void Isbn10CheckSum(string value)
    {
        var sum = value.Take(value.Length - 1).Select((d, i) => (int)char.GetNumericValue(d) * (i + 1)).Sum();

        var controlDigit = sum % 11;
        if ((controlDigit == 10) && value.Last() != 'X') throw new InvalidIsbnException("ISBN inválido");
        if ((controlDigit != 10) && (int)char.GetNumericValue(value.Last()) != controlDigit) throw new InvalidIsbnException("ISBN inválido");
    }

    private void Isbn13CheckSum(List<int> value)
    {
        var sum = value
            .Take(value.Count - 1)
            .Select((d, i) => i % 2 == 0 ? d * 1 : d * 3)
            .Sum();

        var controlDigit = (10 - (sum % 10)) % 10;
        if (controlDigit != value.Last()) throw new InvalidIsbnException("ISBN inválido");
    }

    #endregion
}