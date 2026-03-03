using Sprint_POO_CSharp.Modelos;

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
    Console.WriteLine("3. Inserir Notas dos Alunos");
    Console.WriteLine("4. Definir Turmas dos Professores");
    Console.WriteLine("5. Listar Todos e Estatísticas");
    Console.WriteLine("0. Sair");
    Console.Write("Escolha uma opção: ");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            var novoAluno = Aluno.CadastrarAluno();
            pessoas.Add(novoAluno);
            FinalizarCadastro("Aluno");
            break;
        case "2":
            pessoas.Add(Professor.CadastrarProfessor());
            FinalizarCadastro("Professor");
            break;
        case "3":
            Aluno.InserirNotas(pessoas);
            break;
        case "4":
            Professor.DefinirTurmas(pessoas);
            break;
        case "5":
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