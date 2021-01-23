using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PunsApi.Models
{
    public class RefreshToken
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public Guid PlayerId { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public bool Revoked { get; set; } = false;

        public DateTime Created { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool IsActive => !(IsExpired || Revoked);
    }
}
