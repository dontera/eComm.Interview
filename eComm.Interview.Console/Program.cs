using eComm.Interview.ConsoleApp;

internal class Program
{
    
    private static async Task Main(string[] args)
    {
        Console.WriteLine("eComm.Interview startup");

        // get DataFile
        App apprunner = new App();

        apprunner.GetDataFilePathInput();

        if (await apprunner.LoadDataFile())
        {
            Console.WriteLine($"Parsed Data File, got {apprunner.GetRecordCount()} records");

            apprunner.GetArguments();

            await apprunner.GetDataResult();
        }
        else
            Console.WriteLine("Data File not loaded, exiiting");

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("eComm.Interview exiting");
    }
}