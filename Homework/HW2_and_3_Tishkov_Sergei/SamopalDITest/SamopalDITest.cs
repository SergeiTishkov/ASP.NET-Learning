using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamopalIndustries;
using SamopalIndustries.Entities;
using SamopalIndustries.Entities.Exceptions;

namespace SamopalDITest
{
    [TestClass]
    public class SamopalDITest
    {
        // Integrational tests

        // Should throw ArguementException because class was not binded
        [TestMethod]
        public void SamopalDISeeCommentsIntTest01()
        {
            var di = new SamopalDI();

            // There wasn't Default Bind
            Assert.ThrowsException<ArgumentException>(() => di.GetDefault<IClass1>());

            // And there wasn't Example 1 Bind
            Assert.ThrowsException<ArgumentException>(() => di.GetExample<IClass1>(1));
        }

        // Should not throw any kind of exception if default bind or example bind is changed
        [TestMethod]
        public void SamopalDISeeCommentsIntTest02()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass3, Class3>();
            di.BindExample<IClass2>(1, (args) => new Class2(di.GetDefault<IClass3>()));

            IClass2 class2WithClass3 = di.GetExample<IClass2>(1, null);

            di.BindDefault<IClass3, AnotherClass3>();
            di.BindExample<IClass2>(1, (args) => new AnotherClass2(di.GetDefault<IClass3>()));

            IClass2 class2WithAnotherClass3 = di.GetExample<IClass2>(1, null);
        }

        // Should work good because class was binded correctly
        // Also should automatically get default binded arguments for late binding constructors
        [TestMethod]
        public void SamopalDISeeCommentsIntTest03()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass5>((args) => new Class5("Hello, Test!"));

            di.BindDefault<IClass2, Class2>();
            di.BindDefault<IClass3, Class3>();
            di.BindDefault<IClass4, Class4>();

            di.BindDefault<IClass1, Class1>();

            Type expected = typeof(Class1);
            IClass1 variable = di.GetDefault<IClass1>();
            Type actual = variable.GetType();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Hello, Test!", variable.Class4.Class5.SomeString);
        }

        // Should throw NullReferenseException because args in creator delegate of IClass5 default bind is null
        [TestMethod]
        public void SamopalDISeeCommentsIntTest04()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass5>((args) => new Class5((string)args[0]));

            di.BindDefault<IClass2, Class2>();
            di.BindDefault<IClass3, Class3>();
            di.BindDefault<IClass4, Class4>();

            di.BindDefault<IClass1, Class1>();

            Assert.ThrowsException<NullReferenceException>(() => di.GetDefault<IClass1>());
        }

        // Should work correctly because args in creator delegate wasn't null 
        // Testing of binding types to Func<object[], object>, i.e. with incoming args
        [TestMethod]
        public void SamopalDISeeCommentsIntTest05()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass5>((args) => new Class5((string)args[0]));

            di.BindDefault<IClass2, Class2>();
            di.BindDefault<IClass3, Class3>();
            di.BindDefault<IClass4>((args) =>
            {
                IClass5 c5 = di.GetDefault<IClass5>(args);
                return new Class4(c5);
            });

            di.BindDefault<IClass1>((args) =>
            {
                IClass2 c2 = di.GetDefault<IClass2>();
                IClass4 c4 = di.GetDefault<IClass4>(args);

                return new Class1(c2, c4);
            });

            object[] arrayOfArgs = new object[] { "Hello, Test!" };

            Type expected = typeof(Class1);
            IClass1 variable = di.GetDefault<IClass1>(arrayOfArgs);
            Type actual = variable.GetType();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Hello, Test!", variable.Class4.Class5.SomeString);
            Assert.AreNotEqual(null, variable.Class2.Class3);
        }

        // Should work correctly because args in creator delegate wasn't null 
        // Testing of binding types to Func<object>, i.e. without incoming args
        [TestMethod]
        public void SamopalDISeeCommentsIntTest06()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass5>(() => new Class5("Hello, Test!"));

            di.BindDefault<IClass2, Class2>();
            di.BindDefault<IClass3, Class3>();
            di.BindDefault<IClass4>(() =>
            {
                IClass5 c5 = di.GetDefault<IClass5>();
                return new Class4(c5);
            });

            di.BindDefault<IClass1>(() =>
            {
                IClass2 c2 = di.GetDefault<IClass2>();
                IClass4 c4 = di.GetDefault<IClass4>();

                return new Class1(c2, c4);
            });

            object[] arrayOfArgs = new object[] {  };

            Type expected = typeof(Class1);
            IClass1 variable = di.GetDefault<IClass1>(arrayOfArgs);
            Type actual = variable.GetType();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Hello, Test!", variable.Class4.Class5.SomeString);
            Assert.AreNotEqual(null, variable.Class2.Class3);
        }

        // Should throw an ArgumentException because example = 0 isn't available
        [TestMethod]
        public void SamopalDISeeCommentsIntTest07()
        {
            var di = new SamopalDI();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                int example = 0;
                di.BindExample<IClass5>(example, (args) => new Class5((string)args[0]));
            });
        }

        // Test of example binding, should throw any Exceptiom because 
        // all bindings will be correct, except this fact what
        // Class3's property Class4 is late binded by reflection
        // and auto binding by reflection occurs only by Defauld binding GetDefault()
        [TestMethod]
        public void SamopalDISeeCommentsIntTest08()
        {
            var di = new SamopalDI();

            di.BindExample<IClass5>(100500, (args) => new Class5((string)args[0]));

            // here is the source of problems
            // IClass3 is binding to example 1, not to Default,
            // and IClass2 isn't get IClass3 instance by correct delegate
            di.BindExample<IClass2, Class2>(1456);
            di.BindExample<IClass3, Class3>(3334445);
            di.BindExample<IClass4>(1, (args) =>
            {
                IClass5 c5 = di.GetExample<IClass5>(100500, args);
                return new Class4(c5);
            });

            di.BindExample<IClass1>(-100600, (args) =>
            {
                IClass2 c2 = di.GetExample<IClass2>(1456);
                IClass4 c4 = di.GetExample<IClass4>(1, args);

                return new Class1(c2, c4);
            });

            object[] arrayOfArgs = new object[] { "Hello, Test!" };

            Assert.ThrowsException<ArgumentException>(() => di.GetExample<IClass1>(-100600, arrayOfArgs));
        }

        // Test of example binding, very similar with 5 test,
        // should not throw any Exceptiom because 
        // all bindings will be correct
        [TestMethod]
        public void SamopalDISeeCommentsIntTest09()
        {
            var di = new SamopalDI();

            di.BindExample<IClass5>(100500, (args) => new Class5((string)args[0]));
            
            // now we'll get IClass4 example instance by correct delegate
            di.BindExample<IClass2>(1456, (args) =>
            {
                IClass3 class3 = di.GetExample<IClass3>(3334445);
                return new Class2(class3);
            });
            di.BindExample<IClass3, Class3>(3334445);

            di.BindExample<IClass4>(1, (args) =>
            {
                IClass5 c5 = di.GetExample<IClass5>(100500, args);
                return new Class4(c5);
            });

            di.BindExample<IClass1>(-100600, (args) =>
            {
                IClass2 c2 = di.GetExample<IClass2>(1456);
                IClass4 c4 = di.GetExample<IClass4>(1, args);

                return new Class1(c2, c4);
            });

            object[] arrayOfArgs = new object[] { "Hello, Test!" };

            Type expected = typeof(Class1);
            IClass1 variable = di.GetExample<IClass1>(-100600, arrayOfArgs);
            Type actual = variable.GetType();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual("Hello, Test!", variable.Class4.Class5.SomeString);
            Assert.AreNotEqual(null, variable.Class2.Class3);
        }

        // Should throw custom LateBindingException because we will try to invoke
        // Class 1 where isn't default ctor with LateBindingOptions.DefaultCtor.
        [TestMethod]
        public void SamopalDISeeCommentsIntTest10()
        {
            var di = new SamopalDI(LateBindingOptions.DefaultCtor);

            di.BindDefault<IClass1, Class1>();

            Assert.ThrowsException<LateBindingException>(() => di.GetDefault<IClass1>());
        }

        // Should not throw custom LateBindingException because
        // we will set LateBindingOptions to LateBindingOptions.DefaultOrMinCtor.
        // Also we will look if Class1 instance was created by minimal ctor
        // because it doesn't have default ctor
        [TestMethod]
        public void SamopalDISeeCommentsIntTest11()
        {
            var di = new SamopalDI(LateBindingOptions.DefaultOrMinCtor);

            di.BindDefault<IClass1, Class1>();
            di.BindDefault<IClass2, Class2>();

            var class1 = di.GetDefault<IClass1>();

            Assert.AreEqual(null, class1.Class4);
        }

        // Test of LateBindingOptions.DefaultOrMax
        // Will look if Class1 instance was created by maximal ctor
        // because it doesn't have default ctor
        [TestMethod]
        public void SamopalDISeeCommentsIntTest12()
        {
            var di = new SamopalDI(LateBindingOptions.DefaultOrMaxCtor);

            di.BindDefault<IClass1, Class1>();
            di.BindDefault<IClass2, Class2>();
            di.BindDefault<IClass3, Class3>();
            di.BindDefault<IClass4, Class4>();
            di.BindDefault<IClass5>((args) => new Class5(null));

            var class1 = di.GetDefault<IClass1>();

            Assert.AreNotEqual(null, class1.Class4);
        }

        // Should throw InvalidDelegateReturnTypeException <- name of exception can speak for itself
        [TestMethod]
        public void SamopalDISeeCommentsIntTest13()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass1>((args) => new Class3());

            Assert.ThrowsException<InvalidDelegateReturnTypeException>(() => di.GetDefault<IClass1>());
        }

        // Should throw NullReferenceException because class was binded to arguement delegate,
        // but was invoked without arguements
        [TestMethod]
        public void SamopalDISeeCommentsIntTest14()
        {
            var di = new SamopalDI();

            di.BindDefault<IClass5>((args) => new Class5((string)args[0]));

            Assert.ThrowsException<NullReferenceException>(() => di.GetDefault<IClass5>());
        }
    }

    public interface IClass1
    {
        IClass2 Class2 { get; }
        IClass4 Class4 { get; }
    }

    public interface IClass2
    {
        IClass3 Class3 { get; }
    }

    public interface IClass3
    {
    }

    public interface IClass4
    {
        IClass5 Class5 { get; }
    }

    public interface IClass5
    {
        string SomeString { get; }
    }

    public class Class1 : IClass1
    {
        public Class1(IClass2 class2)
        {
            Class2 = class2;
        }

        public Class1(IClass2 class2, IClass4 class4)
        {
            Class2 = class2;
            Class4 = class4;
        }

        public IClass2 Class2 { get; }

        public IClass4 Class4 { get; }
    }

    public class Class2 : IClass2
    {
        public Class2()
        {
        }

        public Class2(IClass3 class3)
        {
            Class3 = class3;
        }
        public IClass3 Class3 { get; }
    }

    public class AnotherClass2 : IClass2
    {
        public AnotherClass2(IClass3 class3)
        {
            Class3 = class3;
        }
        public IClass3 Class3 { get; }
    }

    public class Class3 : IClass3
    {
    }

    public class AnotherClass3 : IClass3
    {
    }

    public class Class4 : IClass4
    {
        public Class4()
        {
        }

        public Class4(IClass5 class5)
        {
            Class5 = class5;
        }

        public IClass5 Class5 { get; }
    }

    public class Class5 : IClass5
    {
        public Class5(string someString)
        {
            SomeString = someString;
        }

        public string SomeString { get; }
    }
}
