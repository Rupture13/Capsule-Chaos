using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreboardService.Models
{
    public class Highscore
    {
        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public string Playername { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Required]
        public int CollectedScore { get; set; }

        [Required]
        public int FinishedTime { get; set; }

        [Required]
        public int CalculatedTotal { get; set; }
    }
}
