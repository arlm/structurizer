﻿using NUnit.Framework;

namespace Structurizer.UnitTests.StructureBuilderTests
{
    [TestFixture]
    public class StructureBuilderGraphTests : StructureBuilderBaseTests
    {
        protected override void OnTestInitialize()
        {
            Builder = StructureBuilder.Create(c => c.Register<Root>());
        }

        [Test]
        public void CreateStructure_WhenNestedItemExists_NestedWillBePartOfStructure()
        {
            var item = new Root { IntOnRoot = 142, Nested = new Child { IntOnChild = 242 } };

            var structure = Builder.CreateStructure(item);

            Assert.AreEqual(2, structure.Indexes.Count);
            Assert.AreEqual("IntOnRoot", structure.Indexes[0].Path);
            Assert.AreEqual("Nested.IntOnChild", structure.Indexes[1].Path);
        }

        private class Root
        {
            public int IntOnRoot { get; set; }
            public Child Nested { get; set; }
        }

        private class Child
        {
            public int IntOnChild { get; set; }
        }
    }
}