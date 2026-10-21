using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Domain
{
    /// <summary>
    /// A person's name one to three given names followed by a single last name.
    /// The constructor enforces these rules, so an invalid PersonName cannot exist.
    /// </summary>
    public class PersonName
    {
        public const int MinGivenNames = 1;
        public const int MaxGivenNames = 3;

        public IReadOnlyList<string> GivenNames { get; }
        public string LastName { get; }

        public PersonName(IEnumerable<string> givenNames, string lastName)
        {
            if (givenNames == null) throw new ArgumentNullException();

            // copy, so later changes to the caller's list don't affect this one by reference.
            var givenNamesList = givenNames.ToList();
            
            if (givenNamesList.Count < MinGivenNames || givenNamesList.Count > MaxGivenNames)
            {
                throw new ArgumentException($"Given name is outside of required range of {MinGivenNames} to {MaxGivenNames}");
            }

            if(givenNamesList.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Given names cannot be blank.", nameof(givenNames));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be blank.", nameof(lastName));
            }

            GivenNames = givenNamesList.AsReadOnly();
            LastName = lastName;
        }

        public override string ToString() => $"{string.Join(' ', GivenNames)} {LastName}";
    }
}
