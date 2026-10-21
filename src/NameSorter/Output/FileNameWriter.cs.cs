using NameSorter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Output
{
    /// <summary>Writes one name per line to a file, overwriting it if it already exists.</summary>
    public class FileNameWriter : INameWriter
    {
        private readonly string _writePath;

        /// <param name="writePath">
        /// The file to write to. If a file already exists at this path, it is overwritten.
        /// </param>
        public FileNameWriter(string writePath)
        {
            if (string.IsNullOrWhiteSpace(writePath))
            {
                throw new ArgumentException("A file path is required to write to.", nameof(writePath));
            }

            _writePath = writePath;
        }

        /// <summary>
        /// Writes the names to the file at the path given in the constructor, replacing any existing content.
        /// </summary>
        public void Write(IReadOnlyList<PersonName> names)
        {
            File.WriteAllLines(_writePath, names.Select(name => name.ToString()));
        }
    }
}
