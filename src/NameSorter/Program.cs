using NameSorter.Domain;
using NameSorter.Input;
using NameSorter.Sorting;

var source = new FileNameSource(args[0]);
var parser = new PersonNameParser();
var comparer = new LastNameThenGivenNamesComparer();

var lines = source.ReadNames().ToList();

Console.WriteLine($"Printing unsorted data... ({args[0]}) \n");

foreach (var line in lines)
{
    Console.WriteLine(line);
}

var sortedNames = lines
    .Select(parser.Parse)
    .OrderBy(name => name, comparer);

Console.WriteLine();
Console.WriteLine("Printing sorted version... \n");
foreach (var name in sortedNames)
{
    Console.WriteLine(name);
}