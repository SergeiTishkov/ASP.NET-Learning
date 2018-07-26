using SamopalIndustries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamopalIndustries.Entities.KeysAndValues
{
    internal class KeyComparer : IEqualityComparer<Key>
    {
        public bool Equals(Key x, Key y) => x.KeyType == y.KeyType && x.Example == y.Example;

        public int GetHashCode(Key obj) => obj.GetHashCode();
    }
}
