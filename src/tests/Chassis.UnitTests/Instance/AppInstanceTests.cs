using System;
using Chassis.Instance;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Instance
{
    public class AppInstanceSpecs
    {
        [SetUp]
        public void SetUp()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "");
        }



        [Test]
        public void EqualityChecks()
        {
            AppInstance.External.ShouldBe(AppInstance.Default);
        }

        [Test]
        public void DoubleEquals()
        {
            var a = AppInstance.External == AppInstance.Default;
            a.ShouldBe(true);
        }


        [Test]
        public void DoubleEqualsOnProd()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "PRODUCTION");
            var a = AppInstance.External == new AppInstance("PRODUCTION");
            a.ShouldBe(true);
        }

        [Test]
        public void DefaultShouldBeTest()
        {
            AppInstance.External.ShouldBe("00");
        }

        [Test]
        public void EnvOverridesFile()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "INSTANCE");
            AppInstance.External.ShouldBe("INSTANCE");
        }

        [Test]
        public void ShouldUpper()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "iii");
            AppInstance.External.ShouldBe("III");
        }
    }
}
