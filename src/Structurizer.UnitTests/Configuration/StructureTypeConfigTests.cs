using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Structurizer.Configuration;

namespace Structurizer.UnitTests.Configuration
{
    [TestFixture]
    public class StructureTypeConfigTests : UnitTestBase
    {
        [Test]
        public void Ctor_Should_throw_When_missing_type()
        {
            Action invalidAction = () => new StructureTypeConfig(null, default(IndexMode), new HashSet<string>());

            invalidAction.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("structureType");
        }

        [Test]
        public void Ctor_Should_throw_When_missing_member_paths()
        {
            Action invalidAction = () => new StructureTypeConfig(typeof(Dummy), default(IndexMode), null);

            invalidAction.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("memberPaths");
        }

        [Test]
        public void Ctor_Should_initialize_properly()
        {
            var expectedType = typeof(Dummy);
            var config = new StructureTypeConfig(expectedType, IndexMode.Inclusive, new HashSet<string> { "Foo", "Bar" });

            config.Type.Should().Be(expectedType);
            config.IndexMode.Should().Be(IndexMode.Inclusive);
            config.MemberPaths.Should().BeEquivalentTo("Foo", "Bar");
        }

        private class Dummy { }
    }
}