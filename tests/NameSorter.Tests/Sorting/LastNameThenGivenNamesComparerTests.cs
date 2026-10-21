using NameSorter.Domain;
using NameSorter.Sorting;

namespace NameSorter.Tests.Sorting
{
    public class LastNameThenGivenNamesComparerTests
    {
        private readonly LastNameThenGivenNamesComparer _comparer = new();
        private readonly PersonNameParser _parser = new();

        [TestCase("Zoe Adams", "Anna Brown", 
            TestName = "Pair: different last names, last name decides")]
        [TestCase("Anna Smith", "Beth Smith",
            TestName = "Pair: same last name, first given name decides")]
        [TestCase("Anna Beth Smith", "Anna Carol Smith",
            TestName = "Pair: same last name and first given name, second given name decides")]
        [TestCase("Leo Gardner", "Leo James Gardner",
            TestName = "Pair: all shared names match, fewer given names comes first")]
        [TestCase("anna smith", "Beth Smith",
            TestName = "Pair: lowercase vs uppercase, case is ignored")]
        public void Compare_SinglePair_PutsNamesInExpectedOrder(string shouldComeFirst, string shouldComeSecond)
        {
            var first = _parser.Parse(shouldComeFirst);
            var second = _parser.Parse(shouldComeSecond);

            Assert.That(
                _comparer.Compare(first, second), 
                Is.LessThan(0),
                $"Expected '{shouldComeFirst}' to sort before '{shouldComeSecond}'."
            );
            
            Assert.That(
                _comparer.Compare(second, first), 
                Is.GreaterThan(0),
                $"Expected '{shouldComeSecond}' to sort after '{shouldComeFirst}'."
            );
        }

        [TestCase("Janet Parsons", "Janet Parsons", TestName = "Pair: identical names, matching case")]
        [TestCase("Janet Parsons", "JANET PARSONS", TestName = "Pair: identical names, different case")]
        public void Compare_MatchingNames_ReturnsZero(string first, string second)
        {
            var result = _comparer.Compare(_parser.Parse(first), _parser.Parse(second));

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Compare_NullComesBeforeAnyName()
        {
            var name = _parser.Parse("Janet Parsons");

            Assert.That(_comparer.Compare(null, name), Is.LessThan(0));
            Assert.That(_comparer.Compare(name, null), Is.GreaterThan(0));
        }

        [Test]
        public void Sorting_ExampleFromBrief_ProducesExpectedOrder()
        {
            var unsorted = new[]
            {
                "Janet Parsons", "Vaugh Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder",
                "Marin Alvarez", "London Lindsey", "Beau Tristan Bentley", "Leo Gardner",
                "Hunter Uriah Mathew Clarke", "Mikayla Lopez", "Frankie Conner Ritter",
            };

            var sorted = unsorted
                .Select(_parser.Parse)
                .OrderBy(name => name, _comparer)
                .Select(name => name.ToString());

            Assert.That(sorted, Is.EqualTo(new[]
            {
                "Marin Alvarez", "Adonis Julius Archer", "Beau Tristan Bentley", "Hunter Uriah Mathew Clarke",
                "Leo Gardner", "Vaugh Lewis", "London Lindsey", "Mikayla Lopez",
                "Janet Parsons", "Frankie Conner Ritter", "Shelby Nathan Yoder",
            }));
        }
    }
}
