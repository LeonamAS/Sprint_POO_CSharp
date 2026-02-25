namespace Sprint_POO_CSharp.Modelos;

internal abstract class Pessoa
{
    private string Nome { get; set; }
    private int CPF { get; set; }
    private DateTime DataDeNascimento { get; set; }

    public Pessoa(string nome, int cpf, DateTime dataDeNascimento)
    {
        Nome = nome;
        CPF = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public abstract void ExibirDados();
}
