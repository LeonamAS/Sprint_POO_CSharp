using Sprint_POO_CSharp.Modelos;
using Sprint_POO_CSharp.UI;

List<Aluno> alunos = new List<Aluno>();
List<Professor> professores = new List<Professor>();
GerarDadosMockup(alunos, professores);
void ExibirLogo()
{
    Console.WriteLine(@"
███████╗ █████╗  ██████╗██╗   ██╗  ███████╗███╗  ██╗ ██████╗ ██╗     ██╗ ██████╗██╗  ██╗
██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝  ██╔════╝████╗ ██║██╔════╝ ██║     ██║██╔════╝██║  ██║
█████╗  ███████║╚█████╗  ╚████╔╝   █████╗  ██╔██╗██║██║  ██╗ ██║     ██║╚█████╗ ███████║
██╔══╝  ██╔══██║ ╚═══██╗  ╚██╔╝    ██╔══╝  ██║╚████║██║  ╚██╗██║     ██║ ╚═══██╗██╔══██║
███████╗██║  ██║██████╔╝   ██║     ███████╗██║ ╚███║╚██████╔╝███████╗██║██████╔╝██║  ██║
╚══════╝╚═╝  ╚═╝╚═════╝    ╚═╝     ╚══════╝╚═╝  ╚══╝ ╚═════╝ ╚══════╝╚═╝╚═════╝ ╚═╝  ╚═╝
");
}

bool executando = true;
while (executando)
{
    Console.Clear();
    ExibirLogo();
    Console.WriteLine("\n=================================================================================");
    Console.WriteLine("===                        SISTEMA DE GESTÃO ACADÊMICA                        ===");
    Console.WriteLine("=================================================================================\n");
    Console.WriteLine(" 1. Cadastrar Aluno");
    Console.WriteLine(" 2. Cadastrar Professor");
    Console.WriteLine(" 3. Inserir Notas dos Alunos");
    Console.WriteLine(" 4. Definir Turmas dos Professores");
    Console.WriteLine(" 5. Trancar / Ativar Matrícula de Aluno");
    Console.WriteLine(" 6. Alterar Salário de professor");
    Console.WriteLine(" 7. Listar Todos e Estatísticas");
    Console.WriteLine(" 0. Sair");
    Console.Write("\nEscolha uma opção: ");

    string opcao = Console.ReadLine()!;

    switch (opcao)
    {
        case "1":
            alunos.Add(AlunoUI.CadastrarAluno(alunos, professores));
            FinalizarCadastro("Aluno");
            break;
        case "2":
            professores.Add(ProfessorUI.CadastrarProfessor(professores, alunos));
            FinalizarCadastro("Professor");
            break;
        case "3":
            AlunoUI.InserirNotas(alunos);
            break;
        case "4":
            ProfessorUI.DefinirTurmas(professores);
            break;
        case "5":
            AlunoUI.AlterarSituacaoAluno(alunos);
            break;
        case "6":
            ProfessorUI.AlterarSalario(professores);
            break;
        case "7":
            ExibirRelatorios(professores, alunos);
            break;
        case "0":
            executando = false;
            break;
        default:
            UI.ExibirErro("Opção inválida!");
            Thread.Sleep(1000);
            break;
    }
}
static void FinalizarCadastro(string tipo)
{
    UI.ExibirSucesso($"{tipo} cadastrado com sucesso!");
    UI.Pausar();
}
static void ExibirRelatorios(List<Professor> professores, List<Aluno> alunos)
{
    Console.Clear();
    Console.WriteLine("\n===============================================================================");
    Console.WriteLine("                           RELATÓRIO GERAL DO SISTEMA                          ");
    Console.WriteLine("===============================================================================\n");

    // --- TABELA DE ALUNOS ---
    if (alunos.Any())
    {
        Console.WriteLine("----------------------------------- ALUNOS ------------------------------------");
        Console.WriteLine($" {"NOME",-22} | {"CPF",-14} | {"MATRÍCULA",-9} | {"STATUS",-7} | {"MÉDIA"}");
        Console.WriteLine("-------------------------------------------------------------------------------");

        foreach (var aluno in alunos)
        {
            aluno.ExibirDados();
        }

        Console.WriteLine("-------------------------------------------------------------------------------");
        
        var alunosAtivos = alunos.Where(aluno => aluno.Situacao == true).ToList();
        if (alunosAtivos.Any())
        {
            double mediaGeral = alunosAtivos.Average(aluno => aluno.CalcularMedia());
            Console.WriteLine($"MÉDIA GERAL DA ESCOLA (Apenas Ativos): {mediaGeral:F2}\n");
        }
        else
        {
            UI.ExibirAviso("MÉDIA GERAL DA ESCOLA: N/A (Nenhum aluno ativo no momento)\n");
        }
    }

    // --- TABELA DE PROFESSORES ---
    if (professores.Any())
    {
        Console.WriteLine("-------------------------------- PROFESSORES ----------------------------------");
        Console.WriteLine($" {"NOME",-22} | {"CPF",-14} | {"SALÁRIO",-11} | {"TURMAS"}");
        Console.WriteLine("-------------------------------------------------------------------------------");

        foreach (var professor in professores)
        {
            professor.ExibirDados();
        }
        Console.WriteLine("-------------------------------------------------------------------------------\n");
    }

    if (alunos.Count == 0 && professores.Count == 0)
    {
        UI.ExibirAviso("Nenhum registro encontrado no sistema.\n");
    }
    UI.Pausar();
}

// MÉTODO PARA MOCKUP DE DADOS
static void GerarDadosMockup(List<Aluno> alunos, List<Professor> professores)
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

    alunos.Add(aluno1);
    alunos.Add(aluno2);
    professores.Add(prof1);
    professores.Add(prof2);
}