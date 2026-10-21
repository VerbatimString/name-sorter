using NameSorter.Input;
using NameSorter.Tests.TestHelpers;

namespace NameSorter.Tests.Input
{
    public class FileNameSourceTests
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

        private string CreateTempFile(List<string> lines)
        {
            var path = TestFiles.CreateTempFile(lines);
            _createdFiles.Add(path);
            return path;
        }

        [Test]
        public void ReadNames_IgnoresBlankLines()
        {
            var path = CreateTempFile(["Janet Parsons", "", "   ", "Vaugh Lewis"]);
            var names = new FileNameSource(path).ReadNames();
            Assert.That(names, Is.EqualTo(new[] { "Janet Parsons", "Vaugh Lewis" }));
        }

        [TestCase("\n", TestName = "ReadNames_HandlesLinuxLineEndings")]
        [TestCase("\r\n", TestName = "ReadNames_HandlesWindowsLineEndings")]
        public void ReadNames_HandlesLineEndingsFromAnyOperatingSystem(string lineEnding)
        {
            var path = TestFiles.NewTempPath();
            _createdFiles.Add(path);
            File.WriteAllText(path, $"Janet Parsons{lineEnding}Leo Gardner{lineEnding}");

            var names = new FileNameSource(path).ReadNames();

            Assert.That(names, Is.EqualTo(new[] { "Janet Parsons", "Leo Gardner" }));
        }


        [Test]
        public void ReadNames_ReturnsNothingForEmptyFile()
        {
            var path = CreateTempFile([]);
            var names = new FileNameSource(path).ReadNames();
            Assert.That(names, Is.Empty);
        }

        [Test]
        public void ReadNames_ThrowsWhenFileDoesNotExist()
        {
            var source = new FileNameSource(TestFiles.NewTempPath());
            Assert.Throws<FileNotFoundException>(() => source.ReadNames());
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Constructor_RejectsBlankPath(string path)
        {
            Assert.Throws<ArgumentException>(() => new FileNameSource(path));
        }
    }
}
