using NameSorter.Domain;

namespace NameSorter.Tests.Domain
{
    public class PersonNameParserTests
    {
        private readonly PersonNameParser _parser = new();

        [TestCase("Janet Parsons", new[] { "Janet" }, "Parsons")]
        [TestCase("Adonis Julius Archer", new[] { "Adonis", "Julius" }, "Archer")]
        [TestCase("Hunter Uriah Mathew Clarke", new[] { "Hunter", "Uriah", "Mathew" }, "Clarke")]
        public void Parse_TreatsLastWordAsLastNameAndTheRestAsGivenNames(
            string line, string[] expectedGivenNames, string expectedLastName
        ) {
            var name = _parser.Parse(line);
            Assert.That(name.GivenNames, Is.EqualTo(expectedGivenNames));
            Assert.That(name.LastName, Is.EqualTo(expectedLastName));
        }

        [TestCase("  Janet   Parsons  ")]
        [TestCase("Janet\tParsons")]
        public void Parse_IgnoresExtraWhitespace(string line)
        {
            var name = _parser.Parse(line);
            Assert.That(name.ToString(), Is.EqualTo("Janet Parsons"));
        }


        [TestCase("")]
        [TestCase("   ")]
        [TestCase("Cher")]
        [TestCase("One Two Three Four Five")]
        public void Parse_RejectsLinesThatAreNotValidNames(string line)
        {
            Assert.Throws<FormatException>(() => _parser.Parse(line));
        }

        [Test]
        public void Parse_ErrorMessageIncludesTheInvalidLine()
        {
            var exception = Assert.Throws<FormatException>(() => _parser.Parse("Cher"));
            Assert.That(exception.Message, Does.Contain("Cher"));
        }
    }
}
