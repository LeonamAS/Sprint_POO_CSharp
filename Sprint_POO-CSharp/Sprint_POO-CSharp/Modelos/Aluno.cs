namespace Sprint_POO_CSharp.Modelos;

internal class Aluno : Pessoa
{
    private int Matricula { get; set; }
    private List<double> Notas { get; set; } = new List<double>();

    public Aluno(string nome, string cpf, DateTime dataNascimento, int matricula)
        : base(nome, cpf, dataNascimento)
    {
        Matricula = matricula;
    }

    public void AdicionarNota(double nota)
    {
        if (nota >= 0 && nota <= 10)
        {
            Notas.Add(nota);
        }
        else
        {
            Console.WriteLine($"Nota {nota} inválida!!! Use valores entre 0 e 10.");
        }
    }
    public double CalcularMedia() => Notas.Count > 0 ? Notas.Average() : 0;

    public override void ExibirDados()
    {
        Console.WriteLine($"[Aluno] Nome: {Nome} | Média: {CalcularMedia():F2}");
    }
}