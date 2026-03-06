namespace Sprint_POO_CSharp.Modelos;

internal class Aluno : Pessoa
{
    private string Matricula { get; set; }
    private bool Situacao { get; set; }
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
            Console.WriteLine($"\nAção Negada: O aluno {Nome} está com a matrícula inativa e não pode receber notas. ");
            return;
        }
        if (nota >= 0 && nota <= 10)
        {
            Notas.Add(nota);
        }
        else
        {
            Console.WriteLine($"\nNota {nota} inválida!!! Use valores entre 0 e 10.");
        }
    }
    public double CalcularMedia() => Notas.Count > 0 ? Notas.Average() : 0;
    public override void ExibirDados()
    {
        string status = Situacao ? "Ativo" : "Inativo";
        Console.WriteLine($" {Nome,-22} | {CPF,-14} | {Matricula,-9} | {status,-7} | {CalcularMedia():F2}");
    }
    public static Aluno CadastrarAluno(List<Pessoa> listaPessoas)
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRAR NOVO ALUNO ---\n");
        Console.Write("Nome do Aluno: ");

        string nome = "";
        while (true)
        {
            Console.Write("Nome do Aluno: ");
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
                    Console.WriteLine("\nErro: Este CPF já está cadastrado no sistema para outra pessoa. Tente novamente.\n");
                }
                else
                {
                    cpf = apenasNumeros;
                    break;
                }
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
                bool matriculaJaExiste = listaPessoas.OfType<Aluno>().Any(aluno => aluno.Matricula == apenasNumerosMatricula);

                if (matriculaJaExiste)
                {
                    Console.WriteLine("\nErro: Esta matrícula já está cadastrada para outro aluno. Tente novamente.\n");
                }
                else
                {
                    matricula = apenasNumerosMatricula;
                    break;
                }
            }
            else
            {
                Console.WriteLine("\nMatrícula inválida! A matrícula deve conter exatamente 6 números. Tente novamente.");
            }
        }

        var aluno = new Aluno(nome, cpf, DateTime.Now, matricula, true);

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
    public static void AlterarSituacaoAluno(List<Pessoa> listaPessoas)
    {
        Console.Clear();
        Console.WriteLine("--- ALTERAR SITUAÇÃO DA MATRÍCULA (ATIVAR / TRANCAR) ---");

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

        Console.Write("\nDigite o CPF do aluno para alterar a situação: ");
        string cpfBusca = Console.ReadLine()!;

        string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());
        var alunoEncontrado = alunos.FirstOrDefault(aluno =>
            new string(aluno.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
        );

        if (alunoEncontrado != null)
        {
            alunoEncontrado.Situacao = !alunoEncontrado.Situacao;

            string statusAtual = alunoEncontrado.Situacao ? "Ativa" : "Inativa";
            Console.WriteLine($"\nSucesso! A matrícula de {alunoEncontrado.Nome} agora está: {statusAtual}");
        }
        else
        {
            Console.WriteLine("\nAluno não encontrado com esse CPF.");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}