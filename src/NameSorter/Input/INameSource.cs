namespace NameSorter.Input
{
    public interface INameSource
    {
        IEnumerable<string> ReadNames();
    }
}
