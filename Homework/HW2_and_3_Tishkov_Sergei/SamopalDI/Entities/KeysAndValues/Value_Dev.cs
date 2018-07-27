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

        internal bool IsSingleton { get; }

        internal object Singleton { get; set; }

        internal Value_CoolDI(Type type, Delegate creator, bool isSingleton)
        {
            Type = type;
            Creator = creator;
            IsSingleton = isSingleton;
        }
    }
}
