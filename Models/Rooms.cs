using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Rooms
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public int PlayerMinCount { get; set; }
        public int PlayerMaxCount { get; set; }
        public bool IsPassword { get; set; }
        protected virtual string PasswordHash { get; set; }
        [NotMapped]
        public string Password
        {
            //todo:
            //get { return Decrypt(PasswordHash); }
            //set { PasswordHash = Encrypt(value); }
            //adhoc:
            get { return PasswordHash; }
            set { PasswordHash = value; }
        }
        public ICollection<Game> Games { get; set; }

    }
}
