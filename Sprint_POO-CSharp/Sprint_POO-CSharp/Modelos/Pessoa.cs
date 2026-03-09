namespace Sprint_POO_CSharp.Modelos;

using Sprint_POO_CSharp.UI;

internal abstract class Pessoa
{
    public string Nome { get; set; }

    private string _cpf = "";

    public string CPF
    {
        get { return _cpf; }
        set { _cpf = FormatarCPF(value); }
    }

    protected DateTime DataDeNascimento { get; set; }

    public Pessoa(string nome, string cpf, DateTime dataDeNascimento)
    {
        Nome = nome;
        CPF = cpf;
        DataDeNascimento = dataDeNascimento;
    }

    public abstract void ExibirDados();
    private string FormatarCPF(string cpfDigitado)
    {
        if (string.IsNullOrWhiteSpace(cpfDigitado))
            return "CPF Não Informado";

        string apenasNumeros = new string(cpfDigitado.Where(char.IsDigit).ToArray());

        if (apenasNumeros.Length == 11)
        {
            return $"{apenasNumeros.Substring(0, 3)}.{apenasNumeros.Substring(3, 3)}.{apenasNumeros.Substring(6, 3)}-{apenasNumeros.Substring(9, 2)}";
        }

        return cpfDigitado;
    }
    public static string ObterNomeValido(string tipoPessoa)
    {
        int tamanhoMinimo = 3;
        string nome;
        while (true)
        {
            Console.Write($"Nome do {tipoPessoa}: ");
            nome = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length < tamanhoMinimo)
            {
                UI.ExibirErro($"Erro: O nome deve ter pelo menos {tamanhoMinimo} caracteres e não pode ser vazio. Tente novamente.");
            }
            else if (nome.Any(char.IsDigit))
            {
                UI.ExibirErro("Erro: O nome não pode conter números. Tente novamente.");
            }
            else if (nome.Any(caractere => !char.IsLetter(caractere) && !char.IsWhiteSpace(caractere)))
            {
                UI.ExibirErro("Erro: O nome não pode conter caracteres especiais (ex: @, #, !). Tente novamente.");
            }
            else
            {
                break;
            }
        }
        return nome.Trim();
    }
    public static string ObterCpfValido(List<Aluno> alunos, List<Professor> professores)
    {
        string cpf = "";
        while (true)
        {
            Console.Write("CPF (somente números, 11 dígitos): ");
            string entradaCpf = Console.ReadLine()!;
            string apenasNumeros = new string(entradaCpf.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length == 11)
            {
                bool cpfJaExiste = alunos.Any(a => new string(a.CPF.Where(char.IsDigit).ToArray()) == apenasNumeros) ||
                   professores.Any(p => new string(p.CPF.Where(char.IsDigit).ToArray()) == apenasNumeros);

                if (cpfJaExiste)
                {
                    UI.ExibirErro("Erro: Este CPF já está cadastrado no sistema para outra pessoa. Tente novamente.");
                }
                else
                {
                    cpf = apenasNumeros;
                    break;
                }
            }
            else
            {
                UI.ExibirErro("CPF inválido! O CPF deve conter exatamente 11 números. Tente novamente.");
            }
        }
        return cpf;
    }
    public static DateTime ObterDataNascimentoValida()
    {
        DateTime dataNascimento;
        while (true)
        {
            Console.Write("Data de Nascimento (dd/mm/aaaa): ");
            string entrada = Console.ReadLine()!;

            if (DateTime.TryParse(entrada, out dataNascimento))
            {
                if (dataNascimento > DateTime.Now)
                {
                    UI.ExibirErro("Erro: A data de nascimento não pode ser no futuro. Tente novamente.");
                }
                else if (dataNascimento < DateTime.Now.AddYears(-100))
                {
                    UI.ExibirErro("Erro: Idade inválida. O sistema não permite cadastros com mais de 100 anos. Tente novamente.");
                }
                else
                {
                    break;
                }
            }
            else
            {
                UI.ExibirErro("Formato de data inválido. Use o formato dd/mm/aaaa (ex: 15/05/2000).");
            }
        }
        return dataNascimento;
    }
}