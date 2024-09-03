using System.Drawing;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FortuneService>();

var app = builder.Build();

app.MapGet("/", async (HttpContext context, FortuneService fortuneService) =>
{
    var fortune = fortuneService.GetRandomFortune();
    await context.Response.WriteAsync($@"
    <!DOCTYPE html>
    <html>
    <head>
        <title>Enhanced Fortune Cookie</title>
        <style>
            body {{font - family: Arial, sans-serif; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; background-color: #f0f0f0; }}
            .cookie {{background - color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1); text-align: center; }}
            h1 {{color: #333; }}
            p {{color: #666; }}
            button {{background - color: #4CAF50; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin: 5px; }}
        </style>
        <script>
            async function getNewFortune() {{
                const response = await fetch('/api/fortune');
                const fortune = await response.json();
                document.getElementById('message').textContent = fortune.message;
                document.getElementById('category').textContent = fortune.category;
                document.getElementById('luckyNumber').textContent = fortune.luckyNumber;
            }}

            async function getStats() {{
                const response = await fetch('/api/stats');
                const stats = await response.json();
                alert(`Total Requests: ${{stats.totalRequests}}\nMost Popular Category: ${{stats.mostPopularCategory}}`);
            }}
        </script>
    </head>
    <body>
        <div class=""cookie"">
            <h1>Your Enhanced Fortune Cookie</h1>
            <p id=""message"">{fortune.Message}</p>
            <p>Category: <span id=""category"">{fortune.Category}</span></p>
            <p>Lucky Number: <span id=""luckyNumber"">{fortune.LuckyNumber}</span></p>
            <button onclick=""getNewFortune()"">Get Another Fortune</button>
            <button onclick=""getStats()"">View Stats</button>
        </div>
    </body>
    </html>
    ");
});

app.MapGet("/api/fortune", (FortuneService fortuneService) =>
{
    var fortune = fortuneService.GetRandomFortune();
    return Results.Json(new
    {
        message = fortune.Message,
        category = fortune.Category,
        luckyNumber = fortune.LuckyNumber
    });
});

app.MapGet("/api/stats", (FortuneService fortuneService) =>
{
    return Results.Json(fortuneService.GetStats());
});

app.Run();