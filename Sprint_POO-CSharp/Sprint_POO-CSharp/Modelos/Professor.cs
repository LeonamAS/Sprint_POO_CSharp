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
    public void AdicionarTurma(string turma)
    {
        if (!string.IsNullOrWhiteSpace(turma))
        {
            Turmas.Add(turma);
        }
        else
        {
            Console.WriteLine("\nO nome da turma não pode estar vazio!");
        }
    }
    public override void ExibirDados()
    {
        string listaTurmas = Turmas.Count > 0 ? string.Join(", ", Turmas) : "Nenhuma turma";
        Console.WriteLine($" {Nome,-22} | {CPF,-14} | R$ {Salario,-8:F2} | {listaTurmas}");
    }
    public static Professor CadastrarProfessor(List<Pessoa> listaPessoas)
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRAR NOVO PROFESSOR ---\n");
        Console.Write("Nome do Professor: ");
        string nome = "";
        while (true)
        {
            Console.Write("Nome do Professor: ");
            nome = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("O nome não pode ficar em branco. Tente novamente.\n");
            }
            else if (nome.Any(char.IsDigit))
            {
                Console.WriteLine("Erro: O nome não pode conter números. Tente novamente.\n");
            }
            else
            {
                break;
            }
        }

        string cpf = "";
        while (true)
        {
            Console.Write("CPF (somente números, 11 dígitos): ");
            string entradaCpf = Console.ReadLine()!;
            string apenasNumeros = new string(entradaCpf.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length == 11)
            {
                bool cpfJaExiste = listaPessoas.Any(pessoa => new string(pessoa.CPF.Where(char.IsDigit).ToArray()) == apenasNumeros);

                if (cpfJaExiste)
                {
                    Console.WriteLine("Erro: Este CPF já está cadastrado no sistema para outra pessoa. Tente novamente.\n");
                }
                else
                {
                    cpf = apenasNumeros;
                    break;
                }
            }
            else
            {
                Console.WriteLine("\nCPF inválido! O CPF deve conter exatamente 11 números. Tente novamente.");
            }
        }

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
                    Console.WriteLine("\nO salário não pode ser negativo. Digite um valor válido.");
                }
            }
            else
            {
                Console.WriteLine("\nValor inválido. Por favor, digite apenas números para o salário.");
            }
        }

        var prof = new Professor(nome, cpf, DateTime.Now, salario);

        Console.WriteLine("Adicione as turmas do professor (-1 para sair)");
        while (true)
        {
            Console.Write("Turma: ");
            string turma = Console.ReadLine()!;

            if (turma == "-1") break;

            if (string.IsNullOrWhiteSpace(turma))
            {
                Console.WriteLine("O nome da turma não pode estar vazio!\n");
            }
            else if (turma.Any(char.IsDigit))
            {
                Console.WriteLine("Erro: O nome da turma não pode conter números.\n");
            }
            else
            {
                prof.AdicionarTurma(turma);
            }
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
            Console.WriteLine("\nNenhum professor cadastrado no sistema.");
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
        string cpfBusca = Console.ReadLine()!;

        string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());

        var professorEncontrado = professores.FirstOrDefault(professor =>
            new string(professor.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
        );

        if (professorEncontrado != null)
        {
            Console.WriteLine($"\nAdicionando turmas para: {professorEncontrado.Nome}");
            Console.WriteLine("Digite o nome da turma (ou digite -1 para parar):");

            while (true)
            {
                Console.Write("Turma: ");
                string turma = Console.ReadLine()!;

                if (turma == "-1") break;

                if (string.IsNullOrWhiteSpace(turma))
                {
                    Console.WriteLine("O nome da turma não pode estar vazio!\n");
                }
                else if (turma.Any(char.IsDigit))
                {
                    Console.WriteLine("Erro: O nome da turma não pode conter números.\n");
                }
                else
                {
                    professorEncontrado.AdicionarTurma(turma);
                }
            }
            Console.WriteLine("\nTurmas atualizadas com sucesso!");
        }
        else
        {
            Console.WriteLine("\nProfessor não encontrado com esse CPF.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
    public static void AlterarSalario(List<Pessoa> listaPessoas)
    {
        Console.Clear();
        Console.WriteLine("--- ALTERAR SALÁRIO DE PROFESSOR ---");

        var professores = listaPessoas.OfType<Professor>().ToList();

        if (professores.Count == 0)
        {
            Console.WriteLine("\nNenhum professor cadastrado no sistema.");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Professores disponíveis:");
        foreach (var professor in professores)
        {
            professor.ExibirDados();
        }

        Console.Write("\nDigite o CPF do professor para alterar o salário: ");
        string cpfBusca = Console.ReadLine()!;

        string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());
        var professorEncontrado = professores.FirstOrDefault(professor =>
            new string(professor.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
        );

        if (professorEncontrado != null)
        {
            Console.WriteLine($"\nProfessor(a) selecionado(a): {professorEncontrado.Nome}");
            Console.WriteLine($"Salário Atual: R${professorEncontrado.Salario:F2}");

            double novoSalario;
            while (true)
            {
                Console.Write("\nDigite o novo salário: ");
                string entrada = Console.ReadLine()!;

                if (double.TryParse(entrada, out novoSalario))
                {
                    if (novoSalario >= 0)
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
            professorEncontrado.Salario = novoSalario;
            Console.WriteLine("\nSalário atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("\nProfessor não encontrado com esse CPF.");
        }

        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}