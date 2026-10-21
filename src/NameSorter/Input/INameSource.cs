using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Input
{
    public interface INameSource
    {
        IEnumerable<string> ReadNames();
    }
}
