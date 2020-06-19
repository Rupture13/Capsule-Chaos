using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerActionValidationService.Models
{
    public class PerformanceValidationContext : DbContext
    {
        static int count = 0;

        public PerformanceValidationContext(DbContextOptions<PerformanceValidationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("[DEBUG] I got to this point");
            // The container name
            modelBuilder.HasDefaultContainer("PerformanceValidations");
        }

        public virtual DbSet<PerformanceValidation> PerformanceValidations { get; set; }

        private async void SeedContextOnStartup(int counter)
        {
            if (counter > 0)
            {
                return;
            }

            var newPerformanceValidation1 = new PerformanceValidation
            {
                Id = new System.Guid(),
                LevelId = 1,
                MaximumScore = 10,
                MinimumTime = 2000
            };

            var newPerformanceValidation2 = new PerformanceValidation
            {
                Id = new System.Guid(),
                LevelId = 2,
                MaximumScore = 12,
                MinimumTime = 4500
            };

            await this.PerformanceValidations.AddAsync(newPerformanceValidation1);
            await this.PerformanceValidations.AddAsync(newPerformanceValidation2);

            await this.SaveChangesAsync();
        }
    }
}
