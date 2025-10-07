using System.Text.RegularExpressions;

namespace OrdersPoc.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public string Address { get; private set; }

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email não pode ser vazio", nameof(address));

        address = address.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(address, EmailPattern))
            throw new ArgumentException("Email inválido", nameof(address));

        return new Email(address);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}