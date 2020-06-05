using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerActionValidationService.Models
{
    public class PerformanceValidationContext : DbContext
    {
        public PerformanceValidationContext(DbContextOptions<PerformanceValidationContext> options)
            : base(options)
        {
            SeedContextOnStartup();
        }

        public virtual DbSet<PerformanceValidation> PerformanceValidations { get; set; }

        private void SeedContextOnStartup()
        {
            if (this.PerformanceValidations.Find(1) != null)
            {
                return;
            }

            var newPerformanceValidation1 = new PerformanceValidation
            {
                LevelId = 1,
                MaximumScore = 10,
                MinimumTime = 2000
            };

            var newPerformanceValidation2 = new PerformanceValidation
            {
                LevelId = 2,
                MaximumScore = 12,
                MinimumTime = 4500
            };

            this.PerformanceValidations.Add(newPerformanceValidation1);
            this.PerformanceValidations.Add(newPerformanceValidation2);

            this.SaveChanges();
        }
    }
}
