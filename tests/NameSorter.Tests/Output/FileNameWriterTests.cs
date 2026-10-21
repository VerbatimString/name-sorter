using NameSorter.Domain;
using NameSorter.Output;
using NameSorter.Tests.TestHelpers;

namespace NameSorter.Tests.Output
{
    public class FileNameWriterTests
    {
        private readonly List<string> _createdFiles = new();

        [TearDown]
        public void DeleteCreatedFiles()
        {
            foreach (var path in _createdFiles)
            {
                if (File.Exists(path)) File.Delete(path);
            }

            _createdFiles.Clear();
        }

        private string NewTempPath()
        {
            var path = TestFiles.NewTempPath();
            _createdFiles.Add(path);
            return path;
        }

        private string CreateTempFile(List<string> lines)
        {
            var path = TestFiles.CreateTempFile(lines);
            _createdFiles.Add(path);
            return path;
        }

        [Test]
        public void Write_WritesOneNamePerLine()
        {
            var path = NewTempPath();
            var names = new List<PersonName> 
            {
                new(["Marin"], "Alvarez"),
                new(["Adonis", "Julius"], "Archer"),
            };

            new FileNameWriter(path).Write(names);

            Assert.That(File.ReadAllLines(path), Is.EqualTo(new[] { "Marin Alvarez", "Adonis Julius Archer" }));
        }

        [Test]
        public void Write_OverwritesExistingFile()
        {
            var path = CreateTempFile(["Old Name", "Another Old Name"]);

            new FileNameWriter(path).Write([new(["Janet"], "Parsons")]);

            Assert.That(File.ReadAllLines(path), Is.EqualTo(new[] { "Janet Parsons" }));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Constructor_RejectsBlankPath(string path)
        {
            Assert.Throws<ArgumentException>(() => new FileNameWriter(path));
        }
    }
}
