using System.Text.RegularExpressions;

namespace OrdersPoc.Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Numero { get; private set; }

    private Cpf(string numero)
    {
        Numero = numero;
    }

    public static Cpf Create(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("CPF não pode ser vazio", nameof(numero));

        numero = Regex.Replace(numero, @"[^\d]", "");

        if (numero.Length != 11)
            throw new ArgumentException("CPF deve conter 11 dígitos", nameof(numero));

        if (!IsValid(numero))
            throw new ArgumentException("CPF inválido", nameof(numero));

        return new Cpf(numero);
    }

    private static bool IsValid(string cpf)
    {
        if (cpf.Distinct().Count() == 1)
            return false;

        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digito1)
            return false;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return int.Parse(cpf[10].ToString()) == digito2;
    }

    public string FormatarCpf()
    {
        return $"{Numero.Substring(0, 3)}.{Numero.Substring(3, 3)}.{Numero.Substring(6, 3)}-{Numero.Substring(9, 2)}";
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Numero;
    }

    public override string ToString() => FormatarCpf();
}