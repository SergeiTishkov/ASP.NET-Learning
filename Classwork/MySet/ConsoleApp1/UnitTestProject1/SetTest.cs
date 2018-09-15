using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySet;
using MySet.Exceptions;

namespace UnitTestProject1
{
    [TestClass]
    public class SetTest
    {
        [TestMethod]
        public void TestOfAdditionAndFindingWithoutExceptions() 
        {
            var set = new Set<TestModel>();

            var addingModel = new TestModel("Name1");
            set.Add(new TestModel("Name1"));

            var model1 = set.Find(new TestModel("Name1"));
            var model2 = set.Find(m => m.Name == "Name1");

            Assert.AreEqual(1, set.Count);
            Assert.AreEqual(addingModel, model1);
            Assert.AreEqual(addingModel, model2);
        }

        [TestMethod]
        public void TestOfCreationSetWithIEnumerableAsParameterInCtor()
        {
            var addingModel1 = new TestModel();
            var addingModel2 = new TestModel("Name1");

            var list = new List<TestModel>
            {
                addingModel1,
                addingModel2
            };

            var set = new Set<TestModel>(list);

            var model1 = set.Find(addingModel1);
            var model2 = set.Find(addingModel2);

            Assert.AreEqual(2, set.Count);
            Assert.AreEqual(addingModel1, model1);
            Assert.AreEqual(addingModel2, model2);
        }

        [TestMethod]
        public void TestOfEventInvocation()
        {
            var set = new Set<TestModel>();

            var addingModel1 = new TestModel();
            var addingModel2 = new TestModel("Name1");

            set.ItemAdded += (s, e) => 
            {
                foreach (var item in e.Args)
                {
                    Debug.WriteLine(item);
                }
            };

            set.ItemRemoved += (s, e) => Debug.WriteLine("Item successfully removed!!!");

            set.Add(addingModel1, new SetEventArgs(new object[] { 123, "Success!", new List<int>(), Environment.NewLine }));
            set.Add(addingModel2, new SetEventArgs(new object[] { "Success again!" }));

            set.Remove(addingModel1, null);
            set.Remove(addingModel2, null);
        }

        [TestMethod]
        public void TestOfThrowingCustomException()
        {
            var set = new Set<TestModel>();

            var addingModel1 = new TestModel();
            var addingModel2 = new TestModel();
            var addingModel3 = new TestModel("Name1");
            var addingModel4 = new TestModel("Name1");

            set.Add(addingModel1);
            Assert.ThrowsException<AdditionException>(() => set.Add(addingModel2));

            set.Add(addingModel3);
            Assert.ThrowsException<AdditionException>(() => set.Add(addingModel4));

            set.Remove(addingModel1);
            Assert.ThrowsException<RemovalException>(() => set.Remove(addingModel2));

            set.Remove(addingModel3);
            Assert.ThrowsException<RemovalException>(() => set.Remove(addingModel4));

            var list = new List<TestModel>()
            {
                addingModel1,
                addingModel2,
                addingModel3,
                addingModel4
            };

            Assert.ThrowsException<AdditionException>(() => set = new Set<TestModel>(list));
        }
    }
}
