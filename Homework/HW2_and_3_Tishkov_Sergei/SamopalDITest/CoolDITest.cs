using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamopalIndustries;
using SamopalIndustries.Entities;
using SamopalIndustries.Entities.Exceptions;

namespace SamopalDITest
{
    [TestClass]
    public class CoolDITest
    {
        // Testing of simple invocation
        // Should return new Class3 variable, simple test
        [TestMethod]
        public void CoolDISeeCommentsIntTest01()
        {
            CoolDI di = new CoolDI();

            di.BindDefault<IClass3>().To<Class3>();
            var class3 = di.GetDefault<IClass3>();

            Assert.IsTrue(class3 is Class3);
        }

        // Testing of recursive invocation and invocation with delegate without args
        // Should return new Class1 variable, slightly more complex test
        [TestMethod]
        public void CoolDISeeCommentsIntTest02()
        {
            CoolDI di = new CoolDI();

            di.BindDefault<IClass1>().To<Class1>();
            di.BindDefault<IClass2>().To<Class2>();
            di.BindDefault<IClass3>().To<Class3>();
            di.BindDefault<IClass4>().To<Class4>();
            di.BindDefault<IClass5>().ToDelegateWOArgs(() => new Class5("Hey!"));
            var class1 = di.GetDefault<IClass1>();

            Assert.IsTrue(class1 is Class1 && class1.Class4.Class5.SomeString == "Hey!" && class1.Class2.Class3 != null);
        }

        // Testing of throwing exception
        // Should throw WrongParametersException because
        // args wasn't used in invocation of di.GetDefault<IClass1>()
        [TestMethod]
        public void CoolDISeeCommentsIntTest03()
        {
            CoolDI di = new CoolDI();

            di.BindDefault<IClass1>().To<Class1>();
            di.BindDefault<IClass2>().To<Class2>();
            di.BindDefault<IClass3>().To<Class3>();
            di.BindDefault<IClass4>().To<Class4>();
            di.BindDefault<IClass5>().ToDelegateWithArgs((args) => new Class5((string)args[0]));

            Assert.ThrowsException<WrongParametersException>(() => di.GetDefault<IClass1>());
        }

        // Testing of recursive invocation and invocation with delegate with args
        // Should return new Class4 variable
        [TestMethod]
        public void CoolDISeeCommentsIntTest04()
        {
            CoolDI di = new CoolDI();

            di.BindDefault<IClass4>().ToDelegateWithArgs((args) => new Class4(di.GetDefault<IClass5>(args)));
            di.BindDefault<IClass5>().ToDelegateWithArgs((args) => new Class5((string)args[0]));
            object[] array = new object[1] { "Hey!" };
            var class4 = di.GetDefault<IClass4>(array);

            Assert.IsTrue(class4 is Class4 && class4.Class5.SomeString == "Hey!");
        }

        // Testing of singleton invocation
        // All three variables should be the same object
        [TestMethod]
        public void CoolDISeeCommentsIntTest05()
        {
            CoolDI di = new CoolDI();

            di.BindDefaultAsSingleton<IClass5>().ToDelegateWithArgs((args) => new Class5((string)args[0]));
            object[] array = new object[1] { "Hey!" };
            var var1 = di.GetDefault<IClass5>(array);
            var var2 = di.GetDefault<IClass5>(array);
            var var3 = di.GetDefault<IClass5>(array);

            Assert.IsTrue(var1 is Class5 && var2 is Class5 && var3 is Class5 &&
                Equals(var1, var2) && Equals(var2, var3));
        }

        // Testing of default invocation
        // All three variables should NOT be the same object
        // This test will check that default is NOT singleton
        [TestMethod]
        public void CoolDISeeCommentsIntTest06()
        {
            CoolDI di = new CoolDI();

            di.BindDefault<IClass5>().ToDelegateWithArgs((args) => new Class5((string)args[0]));
            object[] array = new object[1] { "Hey!" };
            var var1 = di.GetDefault<IClass5>(array);
            var var2 = di.GetDefault<IClass5>(array);
            var var3 = di.GetDefault<IClass5>(array);

            Assert.IsTrue(var1 is Class5 && var2 is Class5 && var3 is Class5);
            Assert.IsFalse(Equals(var1, var2) || Equals(var2, var3));
        }
    }
}
