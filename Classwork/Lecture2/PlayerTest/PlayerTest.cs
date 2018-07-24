using System;
using DemoMoq.Infrastructure;
using DemoMOQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace PlayerTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestAttack()
        {
            var factoryMoq = new Mock<IWeaponFactory>();
            factoryMoq.Setup(f => f.GetWeapon()).Returns(new Sword());
            var player1 = new Player(factoryMoq.Object) { NickName = "Player 1" };
            var player2 = new Player(factoryMoq.Object) { NickName = "Player 2" };

            player1.Attack(player2);

            factoryMoq.Verify(f => f.GetWeapon(), Times.Once);
            Assert.AreEqual(70, player2.Health);
        }

        [TestMethod]
        public void InjectorTestMethod()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IWeaponFactory>().To<WeaponFactory>();
            kernel.Bind<Player>().ToSelf();
            var player = kernel.Get<Player>();
        }
    }
}
