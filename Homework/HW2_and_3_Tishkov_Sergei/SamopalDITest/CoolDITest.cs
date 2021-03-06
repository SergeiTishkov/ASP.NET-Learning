﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamopalIndustries;
using SamopalIndustries.Entities;
using SamopalIndustries.Entities.Enums;
using SamopalIndustries.Entities.Exceptions;

namespace SamopalDITest
{
    [TestClass]
    public class CoolDITest
    {
        // Testing of simple invocation
        // Must return new Class3 variable, simple test
        [TestMethod]
        public void CoolDISeeCommentsIntTest01()
        {
            CoolDI di = new CoolDI();

            di.BindDefault<IClass3>().To<Class3>();
            var class3 = di.GetDefault<IClass3>();

            Assert.IsTrue(class3 is Class3);
        }

        // Testing of recursive invocation and invocation with delegate without args
        // Must return new Class1 variable, slightly more complex test
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
        // Must throw WrongParametersException because
        // parameter "args" wasn't used in invocation of di.GetDefault<IClass1>() and in binding of IClass4
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
        // Must return new Class4 variable
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
        // All three variables must be the same object
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
        // All three variables must NOT be the same object
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

        // Testing of throwing ArgumentException
        // Must throw this exception because 0 must not be used as example
        [TestMethod]
        public void CoolDISeeCommentsIntTest07()
        {
            CoolDI di = new CoolDI();

            Assert.ThrowsException<ArgumentException>(() => di.BindExample<Class5>(0).ToSelf());
        }

        // Testing of throwing ArgumentException
        // Must throw this exception because there wasn't default binding of IClass3
        [TestMethod]
        public void CoolDISeeCommentsIntTest08()
        {
            CoolDI di = new CoolDI();

            Assert.ThrowsException<UnbindedTypeException>(() => di.GetDefault<Class3>());
        }

        // Testing of throwing ArgumentException
        // Must throw this exception because there wasn't example 1 binding of IClass3
        [TestMethod]
        public void CoolDISeeCommentsIntTest09()
        {
            CoolDI di = new CoolDI();

            Assert.ThrowsException<UnbindedTypeException>(() => di.GetExample<Class3>(1));
        }

        // Testing late binding of unbinded types and recursive invocation of unbinded types
        [TestMethod]
        public void CoolDISeeCommentsIntTest10()
        {
            CoolDI di = new CoolDI(LateBindingOptions.MaxCtor, true);

            di.BindDefault<Class10>().ToDelegateWOArgs(() => new Class10("Hey!"));
            var class6 = di.GetDefault<Class6>();

            Assert.IsTrue(class6 is Class6 && class6.Class9.Class10.SomeString == "Hey!" && class6.Class7.Class8 != null);
        }
    }
}
