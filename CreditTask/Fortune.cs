public class Fortune
{
    public string Message { get; set; }
    public string Category { get; set; }
    public int LuckyNumber { get; set; }

    public Fortune(string message, string category, int luckyNumber)
    {
        Message = message;
        Category = category;
        LuckyNumber = luckyNumber;
    }
}