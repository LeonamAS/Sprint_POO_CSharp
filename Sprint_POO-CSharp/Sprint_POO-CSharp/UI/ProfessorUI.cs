using Sprint_POO_CSharp.Modelos;

namespace Sprint_POO_CSharp.UI;

internal class ProfessorUI
{
    public static Professor CadastrarProfessor(List<Professor> professores, List<Aluno> alunos)
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRAR NOVO PROFESSOR ---\n");

        string nome = PessoaUI.ObterNomeValido("Professor");
        string cpf = PessoaUI.ObterCpfValido(alunos, professores);
        DateTime dataNascimento = PessoaUI.ObterDataNascimentoValida();
        double salario = ObterSalarioValido();

        var prof = new Professor(nome, cpf, dataNascimento, salario);

        Console.WriteLine("Adicione as turmas do professor (-1 para sair)");
        while (true)
        {
            string turma = ObterNomeTurmaValido();
            if (turma == "-1") break;

            prof.AdicionarTurma(turma);
        }
        return prof;
    }
    public static void DefinirTurmas(List<Professor> professores)
    {
        Console.Clear();
        Console.WriteLine("--- DEFINIR TURMAS DE PROFESSOR EXISTENTE ---");

        if (professores.Count == 0)
        {
            UI.ExibirAviso("Nenhum professor cadastrado no sistema.");
            UI.Pausar();
            return;
        }

        Console.WriteLine("Professores disponíveis:");
        foreach (var professor in professores)
        {
            professor.ExibirDados();
        }

        while (true)
        {
            Console.Write("\nDigite o CPF do professor para adicionar turmas (ou digite 0 para cancelar): ");
            string cpfBusca = Console.ReadLine()!;

            if (cpfBusca == "0")
            {
                UI.ExibirAviso("Operação cancelada.");
                break;
            }

            string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());

            var professorEncontrado = professores.FirstOrDefault(professor =>
                new string(professor.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
            );

            if (professorEncontrado != null)
            {
                UI.ExibirAviso($"\nAdicionando turmas para: {professorEncontrado.Nome}");
                Console.WriteLine("Digite o nome da turma (ou digite -1 para parar):");

                while (true)
                {
                    string turma = ObterNomeTurmaValido();
                    if (turma == "-1") break;

                    professorEncontrado.AdicionarTurma(turma);
                }
                UI.ExibirSucesso("Turmas atualizadas com sucesso!");
                break;
            }
            else
            {
                UI.ExibirErro("Professor não encontrado com esse CPF.");
            }
        }
        UI.Pausar();
    }
    public static void AlterarSalario(List<Professor> professores)
    {
        Console.Clear();
        Console.WriteLine("--- ALTERAR SALÁRIO DE PROFESSOR ---");

        if (professores.Count == 0)
        {
            UI.ExibirAviso("Nenhum professor cadastrado no sistema.");
            UI.Pausar();
            return;
        }

        Console.WriteLine("Professores disponíveis:");
        foreach (var professor in professores)
        {
            professor.ExibirDados();
        }

        while (true)
        {
            Console.Write("\nDigite o CPF do professor para alterar o salário (ou digite 0 para cancelar): ");
            string cpfBusca = Console.ReadLine()!;

            if (cpfBusca == "0")
            {
                UI.ExibirAviso("Operação cancelada.");
                break;
            }
            string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());
            var professorEncontrado = professores.FirstOrDefault(professor =>
                new string(professor.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
            );

            if (professorEncontrado != null)
            {
                UI.ExibirAviso($"Professor(a) selecionado(a): {professorEncontrado.Nome}");
                Console.WriteLine($"Salário Atual: R${professorEncontrado.Salario:F2}");

                double novoSalario = ObterSalarioValido();

                professorEncontrado.Salario = novoSalario;
                UI.ExibirSucesso("Salário atualizado com sucesso!");
                break;
            }
            else
            {
                UI.ExibirErro("Professor não encontrado com esse CPF.");
            }
        }
        UI.Pausar();
    }
    private static string ObterNomeTurmaValido()
    {
        int tamanhoMinimo = 3;
        while (true)
        {
            Console.Write("Turma: ");
            string turma = Console.ReadLine()!;

            if (turma == "-1") return "-1";

            if (string.IsNullOrWhiteSpace(turma) || turma.Trim().Length < tamanhoMinimo)
            {
                UI.ExibirErro($"Erro: O nome da turma deve ter pelo menos {tamanhoMinimo} caracteres.");
            }
            else if (turma.Any(char.IsDigit))
            {
                UI.ExibirErro("Erro: O nome da turma não pode conter números.");
            }
            else if (turma.Any(caractere => !char.IsLetter(caractere) && !char.IsWhiteSpace(caractere)))
            {
                UI.ExibirErro("Erro: O nome da turma não pode conter caracteres especiais.");
            }
            else
            {
                return turma.Trim();
            }
        }
    }
    private static double ObterSalarioValido()
    {
        double salario;
        double tetoSalarial = 10000.00;
        double salarioMinimo = 1621.00;

        while (true)
        {
            Console.Write("Digite o salário (R$): ");
            string entrada = Console.ReadLine()!;

            if (double.TryParse(entrada, out salario))
            {
                if (salario < salarioMinimo)
                {
                    UI.ExibirErro($"Erro: O salário não pode ser menor que R$ {salarioMinimo:F2} ou negativo. Tente novamente.");
                }
                else if (salario > tetoSalarial)
                {
                    UI.ExibirErro($"Erro: O salário ultrapassa o teto permitido de R$ {tetoSalarial:F2}. Tente novamente.");
                }
                else
                {
                    break;
                }
            }
            else
            {
                UI.ExibirErro("Erro: Valor inválido. Digite apenas números (ex: 5500,00).");
            }
        }
        return salario;
    }
}