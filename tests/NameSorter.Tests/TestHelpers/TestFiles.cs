    using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Tests.TestHelpers
{
    public static class TestFiles
    {
        public static string NewTempPath() =>
            Path.Combine(Path.GetTempPath(), $"name-sorter-{Guid.NewGuid():N}.txt");

        public static string CreateTempFile(List<string> lines)
        {
            var path = NewTempPath();
            File.WriteAllLines(path, lines);
            return path;
        }
    }

}
