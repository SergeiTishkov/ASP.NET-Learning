using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamopalIndustries.Entities.KeysAndValues
{
    internal class Value_CoolDI
    {
        internal Type Type { get; }

        internal Delegate Creator { get; }

        internal Value_CoolDI(Type type, Delegate creator)
        {
            Type = type;
            Creator = creator;
        }
    }
}
