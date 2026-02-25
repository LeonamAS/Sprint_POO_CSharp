using Sprint_POO_CSharp.Modelos;

//Easy English
string mensagemDeBoasVindas = "Bem-vindo a Easy English";

//Dictionary<int, Menu> opcoes = new();
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

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para cadastrar um novo aluno");
    Console.WriteLine("Digite 2 para cadastrar um novo professor");
    Console.WriteLine("Digite 3 para mostrar lista de pessoas cadastradas");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    //if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
    //{
    //    Menu menuASerExibido = opcoes[opcaoEscolhidaNumerica];
    //    menuASerExibido.Executar();
    //    if (opcaoEscolhidaNumerica > 0) ExibirOpcoesDoMenu();
    //}
    //else
    //{
    //    Console.WriteLine("Opção inválida");
    //}
}

//var pessoas = new List<Pessoa>();

//var aluno1 = new Aluno("João", 123, new DateTime(2005, 5, 10), 01);
//aluno1.Notas.AddRange(new[] { 8.5, 7.0, 9.0 });

//var prof1 = new Professor("Dr. Smith", 456, new DateTime(1980, 1, 1), 5000.00);

//pessoas.Add(aluno1);
//pessoas.Add(prof1);

//Console.WriteLine("--- Listagem Acadêmica ---");
//foreach (var pessoa in pessoas)
//{
//    pessoa.ExibirDados();
//}

//double mediaGeralAlunos = pessoas.OfType<Aluno>().Average(a => a.CalcularMedia());
//Console.WriteLine($"\nMédia Geral dos Alunos: {mediaGeralAlunos:F2}");

ExibirOpcoesDoMenu();