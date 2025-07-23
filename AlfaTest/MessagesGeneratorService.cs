namespace AlfaTest;

public static class MessagesGeneratorService
{
    public static async Task SendNumbers()
    {
        while (true)
        {
            var numbers = GenerateNumbers();

            foreach (var number in numbers)
            {
                number.EntryTime = DateTime.Now;
                await KafkaProducerService.SendMessage(number);

                var delayMs = (int)(number.Number % 17);
                await Task.Delay(delayMs);

                await WaitingNextSecond();
            }
        }
    }

    private static IEnumerable<NumberInfo> GenerateNumbers()
    {
        var numbersInfo = new List<NumberInfo>();
        var random = new Random();

        while (numbersInfo.Count < 20)
        {
            var num = (ulong)random.Next(1, 999999999);

            numbersInfo.Add(new NumberInfo
            {
                Number = num,
                GeneratedTime = DateTime.UtcNow
            });
        }

        return numbersInfo;
    }
    
    private static async Task WaitingNextSecond()
    {
        var now = DateTime.UtcNow;
        var msToNextSecond = 1000 - now.Millisecond;
        if (msToNextSecond > 0)
            await Task.Delay(msToNextSecond);
    }
}