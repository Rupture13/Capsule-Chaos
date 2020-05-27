using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerPerformanceService.Models
{
    public class PerformanceSnapshot
    {
        public int Id { get; set; }

        [Required]
        public int Timestamp { get; set; }

        [Required]
        public Vector3Ser Location { get; set; }

        [Required]
        public Vector3Ser Rotation { get; set; }
    }

    [System.Serializable]
    public struct Vector3Ser
    {
        public Vector3Ser(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
