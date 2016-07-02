using System;
using Chassis.Env;
using NUnit.Framework;
using Shouldly;

namespace Chassis.UnitTests.Env
{
    public class AppEnvSpecs
    {
        [SetUp]
        public void SetUp()
        {
            FS.DeleteFile(AppEnv.EnvironmentFile);
            Environment.SetEnvironmentVariable(AppEnv.EnvironmentVariable, "");
        }

        [TearDown]
        public void TearDown()
        {
            FS.DeleteFile(AppEnv.EnvironmentFile);
        }

        [Test]
        public void EqualityChecks()
        {
            AppEnv.ENV.ShouldBe(AppEnv.TEST);
        }

        [Test]
        public void DoubleEquals()
        {
            var a = AppEnv.ENV == AppEnv.TEST;
            a.ShouldBe(true);
        }


        [Test]
        public void DoubleEqualsOnProd()
        {
            Environment.SetEnvironmentVariable(AppEnv.EnvironmentVariable, "PRODUCTION");
            var a = AppEnv.ENV == AppEnv.PRODUCTION;
            a.ShouldBe(true);
        }

        [Test]
        public void DefaultShouldBeTest()
        {
            AppEnv.Set(null);
            AppEnv.ENV.ShouldBe("TEST");
        }

        [Test]
        public void CantSetInCodeIfSetInFile()
        {
            FS.WriteFile(AppEnv.EnvironmentFile, "FILE");
            AppEnv.Set(AppEnv.PRODUCTION);

            AppEnv.ENV.ShouldBe("FILE");
        }

        [Test]
        public void CanSetFromCode()
        {
            AppEnv.Set(AppEnv.PRODUCTION);
            AppEnv.ENV.ShouldBe("PRODUCTION");
        }


        [Test]
        public void CanSetFromCodeIfNotDebug()
        {
            Environment.SetEnvironmentVariable(AppEnv.EnvironmentVariable, "PRODUCTION");
            AppEnv.Set("BOB");
            AppEnv.ENV.ShouldBe("PRODUCTION");
        }

        [Test]
        public void EasyModeSet()
        {
            AppEnv.Set("BOB");
            AppEnv.ENV.ShouldBe("BOB");
        }


        [Test]
        public void FileOption()
        {
            FS.WriteFile(AppEnv.EnvironmentFile, "FILE");
            AppEnv.ENV.ShouldBe("FILE");
        }

        [Test]
        public void EnvOverridesFile()
        {
            FS.WriteFile(AppEnv.EnvironmentFile, "FILE");

            Environment.SetEnvironmentVariable(AppEnv.EnvironmentVariable, "ENV");
            AppEnv.ENV.ShouldBe("ENV");
        }


        [Test]
        public void ShouldUpper()
        {
            AppEnv.Set("bob");
            AppEnv.ENV.ShouldBe("BOB");
        }

        [Test]
        public void ShouldAcceptAppEnv()
        {
            AppEnv.Set(AppEnv.TEST);
            AppEnv.ENV.ShouldBe("TEST");
        }

        [Test]
        public void CantOverrideProductionIfSetInFile()
        {
            FS.WriteFile(AppEnv.EnvironmentFile, "PRODUCTION");
            AppEnv.Set(AppEnv.TEST);

            AppEnv.ENV.ShouldBe(AppEnv.PRODUCTION);
        }
    }
}
