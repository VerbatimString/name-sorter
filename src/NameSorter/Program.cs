using NameSorter;
using NameSorter.Domain;
using NameSorter.Input;
using NameSorter.Output;
using NameSorter.Sorting;

const string OutputFileName = "sorted-names-list.txt";

if (args.Length != 1)
{
    var problem = args.Length == 0
        ? "No input file was given."
        : $"Expected 1 input file but got {args.Length}.";

    Console.Error.WriteLine($"Error: {problem}");
    Console.Error.WriteLine("Usage: name-sorter <input-file>");
    Console.Error.WriteLine("Example: name-sorter ./unsorted-names-list.txt");
    return 1;
}

try
{
    var app = new NameSorterApp(
        new FileNameSource(args[0]),
        new PersonNameParser(),
        new LastNameThenGivenNamesComparer(),
        [
            new ConsoleNameWriter(Console.Out), 
            new FileNameWriter(OutputFileName)
        ]
    );

    app.Run();
    return 0;
}

//intentionally limiting error catching to get stacktrace for actually unexpected errors.
catch (Exception ex) when (ex is 
    IOException or 
    UnauthorizedAccessException or 
    FormatException or 
    ArgumentException
) {
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}