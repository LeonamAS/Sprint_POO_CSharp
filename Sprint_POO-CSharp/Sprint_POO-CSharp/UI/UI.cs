namespace Sprint_POO_CSharp.UI;

public static class UI
{
    public static void ExibirErro(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n" + mensagem + "\n");
        Console.ResetColor();
    }
    public static void ExibirAviso(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n" + mensagem + "\n");
        Console.ResetColor();
    }
    public static void ExibirSucesso(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n" + mensagem + "\n");
        Console.ResetColor();
    }
    public static void Pausar()
    {
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}