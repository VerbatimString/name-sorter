using NameSorter.Domain;

namespace NameSorter.Tests.Domain
{
    public class PersonNameTests
    {

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Constructor_AllowsOneToThreeGivenNames_AsRequiredByBrief(int givenNameCount)
        {
            var givenNames = Enumerable.Repeat("Given", givenNameCount);
            var name = new PersonName(givenNames, "Last");
            Assert.That(name.GivenNames, Has.Count.EqualTo(givenNameCount));
        }

        [TestCase(0)]
        [TestCase(4)]
        public void Constructor_RejectsFewerThanOneOrMoreThanThreeGivenNames_AsRequiredByBrief(int givenNameCount)
        {
            var givenNames = Enumerable.Repeat("Given", givenNameCount);
            Assert.Throws<ArgumentException>(() => new PersonName(givenNames, "Last"));
        }


        [TestCase("")]
        [TestCase("   ")]
        public void Constructor_RejectsBlankLastName(string lastName)
        {
            Assert.Throws<ArgumentException>(() => new PersonName(new[] { "Janet" }, lastName));
        }

        [Test]
        public void Constructor_RejectsBlankGivenName()
        {
            Assert.Throws<ArgumentException>(() => new PersonName(new[] { "Janet", " " }, "Parsons"));
        }

        [Test]
        public void ToString_ReturnsGivenNamesThenLastName()
        {
            var name = new PersonName(new[] { "Hunter", "Uriah", "Mathew" }, "Clarke");
            Assert.That(name.ToString(), Is.EqualTo("Hunter Uriah Mathew Clarke"));
        }
    }
}
