namespace Sprint_POO_CSharp.Modelos;

internal abstract class Pessoa
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    protected DateTime DataDeNascimento { get; set; }

    public Pessoa(string nome, string cpf, DateTime dataDeNascimento)
    {
        Nome = nome;
        CPF = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public abstract void ExibirDados();
}