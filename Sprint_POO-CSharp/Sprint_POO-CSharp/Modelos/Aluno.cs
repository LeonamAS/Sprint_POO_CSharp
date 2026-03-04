namespace Sprint_POO_CSharp.Modelos;

internal class Aluno : Pessoa
{
    private string Matricula { get; set; }
    private List<double> Notas { get; set; } = new List<double>();

    public Aluno(string nome, string cpf, DateTime dataNascimento, string matricula)
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
        Console.WriteLine($"[Aluno] Nome: {Nome} | CPF: {CPF} | Matrícula: {Matricula} | Média: {CalcularMedia():F2}");
    }

    public static Aluno CadastrarAluno()
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRAR NOVO ALUNO ---\n");
        Console.Write("Nome do Aluno: ");
        string nome = Console.ReadLine()!;

        string cpf = "";
        while (true)
        {
            Console.Write("CPF (somente números, 11 dígitos): ");
            string entradaCpf = Console.ReadLine()!;

            string apenasNumeros = new string(entradaCpf.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length == 11)
            {
                cpf = apenasNumeros;
                break;
            }
            else
            {
                Console.WriteLine("CPF inválido! O CPF deve conter exatamente 11 números. Tente novamente.");
            }
        }

        string matricula = "";
        while (true)
        {
            Console.Write("Matrícula (exatamente 6 números): ");
            string entradaMatricula = Console.ReadLine()!;

            string apenasNumerosMatricula = new string(entradaMatricula.Where(char.IsDigit).ToArray());

            if (apenasNumerosMatricula.Length == 6)
            {
                matricula = apenasNumerosMatricula;
                break;
            }
            else
            {
                Console.WriteLine("Matrícula inválida! A matrícula deve conter exatamente 6 números. Tente novamente.");
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
            Console.WriteLine("\nNenhum aluno cadastrado no sistema.");
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

        string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());

        var alunoEncontrado = alunos.FirstOrDefault(aluno =>
            new string(aluno.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
        );

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
            Console.WriteLine("\nNotas atualizadas com sucesso!");
        }
        else
        {
            Console.WriteLine("\nAluno não encontrado com esse CPF.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}