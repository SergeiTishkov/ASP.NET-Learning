using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class TestModel : IEquatable<TestModel>
    {
        public TestModel() : this(null)
        {
        }

        public TestModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool Equals(TestModel other)
            =>
            string.Compare(this.Name, other.Name, StringComparison.InvariantCultureIgnoreCase) == 0;

        public override bool Equals(object obj)
            =>
            obj is TestModel other && Equals(other);

        public override int GetHashCode()
            =>
            539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
    }
}
