using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerPerformanceService.Models
{
    public class PlayerPerformance
    {
        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public string PlayerName { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Required]
        public virtual ICollection<PerformanceSnapshot> Snapshots { get; set; }

        public PlayerPerformance()
        {
            Snapshots = new HashSet<PerformanceSnapshot>();
        }
    }
}
