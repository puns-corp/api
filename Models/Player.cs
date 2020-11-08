using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PunsApi.Models
{
    public class Player
    {
        public Guid ID { get; set; }
        public string Nick { get; set; }
        public bool IsGameMaster { get; set; }
        public bool IsPlaying { get; set; }
        protected virtual string PasswordHash { get; set; }
        [NotMapped]
        public string Password
        {
            //todo:
            //get { return Decrypt(PasswordHash); }
            //set { PasswordHash = Encrypt(value); }
            //adhoc
            get { return PasswordHash; }
            set { PasswordHash = value; }
        }

        //Foreign key to Game
        [ForeignKey("Game")]
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        

    }
}
