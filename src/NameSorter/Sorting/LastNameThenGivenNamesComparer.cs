using NameSorter.Domain;

namespace NameSorter.Sorting
{
    public class LastNameThenGivenNamesComparer: IComparer<PersonName>
    {
        private static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

        /// <summary>
        /// Compares by last name. If last names match, compares each given name in order.
        /// If all shared given names match, the name with fewer given names comes first.
        /// </summary>
        /// <returns>Negative if x comes first, positive if y comes first, zero if equal.</returns>
        public int Compare(PersonName? x, PersonName? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            var byLastName = NameComparer.Compare(x.LastName, y.LastName);
            if (byLastName != 0) return byLastName;

            return CompareGivenNames(x.GivenNames, y.GivenNames);
        }

        private static int CompareGivenNames(IReadOnlyList<string> x, IReadOnlyList<string> y)
        {
            var sharedCount = Math.Min(x.Count, y.Count);

            for (var i = 0; i < sharedCount; i++)
            {
                var result = NameComparer.Compare(x[i], y[i]);
                if (result != 0) return result;
            }

            return x.Count.CompareTo(y.Count);
        }
    }
}
