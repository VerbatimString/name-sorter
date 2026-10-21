using NameSorter.Domain;
using NameSorter.Output;

namespace NameSorter.Tests.Output
{
    public class ConsoleNameWriterTests
    {
        [Test]
        public void Write_WritesOneNamePerLine()
        {
            var names = new List<PersonName>
            {
                new(["Marin"], "Alvarez"),
                new(["Adonis", "Julius"], "Archer"),
            };

            using var output = new StringWriter();

            new ConsoleNameWriter(output).Write(names);

            var nl = Environment.NewLine;
            Assert.That(output.ToString(), Is.EqualTo($"Marin Alvarez{nl}Adonis Julius Archer{nl}"));
        }

        [Test]
        public void Write_WritesNothingForEmptyList()
        {
            using var output = new StringWriter();

            new ConsoleNameWriter(output).Write([]);

            Assert.That(output.ToString(), Is.Empty);
        }
    }
}
