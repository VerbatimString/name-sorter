using NameSorter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Output
{
    public interface INameWriter
    {
        void Write(IReadOnlyList<PersonName> names);
    }
}
