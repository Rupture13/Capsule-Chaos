using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoreboardService
{
    public class RabbitMQOptions
    {
        public const string Position = "RabbitMQ";

        public string Connection { get; set; }
    }
}
