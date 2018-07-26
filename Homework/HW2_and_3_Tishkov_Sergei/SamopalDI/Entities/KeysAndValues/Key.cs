using System;
using System.Collections.Generic;

namespace SamopalIndustries.Entities.KeysAndValues
{
    internal struct Key
    {
        internal Key(Type keyType, int example)
        {
            KeyType = keyType;
            Example = example;
        }

        internal Type KeyType { get; }

        internal int Example { get; }

        public override bool Equals(object obj) => obj is Key other && this.KeyType == other.KeyType && this.Example == other.Example;

        public override int GetHashCode()
        {
            var hashCode = -806118331;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(KeyType);
            hashCode = hashCode * -1521134295 + Example.GetHashCode();
            return hashCode;
        }
    }
}