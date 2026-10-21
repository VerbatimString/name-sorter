using NameSorter.Domain;
using NameSorter.Input;
using NameSorter.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter
{
    public class NameSorterApp
    {
        private readonly INameSource _source;
        private readonly PersonNameParser _parser;
        private readonly IComparer<PersonName> _comparer;
        private readonly List<INameWriter> _writers;

        public NameSorterApp(
            INameSource source,
            PersonNameParser parser,
            IComparer<PersonName> comparer,
            List<INameWriter> writers
        ) {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parser == null) throw new ArgumentNullException(nameof(parser));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (writers == null) throw new ArgumentNullException(nameof(writers));

            _source = source;
            _parser = parser;
            _comparer = comparer;
            _writers = writers;
        }

        /// <summary>
        /// Parses every line before writing anything, so one invalid line means
        /// nothing is written (no half-finished output file).
        /// </summary>
        public void Run()
        {
            var sortedNames = _source.ReadNames()
                .Select(_parser.Parse)
                .OrderBy(name => name, _comparer)
                .ToList();

            foreach (var writer in _writers)
            {
                writer.Write(sortedNames);
            }
        }
    }
}
