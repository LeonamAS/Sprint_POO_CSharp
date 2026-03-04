namespace Sprint_POO_CSharp.Modelos;

internal abstract class Pessoa
{
    public string Nome { get; set; }

    private string _cpf = "";

    public string CPF
    {
        get { return _cpf; }
        set { _cpf = FormatarCPF(value); }
    }

    protected DateTime DataDeNascimento { get; set; }

    public Pessoa(string nome, string cpf, DateTime dataDeNascimento)
    {
        Nome = nome;
        CPF = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public abstract void ExibirDados();

    private string FormatarCPF(string cpfDigitado)
    {
        if (string.IsNullOrWhiteSpace(cpfDigitado))
            return "CPF Não Informado";

        string apenasNumeros = new string(cpfDigitado.Where(char.IsDigit).ToArray());

        if (apenasNumeros.Length == 11)
        {
            return $"{apenasNumeros.Substring(0, 3)}.{apenasNumeros.Substring(3, 3)}.{apenasNumeros.Substring(6, 3)}-{apenasNumeros.Substring(9, 2)}";
        }

        return cpfDigitado;
    }
}