using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Password
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("PasswordCategoryId")]
        public PasswordCategory PasswordCategory { get; set; }

        public Guid PasswordCategoryId { get; set; }

        public string Content { get; set; }
    }
}
