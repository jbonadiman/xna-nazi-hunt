namespace NaziHunt;

public static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static void Main(string[] args)
    {
        using Game game = new();

        game.Run();
    }
}
