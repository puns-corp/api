using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Results
    {
        public Guid Id { get; set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("Player")]
        public virtual Player Player { get; set; }
        public int Score { get; set; }
    }
}
