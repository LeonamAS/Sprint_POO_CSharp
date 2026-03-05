using Sprint_POO_CSharp.Modelos;

string mensagemDeBoasVindas = "Bem-vindo a Easy English";

var pessoas = new List<Pessoa>();
GerarDadosMockup(pessoas);
void ExibirLogo()
{
    Console.WriteLine(@"
███████╗░█████╗░░██████╗██╗░░░██╗  ███████╗███╗░░██╗░██████╗░██╗░░░░░██╗░██████╗██╗░░██╗
██╔════╝██╔══██╗██╔════╝╚██╗░██╔╝  ██╔════╝████╗░██║██╔════╝░██║░░░░░██║██╔════╝██║░░██║
█████╗░░███████║╚█████╗░░╚████╔╝░  █████╗░░██╔██╗██║██║░░██╗░██║░░░░░██║╚█████╗░███████║
██╔══╝░░██╔══██║░╚═══██╗░░╚██╔╝░░  ██╔══╝░░██║╚████║██║░░╚██╗██║░░░░░██║░╚═══██╗██╔══██║
███████╗██║░░██║██████╔╝░░░██║░░░  ███████╗██║░╚███║╚██████╔╝███████╗██║██████╔╝██║░░██║
╚══════╝╚═╝░░╚═╝╚═════╝░░░░╚═╝░░░  ╚══════╝╚═╝░░╚══╝░╚═════╝░╚══════╝╚═╝╚═════╝░╚═╝░░╚═╝
");
    Console.WriteLine(mensagemDeBoasVindas);
}
bool executando = true;

while (executando)
{
    Console.Clear();
    ExibirLogo();
    Console.WriteLine("\n--- SISTEMA DE GESTÃO ACADÊMICA ---");
    Console.WriteLine("1. Cadastrar Aluno");
    Console.WriteLine("2. Cadastrar Professor");
    Console.WriteLine("3. Inserir Notas dos Alunos");
    Console.WriteLine("4. Definir Turmas dos Professores");
    Console.WriteLine("5. Trancar / Ativar Matrícula de Aluno");
    Console.WriteLine("6. Listar Todos e Estatísticas");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");

    string opcao = Console.ReadLine()!;

    switch (opcao)
    {
        case "1":
            pessoas.Add(Aluno.CadastrarAluno(pessoas));
            FinalizarCadastro("Aluno");
            break;
        case "2":
            pessoas.Add(Professor.CadastrarProfessor(pessoas));
            FinalizarCadastro("Professor");
            break;
        case "3":
            Aluno.InserirNotas(pessoas);
            break;
        case "4":
            Professor.DefinirTurmas(pessoas);
            break;
        case "5":
            Aluno.AlterarSituacaoAluno(pessoas);
            break;
        case "6":
            ExibirRelatorios(pessoas);
            break;
        case "0":
            executando = false;
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}
static void FinalizarCadastro(string tipo)
{
    Console.WriteLine($"\n{tipo} cadastrado com sucesso!");
    Console.WriteLine("Pressione qualquer tecla para voltar...");
    Console.ReadKey();
}
static void ExibirRelatorios(List<Pessoa> lista)
{
    Console.Clear();
    Console.WriteLine("\n--- LISTAGEM GERAL ---");
    foreach (var pessoa in lista)
    {
        pessoa.ExibirDados();
    }

    var alunos = lista.OfType<Aluno>().ToList();
    if (alunos.Any())
    {
        double mediaGeral = alunos.Average(a => a.CalcularMedia());
        Console.WriteLine($"\nMédia Geral de todos os alunos: {mediaGeral:F2}");
    }
    Console.WriteLine("Digite uma tecla para voltar ao menu principal");
    Console.ReadKey();
}

// MÉTODO PARA MOCKUP DE DADOS
static void GerarDadosMockup(List<Pessoa> lista)
{
    // --- MOCKUP DE ALUNOS ---
    var aluno1 = new Aluno("Ana Silva", "11111111111", new DateTime(2005, 3, 15), "000001", true);
    aluno1.AdicionarNota(8.5);
    aluno1.AdicionarNota(9.0);

    var aluno2 = new Aluno("Carlos Mendes", "22222222222", new DateTime(2004, 7, 22), "000002", false);
    aluno2.AdicionarNota(7.5);
    aluno2.AdicionarNota(6.0);

    // --- MOCKUP DE PROFESSORES ---
    var prof1 = new Professor("Fernanda Souza", "33333333333", new DateTime(1985, 10, 5), 5500.00);
    prof1.AdicionarTurma("Inglês Básico");
    prof1.AdicionarTurma("Inglês Intermediário");

    var prof2 = new Professor("Roberto Alves", "44444444444", new DateTime(1980, 1, 20), 6200.00);
    prof2.AdicionarTurma("Conversação Avançada");

    lista.Add(aluno1);
    lista.Add(aluno2);
    lista.Add(prof1);
    lista.Add(prof2);
}