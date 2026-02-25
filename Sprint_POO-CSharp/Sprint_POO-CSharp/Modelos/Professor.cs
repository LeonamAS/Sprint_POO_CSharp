namespace Sprint_POO_CSharp.Modelos;

internal class Professor : Pessoa
{
    private double Salario { get; set; }
    private List<string> Turmas { get; set; } = new List<string>();

    public Professor(string nome, int cpf, DateTime dataNascimento, double salario)
        : base(nome, cpf, dataNascimento)
    {
        Salario = salario;
    }

    public override void ExibirDados()
    {
        //Console.WriteLine($"[Professor] Nome: {Nome} | Salário: R${Salario:F2}");
    }
}
