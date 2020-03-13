using System;
using System.Collections.Generic;
using System.Text;

namespace cw2
{
    class OwnComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.FName} {x.LName} {x.Index}", $"{y.FName} {y.LName} {y.Index}");
        }

        public int GetHashCode(Student obj)
        {
            return StringComparer
                .OrdinalIgnoreCase
                .GetHashCode($"{obj.FName} {obj.LName} {obj.Index}");
        }
    }
}
