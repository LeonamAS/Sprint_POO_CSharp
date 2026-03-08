namespace Sprint_POO_CSharp.Modelos;

internal class Aluno : Pessoa
{
    private string Matricula { get; set; }
    public bool Situacao { get; private set; }
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

        string nome = Pessoa.ObterNomeValido("Aluno");
        string cpf = Pessoa.ObterCpfValido(listaPessoas);
        DateTime dataNascimento = Pessoa.ObterDataNascimentoValida();

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

        var aluno = new Aluno(nome, cpf, dataNascimento, matricula, true);

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

        while (true)
        {
            Console.Write("\nDigite o CPF do aluno para adicionar notas (ou digite 0 para cancelar): ");
            string cpfBusca = Console.ReadLine()!;

            if (cpfBusca == "0")
            {
                Console.WriteLine("Operação cancelada.");
                break;
            }

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
                break;
            }
            else
            {
                Console.WriteLine("\nAluno não encontrado com esse CPF.");
            }
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

        while (true)
        {
            Console.Write("\nDigite o CPF do aluno para alterar a situação (ou digite 0 para cancelar): ");
            string cpfBusca = Console.ReadLine()!;

            if (cpfBusca == "0")
            {
                Console.WriteLine("Operação cancelada.");
                break;
            }

            string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());
            var alunoEncontrado = alunos.FirstOrDefault(aluno =>
                new string(aluno.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
            );

            if (alunoEncontrado != null)
            {
                alunoEncontrado.Situacao = !alunoEncontrado.Situacao;

                string statusAtual = alunoEncontrado.Situacao ? "Ativa" : "Inativa";
                Console.WriteLine($"\nSucesso! A matrícula de {alunoEncontrado.Nome} agora está: {statusAtual}");
                break;
            }
            else
            {
                Console.WriteLine("\nAluno não encontrado com esse CPF.");
            }
        }
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}