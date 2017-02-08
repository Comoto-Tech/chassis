using System;
using Chassis.Env;
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
            AppInstance.INSTANCE.ShouldBe(AppInstance.DEFAULT);
        }

        [Test]
        public void DoubleEquals()
        {
            var a = AppInstance.INSTANCE == AppInstance.DEFAULT;
            a.ShouldBe(true);
        }


        [Test]
        public void DoubleEqualsOnProd()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "PRODUCTION");
            var a = AppInstance.INSTANCE == new AppInstance("PRODUCTION");
            a.ShouldBe(true);
        }

        [Test]
        public void DefaultShouldBeTest()
        {
            AppInstance.INSTANCE.ShouldBe("00");
        }

        [Test]
        public void EnvOverridesFile()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "INSTANCE");
            AppInstance.INSTANCE.ShouldBe("INSTANCE");
        }

        [Test]
        public void ShouldUpper()
        {
            Environment.SetEnvironmentVariable(AppInstance.EnvironmentVariable, "iii");
            AppInstance.INSTANCE.ShouldBe("III");
        }
    }
}
