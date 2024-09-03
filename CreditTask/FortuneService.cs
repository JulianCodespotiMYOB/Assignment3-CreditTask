using System;
using System.Collections.Generic;
using System.Linq;

public class FortuneService
{
    private readonly List<Fortune> _fortunes;
    private readonly Random _random = new Random();
    public int TotalRequests { get; private set; }
    public Dictionary<string, int> CategoryStats { get; private set; }

    public FortuneService()
    {
        _fortunes = new List<Fortune>
        {
            new Fortune("A beautiful, smart, and loving person will be coming into your life.", "Love", 7),
            new Fortune("A dubious friend may be an enemy in camouflage.", "Friendship", 13),
            new Fortune("A faithful friend is a strong defense.", "Friendship", 2),
            new Fortune("A fresh start will put you on your way.", "Career", 9),
            new Fortune("A golden egg of opportunity falls into your lap this month.", "Finance", 8),
            new Fortune("Adventure can be real happiness.", "Personal Growth", 11),
            new Fortune("All your hard work will soon pay off.", "Career", 4)
        };
        CategoryStats = new Dictionary<string, int>();
    }

    public Fortune GetRandomFortune()
    {
        TotalRequests++;
        var fortune = _fortunes[_random.Next(_fortunes.Count)];

        if (!CategoryStats.ContainsKey(fortune.Category))
            CategoryStats[fortune.Category] = 0;
        CategoryStats[fortune.Category]++;

        return fortune;
    }

    public Dictionary<string, object> GetStats()
    {
        return new Dictionary<string, object>
        {
            { "TotalRequests", TotalRequests },
            { "CategoryStats", CategoryStats },
            { "MostPopularCategory", CategoryStats.OrderByDescending(x => x.Value).First().Key }
        };
    }
}