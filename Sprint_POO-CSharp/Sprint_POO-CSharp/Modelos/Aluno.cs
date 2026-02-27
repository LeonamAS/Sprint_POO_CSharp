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

    public static Aluno CadastrarAluno()
    {
        Console.Clear();
        Console.Write("Nome do Aluno: ");
        string nome = Console.ReadLine();
        Console.Write("CPF: ");
        string cpf = Console.ReadLine();
        Console.Write("Matrícula (número): ");
        int matricula = int.Parse(Console.ReadLine());

        var aluno = new Aluno(nome, cpf, DateTime.Now, matricula);

        Console.WriteLine("Digite as notas (ou -1 para parar):");
        while (true)
        {
            Console.Write("Nota: ");
            if (double.TryParse(Console.ReadLine(), out double nota))
            {
                if (nota == -1) break;
                aluno.AdicionarNota(nota);
            }
        }
        return aluno;
    }
}