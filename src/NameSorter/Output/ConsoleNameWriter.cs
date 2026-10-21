using NameSorter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Output
{
    /// <summary>
    /// Writes one name per line to a TextWriter.
    /// Callers can pass any TextWriter (Console.Out, StringWriter, etc.).
    /// </summary>
    public class ConsoleNameWriter : INameWriter
    {
        private readonly TextWriter _output;
        public ConsoleNameWriter(TextWriter output)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));

            _output = output;
        }

        public void Write(IReadOnlyList<PersonName> names)
        {
            foreach (var name in names)
            {
                _output.WriteLine(name);
            }
        }
    }
}
