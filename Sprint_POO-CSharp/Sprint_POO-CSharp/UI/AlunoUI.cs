using Sprint_POO_CSharp.Modelos;

namespace Sprint_POO_CSharp.UI;

internal class AlunoUI
{
    public static Aluno CadastrarAluno(List<Aluno> alunos, List<Professor> professores)
    {
        Console.Clear();
        Console.WriteLine("--- CADASTRAR NOVO ALUNO ---\n");

        string nome = PessoaUI.ObterNomeValido("Aluno");
        string cpf = PessoaUI.ObterCpfValido(alunos, professores);
        DateTime dataNascimento = PessoaUI.ObterDataNascimentoValida();
        string matricula = GerarMatricula(alunos);
        UI.ExibirAviso($"Matrícula gerada para o aluno: {matricula}");

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
                UI.ExibirErro("Valor inválido. Digite um número.");
            }
        }
        return aluno;
    }
    public static void InserirNotas(List<Aluno> alunos)
    {
        Console.Clear();
        Console.WriteLine("--- INSERIR NOTAS DE ALUNO EXISTENTE ---");

        if (alunos.Count == 0)
        {
            UI.ExibirAviso("\nNenhum aluno cadastrado no sistema.");
            UI.Pausar();
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
                UI.ExibirAviso("Operação cancelada.");
                break;
            }

            string numerosBusca = new string(cpfBusca.Where(char.IsDigit).ToArray());

            var alunoEncontrado = alunos.FirstOrDefault(aluno =>
                new string(aluno.CPF.Where(char.IsDigit).ToArray()) == numerosBusca
            );

            if (alunoEncontrado != null)
            {
                UI.ExibirAviso($"\nAdicionando notas para: {alunoEncontrado.Nome}");
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
                        UI.ExibirErro("Valor inválido. Digite um número.");
                    }
                }
                UI.ExibirSucesso("Notas atualizadas com sucesso!");
                break;
            }
            else
            {
                UI.ExibirErro("Aluno não encontrado com esse CPF.");
            }
        }
        UI.Pausar();
    }
    public static void AlterarSituacaoAluno(List<Aluno> alunos)
    {
        Console.Clear();
        Console.WriteLine("--- ALTERAR SITUAÇÃO DA MATRÍCULA (ATIVAR / TRANCAR) ---");

        if (alunos.Count == 0)
        {
            UI.ExibirAviso("Nenhum aluno cadastrado no sistema.");
            UI.Pausar();
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
                UI.ExibirAviso("Operação cancelada.");
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
                UI.ExibirSucesso($"Sucesso! A matrícula de {alunoEncontrado.Nome} agora está: {statusAtual}");
                break;
            }
            else
            {
                UI.ExibirErro("Aluno não encontrado com esse CPF.");
            }
        }
        UI.Pausar();
    }
    private static string GerarMatricula(List<Aluno> alunos)
    {
        if (alunos.Count == 0)
        {
            return "000001";
        }
        int maiorMatricula = 0;

        foreach (var aluno in alunos)
        {
            if (int.TryParse(aluno.Matricula, out int numeroMatricula))
            {
                if (numeroMatricula > maiorMatricula)
                {
                    maiorMatricula = numeroMatricula;
                }
            }
        }
        int novaMatricula = maiorMatricula + 1;

        return novaMatricula.ToString("D6");
    }
}