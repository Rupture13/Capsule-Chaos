using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreboardService.Models
{
    public class ScoreboardContext : DbContext
    {
        public ScoreboardContext(DbContextOptions<ScoreboardContext> options)
            : base(options)
        {
            //TODO: Remove this method call and the method itself after testing
            //Parameter:
            //1 for seeding only with AdviceContents
            //2 for seeding only with AdviceContents and Advicese
            //3 for seeding with all models
            SeedContextOnStartup();
        }

        public virtual DbSet<Highscore> Highscores { get; set; }

        private void SeedContextOnStartup()
        {
            if (this.Highscores.Find(1) != null)
            {
                return;
            }

            var highscore1 = new Highscore
            {
                PlayerId = 1,
                Playername = "Rupture13",
                LevelId = 1,
                CollectedScore = 10,
                FinishedTime = 4500,
                CalculatedTotal = 4500
            };
            var highscore2 = new Highscore
            {
                PlayerId = 2,
                Playername = "Vasharnesh",
                LevelId = 1,
                CollectedScore = 10,
                FinishedTime = 4000,
                CalculatedTotal = 4000
            };

            this.Highscores.Add(highscore1);
            this.Highscores.Add(highscore2);

            this.SaveChanges();
        }
    }
}
