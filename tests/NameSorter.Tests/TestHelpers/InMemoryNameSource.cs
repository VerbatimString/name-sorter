using NameSorter.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Tests.TestHelpers
{
    /// <summary>
    /// A name source backed by a list, so app tests don't need files.
    /// Stands in for FileNameSource in tests.    
    /// </summary>
    public class InMemoryNameSource : INameSource
    {
        private readonly List<string> _lines;
        public InMemoryNameSource(List<string> lines)
        {
            _lines = lines;
        }
        public IEnumerable<string> ReadNames() => _lines;
    }
}
