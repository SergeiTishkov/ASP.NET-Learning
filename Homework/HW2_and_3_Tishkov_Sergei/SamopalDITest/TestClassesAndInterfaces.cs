using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamopalDITest
{
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

    public class Class6
    {
        public Class6(Class7 class7)
        {
            Class7 = class7;
        }
        public Class6(Class7 class7, Class9 class9)
        {
            Class7 = class7;
            Class9 = class9;
        }
        public Class7 Class7 { get; }
        public Class9 Class9 { get; }
    }

    public class Class7
    {
        public Class7(Class8 class8)
        {
            Class8 = class8;
        }

        public Class8 Class8 { get; }
    }
    
    public class Class8
    {
        public Class8()
        {
        }
    }
    public class Class9
    {
        public Class9(Class10 class10)
        {
            Class10 = class10;
        }

        public Class10 Class10 { get; }
    }
    public class Class10
    {
        public Class10(string someString)
        {
            SomeString = someString;
        }

        public string SomeString { get; }
    }
}
