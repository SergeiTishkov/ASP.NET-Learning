using DemoMoq.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMOQ
{
    public class Player
    {
        private Weapon _weapon;
        private IWeaponFactory _factory;

        public string NickName { get; set; } = "Player";
        public int Health { get; set; } = 100;

        public Player(IWeaponFactory factory)
        {
            _factory = factory;
        }

        public void Attack(Player u)
        {
            _weapon = _factory.GetWeapon();
            u.Health -= _weapon.Damage;
        }
    }

    public class WeaponFactory : IWeaponFactory
    {
        public Weapon GetWeapon()
        {
            throw new NotImplementedException();
        }
    }

    

   
}
