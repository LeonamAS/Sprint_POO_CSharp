namespace Sprint_POO_CSharp.Modelos;

using Sprint_POO_CSharp.UI;

internal class Professor : Pessoa
{
    public double Salario { get; set; }
    private List<string> Turmas { get; set; } = new List<string>();

    public Professor(string nome, string cpf, DateTime dataNascimento, double salario)
        : base(nome, cpf, dataNascimento)
    {
        Salario = salario;
    }
    public void AdicionarTurma(string turma)
    {
        if (!string.IsNullOrWhiteSpace(turma))
        {
            Turmas.Add(turma);
        }
        else
        {
            UI.ExibirErro("O nome da turma não pode estar vazio!");
        }
    }
    public override void ExibirDados()
    {
        string listaTurmas = Turmas.Count > 0 ? string.Join(", ", Turmas) : "Nenhuma turma";
        Console.WriteLine($" {Nome,-22} | {CPF,-14} | R$ {Salario,-8:F2} | {listaTurmas}");
    }
}