using System.Text.RegularExpressions;

namespace OrdersPoc.Domain.ValueObjects;

public sealed class Cnpj : ValueObject
{
    public string Numero { get; private set; }

    private Cnpj(string numero)
    {
        Numero = numero;
    }

    public static Cnpj Create(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("CNPJ não pode ser vazio", nameof(numero));

        numero = Regex.Replace(numero, @"[^\d]", "");

        if (numero.Length != 14)
            throw new ArgumentException("CNPJ deve conter 14 dígitos", nameof(numero));

        if (!IsValid(numero))
            throw new ArgumentException("CNPJ inválido", nameof(numero));

        return new Cnpj(numero);
    }

    private static bool IsValid(string cnpj)
    {
        if (cnpj.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }

    public string FormatarCnpj()
    {
        return $"{Numero.Substring(0, 2)}.{Numero.Substring(2, 3)}.{Numero.Substring(5, 3)}/{Numero.Substring(8, 4)}-{Numero.Substring(12, 2)}";
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Numero;
    }

    public override string ToString() => FormatarCnpj();
}
