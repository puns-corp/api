using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Result
    {
        [Key]
        public Guid Id { get; set;}

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public Guid PlayerId { get; set; }

        public int Score { get; set; }
    }
}
