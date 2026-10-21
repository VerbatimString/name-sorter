namespace NameSorter.Input
{
    public class FileNameSource : INameSource
    {
        private readonly string _filePath;
        
        public FileNameSource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A file path is required.", nameof(filePath));
            }

            _filePath = filePath;
        }

        public IEnumerable<string> ReadNames()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"Could not find the input file '{_filePath}'.", _filePath);
            }

            return File
                .ReadAllLines(_filePath)
                .Where(line => 
                    !string.IsNullOrWhiteSpace(line)
                );
        }
    }
}
