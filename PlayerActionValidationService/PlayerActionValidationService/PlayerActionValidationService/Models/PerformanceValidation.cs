using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerActionValidationService.Models
{
    public class PerformanceValidation
    {
        public int Id { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Required]
        public int MaximumScore { get; set; }

        [Required]
        public int MinimumTime { get; set; }
    }
}
