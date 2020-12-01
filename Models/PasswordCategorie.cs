using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class PasswordCategory
    {
        [Key]
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<Password> Passwords { get; set; }
    }
}
