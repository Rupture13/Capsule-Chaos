using NUnit.Framework;
using PlayerActionValidationService.Controllers;
using PlayerActionValidationService.Models;

namespace PlayerActionValidationService_Tests
{
    public class Tests
    {
        private PerformanceValidation newPerformance;
        private PerformanceValidation validation;

        [SetUp]
        public void Setup()
        {
            validation = new PerformanceValidation
            {
                LevelId = 1,
                MaximumScore = 10,
                MinimumTime = 2000
            };
        }

        [Test]
        public void TestValidationNewPerformanceGood()
        {
            newPerformance = new PerformanceValidation
            {
                LevelId = 1,
                MaximumScore = 10,
                MinimumTime = 3500
            };

            bool result = ValidationsController.PerformanceMatchesValidation(newPerformance, validation);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestValidationNewPerformanceBadLevelId()
        {
            newPerformance = new PerformanceValidation
            {
                LevelId = 2,
                MaximumScore = 10,
                MinimumTime = 3500
            };

            bool result = ValidationsController.PerformanceMatchesValidation(newPerformance, validation);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestValidationNewPerformanceBadScore()
        {
            newPerformance = new PerformanceValidation
            {
                LevelId = 1,
                MaximumScore = 12,
                MinimumTime = 3500
            };

            bool result = ValidationsController.PerformanceMatchesValidation(newPerformance, validation);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestValidationNewPerformanceBadTime()
        {
            newPerformance = new PerformanceValidation
            {
                LevelId = 1,
                MaximumScore = 10,
                MinimumTime = 1900
            };

            bool result = ValidationsController.PerformanceMatchesValidation(newPerformance, validation);
            Assert.IsFalse(result);
        }
    }
}