using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamopalIndustries.Entities.KeysAndValues
{
    internal class Value
    {
        internal Type Type { get; }

        internal Func<object> CreatorWOArgs { get; }

        internal Func<object[], object> CreatorWithArgs { get; }

        internal Value(Type type, Func<object> creatorWOArgs, Func<object[], object> creatorWithArgs)
        {
            Type = type;
            CreatorWOArgs = creatorWOArgs;
            CreatorWithArgs = creatorWithArgs;
        }
    }
}
