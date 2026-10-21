using NameSorter.Input;

var source = new FileNameSource(args[0]);

foreach (var line in source.ReadNames())
{
    Console.WriteLine(line);
}