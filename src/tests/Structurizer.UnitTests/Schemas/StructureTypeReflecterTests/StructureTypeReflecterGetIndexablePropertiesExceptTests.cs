﻿using System;
using System.Linq;
using NUnit.Framework;

namespace Structurizer.UnitTests.Schemas.StructureTypeReflecterTests
{
    [TestFixture]
    public class StructureTypeReflecterGetIndexablePropertiesExceptTests : StructureTypeReflecterTestsBase
    {
        [Test]
        public void GetIndexablePropertiesExcept_WhenCalledWithNullExlcudes_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => ReflecterFor().GetIndexablePropertiesExcept(typeof(Item), null));

            Assert.AreEqual("memberPaths", ex.ParamName);
        }

        [Test]
        public void GetIndexablePropertiesExcept_WhenCalledWithNoExlcudes_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentException>(() => ReflecterFor().GetIndexablePropertiesExcept(typeof(Item), new string[] { }));

            Assert.AreEqual("memberPaths", ex.ParamName);
        }

        [Test]
        public void GetIndexablePropertiesExcept_WhenBytesArrayExists_PropertyIsNotReturned()
        {
            var properties = ReflecterFor().GetIndexablePropertiesExcept(typeof(Item), new[] { "" });

            Assert.IsNull(properties.SingleOrDefault(p => p.Path == "Bytes1"));
        }

        [Test]
        public void GetIndexablePropertiesExcept_WhenExcludingAllProperties_NoPropertiesAreReturned()
        {
            var properties = ReflecterFor().GetIndexablePropertiesExcept(typeof(Item), new[] { "Bool1", "DateTime1", "String1", "Nested", "Nested.Int1OnNested", "Nested.String1OnNested" });

            Assert.AreEqual(0, properties.Count());
        }

        [Test]
        public void GetIndexablePropertiesExcept_WhenExcludingComplexNested_NoNestedPropertiesAreReturned()
        {
            var properties = ReflecterFor().GetIndexablePropertiesExcept(typeof(Item), new[] { "Nested" });

            Assert.AreEqual(0, properties.Count(p => p.Path.StartsWith("Nested")));
        }

        [Test]
        public void GetIndexablePropertiesExcept_WhenExcludingNestedSimple_OtherSimpleNestedPropertiesAreReturned()
        {
            var properties = ReflecterFor().GetIndexablePropertiesExcept(typeof(Item), new[] { "Nested.String1OnNested" });

            Assert.AreEqual(1, properties.Count(p => p.Path.StartsWith("Nested")));
            Assert.AreEqual(1, properties.Count(p => p.Path == "Nested.Int1OnNested"));
        }

        private class Item
        {
            public bool Bool1 { get; set; }
            public DateTime DateTime1 { get; set; }
            public string String1 { get; set; }
            public byte[] Bytes1 { get; set; }
            public Nested Nested { get; set; }
        }

        private class Nested
        {
            public int Int1OnNested { get; set; }
            public string String1OnNested { get; set; }
        }
    }
}