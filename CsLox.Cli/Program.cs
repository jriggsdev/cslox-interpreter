using CsLox.Cli.Expressions;

namespace CsLox.Cli;

public static class Program
{
    private static bool _hadError = false;
    
    public static void Main(string[] args)
    {
        switch (args.Length)
        {
            case > 1:
                Console.WriteLine("Usage: cslox [script]");
                Environment.Exit(64);
                break;
            case 1:
                RunFile(args[0]);
                break;
            default:
                RunPrompt();
                break;
        }
    }

    private static void RunFile(string path)
    {
       var source = File.ReadAllText(path); 
       Run(source);

       if (_hadError)
       {
           Environment.Exit(65);
       }
    }

    private static void RunPrompt()
    {
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            
            if (input is null)
            {
                break;
            }

            Run(input);
            _hadError = false;
        } 
    }

    private static void Run(string source)
    {
        var scanner = new Scanner(source);
        var scanTokenResults = scanner.ScanTokens();

        foreach (var result in scanTokenResults)
        {
            if (result.IsOk)
            {
                var token = result.GetToken();
                Console.WriteLine(token);
            }
            else
            {
                var error = result.Error();
                Report(error.Line, error.Message);
            }
        }
    }
    
    private static void Report(int line, string message, string where = "")
    {
        Console.WriteLine($"[line {line}] Error {where}: {message}");
        _hadError = true;
    }
}