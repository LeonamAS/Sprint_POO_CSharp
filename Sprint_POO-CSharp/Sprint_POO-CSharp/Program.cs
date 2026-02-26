using Sprint_POO_CSharp.Modelos;

//Easy English
string mensagemDeBoasVindas = "Bem-vindo a Easy English";

var pessoas = new List<Pessoa>();

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
    Console.WriteLine("3. Listar Todos e Estatísticas");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarAluno(pessoas);
            break;
        case "2":
            CadastrarProfessor(pessoas);
            break;
        case "3":
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
static void CadastrarAluno(List<Pessoa> lista)
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
    lista.Add(aluno);
    Console.WriteLine("Aluno cadastrado com sucesso!");
    Console.WriteLine("Digite uma tecla para voltar ao menu principal");
    Console.ReadKey();
}

static void CadastrarProfessor(List<Pessoa> lista)
{
    Console.Clear();
    Console.Write("Nome do Professor: ");
    string nome = Console.ReadLine();
    Console.Write("CPF: ");
    string cpf = Console.ReadLine();
    Console.Write("Salário: ");
    double salario = double.Parse(Console.ReadLine());

    var prof = new Professor(nome, cpf, DateTime.Now, salario);
    lista.Add(prof);
    Console.WriteLine("Professor cadastrado com sucesso!");
    Console.WriteLine("Digite uma tecla para voltar ao menu principal");
    Console.ReadKey();
}

static void ExibirRelatorios(List<Pessoa> lista)
{
    Console.Clear();
    Console.WriteLine("\n--- LISTAGEM GERAL ---");
    foreach (var p in lista)
    {
        p.ExibirDados();
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