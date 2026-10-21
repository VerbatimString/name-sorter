using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Domain
{
    public class PersonNameParser
    {
        private const int MinWords = PersonName.MinGivenNames + 1;
        private const int MaxWords = PersonName.MaxGivenNames + 1;

        public PersonName Parse(string line)
        {
            if (line == null) throw new ArgumentNullException();
            var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < MinWords || words.Length > MaxWords)
            {
                throw new FormatException(
                    $"'{line.Trim()}' is not a valid name. " + 
                    $"Please ensure given name count is between " +
                    $"{PersonName.MinGivenNames} to {PersonName.MaxGivenNames}, " +
                    $"followed by a last name.");
            }

            var lastName = words[words.Length - 1];
            var givenNames = words.Take(words.Length - 1);

            return new PersonName(givenNames, lastName);
        }
    }
}
