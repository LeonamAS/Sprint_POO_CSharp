namespace Sprint_POO_CSharp.Modelos;

using Sprint_POO_CSharp.UI;

internal class Aluno : Pessoa
{
    public string Matricula { get; set; }
    public bool Situacao { get; set; }
    private List<double> Notas { get; set; } = new List<double>();

    public Aluno(string nome, string cpf, DateTime dataNascimento, string matricula, bool situacao)
        : base(nome, cpf, dataNascimento)
    {
        Matricula = matricula;
        Situacao = situacao;
    }
    public void AdicionarNota(double nota)
    {
        if (!Situacao)
        {
            UI.ExibirErro($"Ação Negada: O aluno {Nome} está com a matrícula inativa e não pode receber notas. ");
            return;
        }
        if (nota >= 0 && nota <= 10)
        {
            Notas.Add(nota);
        }
        else
        {
            UI.ExibirErro($"Nota {nota} inválida!!! Use valores entre 0 e 10.");
        }
    }
    public double CalcularMedia() => Notas.Count > 0 ? Notas.Average() : 0;
    public override void ExibirDados()
    {
        string status = Situacao ? "Ativo" : "Inativo";
        Console.WriteLine($" {Nome,-22} | {CPF,-14} | {Matricula,-9} | {status,-7} | {CalcularMedia():F2}");
    }
}