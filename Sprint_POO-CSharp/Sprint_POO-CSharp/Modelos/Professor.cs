namespace Sprint_POO_CSharp.Modelos;

internal class Professor : Pessoa
{
    private double Salario { get; set; }
    private List<string> Turmas { get; set; } = new List<string>();
    //Definir um padrão para as turmas

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
            Console.WriteLine("O nome da turma não pode estar vazio!");
        }
    }
    public override void ExibirDados()
    {
        string listaTurmas = Turmas.Count > 0 ? string.Join(", ", Turmas) : "Nenhuma turma";
        Console.WriteLine($"[Professor] Nome: {Nome} | CPF: {CPF} | Salário: R${Salario:F2} | Turmas: {listaTurmas}");
    }
    public static Professor CadastrarProfessor()
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRAR NOVO PROFESSOR ---\n");
        Console.Write("Nome do Professor: ");
        string nome = Console.ReadLine()!;

        Console.Write("CPF: ");
        string cpf = Console.ReadLine()!;
        //Tornar impossível adicionar professor sem CPF;

        double salario;

        while (true)
        {
            Console.Write("Salário: ");
            string entrada = Console.ReadLine()!;

            if (double.TryParse(entrada, out salario))
            {
                if (salario >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("O salário não pode ser negativo. Digite um valor válido.");
                }
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, digite apenas números para o salário.");
            }
        }

        var prof = new Professor(nome, cpf, DateTime.Now, salario);

        Console.WriteLine("Adicione as turmas do professor (-1 para sair)");
        while (true)
        {
            
            Console.Write("Turma: ");
            string turma = Console.ReadLine()!;

            if (turma == "-1") break;
            prof.AdicionarTurma(turma);
        }
        return prof;
    }
    public static void DefinirTurmas(List<Pessoa> listaPessoas)
    {
        Console.Clear();
        Console.WriteLine("--- DEFINIR TURMAS DE PROFESSOR EXISTENTE ---");

        var professores = listaPessoas.OfType<Professor>().ToList();

        if (professores.Count == 0)
        {
            Console.WriteLine("Nenhum professor cadastrado no sistema.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Professores disponíveis:");
        foreach (var professor in professores)
        {
            professor.ExibirDados();
        }

        Console.Write("\nDigite o CPF do professor para adicionar turmas: ");
        string cpfBusca = Console.ReadLine();

        var professorEncontrado = professores.FirstOrDefault(professor => professor.CPF == cpfBusca);

        if (professorEncontrado != null)
        {
            Console.WriteLine($"\nAdicionando turmas para: {professorEncontrado.Nome}");
            Console.WriteLine("Digite o nome da turma (ou digite -1 para parar):");

            while (true)
            {
                Console.Write("Turma: ");
                string turma = Console.ReadLine();

                if(turma == "-1") break;

                professorEncontrado.AdicionarTurma(turma);
            }
            Console.WriteLine("Turmas atualizadas com sucesso!");
        }
        else
        {
            Console.WriteLine("\nProfessor não encontrado com esse CPF.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}
