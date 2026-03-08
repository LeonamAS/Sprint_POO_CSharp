namespace Sprint_POO_CSharp.Modelos;

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
                Console.WriteLine($"Erro: O nome deve ter pelo menos {tamanhoMinimo} caracteres e não pode ser vazio. Tente novamente.\n");
            }
            else if (nome.Any(char.IsDigit))
            {
                Console.WriteLine("Erro: O nome não pode conter números. Tente novamente.\n");
            }
            else if (nome.Any(caractere => !char.IsLetter(caractere) && !char.IsWhiteSpace(caractere)))
            {
                Console.WriteLine("Erro: O nome não pode conter caracteres especiais (ex: @, #, !). Tente novamente.\n");
            }
            else
            {
                break;
            }
        }
        return nome.Trim();
    }
    public static string ObterCpfValido(List<Pessoa> listaPessoas)
    {
        string cpf = "";
        while (true)
        {
            Console.Write("CPF (somente números, 11 dígitos): ");
            string entradaCpf = Console.ReadLine()!;
            string apenasNumeros = new string(entradaCpf.Where(char.IsDigit).ToArray());

            if (apenasNumeros.Length == 11)
            {
                bool cpfJaExiste = listaPessoas.Any(pessoa => new string(pessoa.CPF.Where(char.IsDigit).ToArray()) == apenasNumeros);

                if (cpfJaExiste)
                {
                    Console.WriteLine("Erro: Este CPF já está cadastrado no sistema para outra pessoa. Tente novamente.\n");
                }
                else
                {
                    cpf = apenasNumeros;
                    break;
                }
            }
            else
            {
                Console.WriteLine("\nCPF inválido! O CPF deve conter exatamente 11 números. Tente novamente.");
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
                    Console.WriteLine("Erro: A data de nascimento não pode ser no futuro. Tente novamente.\n");
                }
                else if (dataNascimento < DateTime.Now.AddYears(-100))
                {
                    Console.WriteLine("Erro: Idade inválida. O sistema não permite cadastros com mais de 100 anos. Tente novamente.\n");
                }
                else
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Formato de data inválido. Use o formato dd/mm/aaaa (ex: 15/05/2000).\n");
            }
        }
        return dataNascimento;
    }
}