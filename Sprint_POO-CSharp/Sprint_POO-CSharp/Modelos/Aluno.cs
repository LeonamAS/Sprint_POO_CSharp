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
        Console.WriteLine("--- CADASTRAR NOVO ALUNO ---\n");
        Console.Write("Nome do Aluno: ");
        string nome = Console.ReadLine()!;

        Console.Write("CPF: ");
        string cpf = Console.ReadLine()!;
        //Tornar impossível adicionar aluno sem CPF

        int matricula;

        while (true)
        {
            Console.Write("Matrícula (número): ");
            string entrada = Console.ReadLine()!;

            if (int.TryParse(entrada, out matricula))
            {
                break;
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, digite apenas números inteiros para a matrícula.");
            }
        }

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
            else
            {
                Console.WriteLine("Valor inválido. Digite um número.");
            }
        }
        return aluno;
    }

    public static void InserirNotas(List<Pessoa> listaPessoas)
    {
        Console.Clear();
        Console.WriteLine("--- INSERIR NOTAS DE ALUNO EXISTENTE ---");

        var alunos = listaPessoas.OfType<Aluno>().ToList();

        if (alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado no sistema.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Alunos disponíveis:");
        foreach (var aluno in alunos)
        {
            aluno.ExibirDados();
        }

        Console.Write("\nDigite o CPF do aluno para adicionar notas: ");
        string cpfBusca = Console.ReadLine()!;

        var alunoEncontrado = alunos.FirstOrDefault(aluno => aluno.CPF == cpfBusca);

        if (alunoEncontrado != null)
        {
            Console.WriteLine($"\nAdicionando notas para: {alunoEncontrado.Nome}");
            Console.WriteLine("Digite as notas (ou -1 para parar):");

            while (true)
            {
                Console.Write("Nota: ");
                if (double.TryParse(Console.ReadLine(), out double nota))
                {
                    if (nota == -1) break;

                    alunoEncontrado.AdicionarNota(nota);
                }
                else
                {
                    Console.WriteLine("Valor inválido. Digite um número.");
                }
            }
            Console.WriteLine("Notas atualizadas com sucesso!");
        }
        else
        {
            Console.WriteLine("\nAluno não encontrado com esse CPF.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}