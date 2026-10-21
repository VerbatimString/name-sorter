namespace NameSorter.Domain
{
    public class PersonNameParser
    {
        private const int MinWords = PersonName.MinGivenNames + 1;
        private const int MaxWords = PersonName.MaxGivenNames + 1;

        /// <summary>
        /// Turns a line of text such as "Adonis Julius Archer" into a <see cref="PersonName"/>.
        /// </summary>
        public PersonName Parse(string line)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));
            var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < MinWords || words.Length > MaxWords)
            {
                throw new FormatException(
                    $"'{line.Trim()}' is not a valid name. " + 
                    $"Please ensure given name count is between " +
                    $"{PersonName.MinGivenNames} and {PersonName.MaxGivenNames}, " +
                    $"followed by a last name.");
            }

            var lastName = words[words.Length - 1];
            var givenNames = words.Take(words.Length - 1);

            return new PersonName(givenNames, lastName);
        }
    }
}
