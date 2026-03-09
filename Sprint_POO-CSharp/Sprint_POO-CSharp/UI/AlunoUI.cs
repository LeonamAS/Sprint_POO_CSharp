using Sprint_POO_CSharp.Modelos;

namespace Sprint_POO_CSharp.UI;

internal class AlunoUI
{
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
                    UI.ExibirErro("Erro: Esta matrícula já está cadastrada para outro aluno. Tente novamente.\n");
                }
                else
                {
                    matricula = apenasNumerosMatricula;
                    break;
                }
            }
            else
            {
                UI.ExibirErro("Matrícula inválida! A matrícula deve conter exatamente 6 números. Tente novamente.");
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
                UI.ExibirErro("Valor inválido. Digite um número.");
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
            UI.ExibirAviso("\nNenhum aluno cadastrado no sistema.");
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
            UI.ExibirAviso("Nenhum aluno cadastrado no sistema.");
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
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}
