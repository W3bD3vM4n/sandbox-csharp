using Microsoft.AspNetCore;

// 1. CALL THE ENTRY POINT
namespace ApiLoopThree;

public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }
    
    // The Startup.cs it's called here
    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
}