namespace Sprint_POO_CSharp.Modelos;

internal class Aluno : Pessoa
{
    private int Matricula { get; set; }
    private List<double> Notas { get; set; } = new List<double>();

    public Aluno(string nome, int cpf, DateTime dataNascimento, int matricula)
        : base(nome, cpf, dataNascimento)
    {
        Matricula = matricula;
    }

    public double CalcularMedia() => Notas.Count > 0 ? Notas.Average() : 0;

    public override void ExibirDados()
    {
        //Console.WriteLine($"[Aluno] Nome: {Nome} | Média: {CalcularMedia():F2}");
    }
}