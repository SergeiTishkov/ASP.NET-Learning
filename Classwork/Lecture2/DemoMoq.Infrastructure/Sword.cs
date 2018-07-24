using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMoq.Infrastructure
{
    public class Sword : Weapon
    {
        public Sword()
        {
            Damage = 30;
            Name = "Holy Sword";
        }
    }
}
