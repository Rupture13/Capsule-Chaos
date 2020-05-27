using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayerPerformanceService.Models
{
    public class PlayerPerformanceContext : DbContext
    {
        public PlayerPerformanceContext(DbContextOptions<PlayerPerformanceContext> options)
            : base(options)
        {
            //TODO: Remove this method call and the method itself after testing
            //Parameter:
            //1 for seeding only with AdviceContents
            //2 for seeding only with AdviceContents and Advicese
            //3 for seeding with all models
            SeedContextOnStartup();
        }

        public virtual DbSet<PlayerPerformance> PlayerPerformances { get; set; }
        public virtual DbSet<PerformanceSnapshot> PerformanceSnapshots { get; set; }

        private void SeedContextOnStartup()
        {
            if (this.PlayerPerformances.Find(1) != null)
            {
                return;
            }

            var snapshot1 = new PerformanceSnapshot
            {
                Timestamp = 1,
                Location = new Vector3Ser(0, 0, 0),
                Rotation = new Vector3Ser(0, 0, 0)
            };
            var snapshot2 = new PerformanceSnapshot
            {
                Timestamp = 2,
                Location = new Vector3Ser(1, 0, 0),
                Rotation = new Vector3Ser(90, 0, 0)
            };
            var snapshot3 = new PerformanceSnapshot
            {
                Timestamp = 3,
                Location = new Vector3Ser(1.5f, 0, 1),
                Rotation = new Vector3Ser(0, 90, 0)
            };
            var snapshot4 = new PerformanceSnapshot
            {
                Timestamp = 4,
                Location = new Vector3Ser(2, 0, 2),
                Rotation = new Vector3Ser(0, 0, 90)
            };
            var snapshot5 = new PerformanceSnapshot
            {
                Timestamp = 5,
                Location = new Vector3Ser(2, 0, 3),
                Rotation = new Vector3Ser(90, 90, 0)
            };

            //this.PerformanceSnapshots.Add(snapshot1);
            //this.PerformanceSnapshots.Add(snapshot2);
            //this.PerformanceSnapshots.Add(snapshot3);
            //this.PerformanceSnapshots.Add(snapshot4);
            //this.PerformanceSnapshots.Add(snapshot5);

            var performance = new PlayerPerformance
            {
                PlayerId = 2,
                LevelId = 1
            };
            performance.Snapshots.Add(snapshot1);
            performance.Snapshots.Add(snapshot2);
            performance.Snapshots.Add(snapshot3);
            performance.Snapshots.Add(snapshot4);
            performance.Snapshots.Add(snapshot5);

            this.PlayerPerformances.Add(performance);

            this.SaveChanges();
        }

    }
}
