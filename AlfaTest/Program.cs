namespace AlfaTest;

internal static class Program
{
    public static async Task Main()
    {
        await MessagesGeneratorService.SendNumbers();
    }
}