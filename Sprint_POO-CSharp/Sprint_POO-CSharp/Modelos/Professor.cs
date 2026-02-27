namespace Sprint_POO_CSharp.Modelos;

internal class Professor : Pessoa
{
    private double Salario { get; set; }
    private List<string> Turmas { get; set; } = new List<string>();

    public Professor(string nome, string cpf, DateTime dataNascimento, double salario)
        : base(nome, cpf, dataNascimento)
    {
        Salario = salario;
    }
    public void AdicionarTurma(string nomeTurma)
    {
        if (!string.IsNullOrWhiteSpace(nomeTurma)) Turmas.Add(nomeTurma);
    }
    public override void ExibirDados()
    {
        Console.WriteLine($"[Professor] Nome: {Nome} | Salário: R${Salario:F2}");
    }
    public static Professor CadastrarProfessor()
    {
        Console.Clear();
        Console.Write("Nome do Professor: ");
        string nome = Console.ReadLine();
        Console.Write("CPF: ");
        string cpf = Console.ReadLine();
        Console.Write("Salário: ");
        double salario = double.Parse(Console.ReadLine());

        var prof = new Professor(nome, cpf, DateTime.Now, salario);

        Console.WriteLine("Adicione as turmas do professor (-1 para sair)");
        while (true)
        {
            
            Console.Write("Turma: ");
            string turma = Console.ReadLine();
            if (turma == "-1") break;
            prof.AdicionarTurma(turma);
        }
        return prof;
    }
}
