using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMoq.Infrastructure
{
    public interface IWeaponFactory
    {
        Weapon GetWeapon();
    }
}
