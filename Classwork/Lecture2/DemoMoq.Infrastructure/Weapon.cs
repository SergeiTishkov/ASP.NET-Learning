using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMoq.Infrastructure
{
    public abstract class Weapon
    {
        public int Damage { get; protected set; }
        public string Name { get; protected set; }
    }
}
