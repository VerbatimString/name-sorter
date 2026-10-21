using NameSorter.Domain;
using NameSorter.Output;
using NameSorter.Sorting;
using NameSorter.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Tests
{
    public class NameSorterAppTests
    {
        private static NameSorterApp CreateApp(List<string> lines, List<INameWriter> writers)
        {
            return new NameSorterApp(
                new InMemoryNameSource(lines),
                new PersonNameParser(),
                new LastNameThenGivenNamesComparer(),
                writers);
        }

        [Test]
        public void Run_SendsSortedNamesToEveryWriter()
        {
            using var first = new StringWriter();
            using var second = new StringWriter();

            CreateApp(
                ["Janet Parsons", "Marin Alvarez", "Leo Gardner"],
                [new ConsoleNameWriter(first), new ConsoleNameWriter(second)]).Run();

            var nl = Environment.NewLine;
            var expected = $"Marin Alvarez{nl}Leo Gardner{nl}Janet Parsons{nl}";
            Assert.That(first.ToString(), Is.EqualTo(expected));
            Assert.That(second.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void Run_WritesNothingWhenSourceIsEmpty()
        {
            using var output = new StringWriter();
            
            List<INameWriter> writers = [
                new ConsoleNameWriter(output)
             ];
            
            CreateApp([], writers).Run();
            
            Assert.That(output.ToString(), Is.Empty);
        }

        [Test]
        public void Run_WritesNothingWhenAnyLineIsInvalid()
        {
            using var output = new StringWriter();

            List<INameWriter> writers = [
                new ConsoleNameWriter(output)
            ];

            var app = CreateApp(["Janet Parsons", "Cher"], writers);

            Assert.Throws<FormatException>(() => app.Run());
            Assert.That(output.ToString(), Is.Empty);
        }
    }
}
