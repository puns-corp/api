using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Password
    {
        [Key]
        public Guid Id { get; set; }
        public string PasswordContent { get; set; }
    }
}
