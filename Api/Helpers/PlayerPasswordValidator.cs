using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PunsApi.Helpers
{
    public class PlayerPasswordValidator
    {
        public int MinLength { get; set; } = 8;

        public PlayerPasswordValidator()
        {
        }

        public PlayerPasswordValidator(int minLength)
        {
            MinLength = minLength;
        }

        public (bool, string) ValidateAsync(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < MinLength)
                return (false, "Password too short");
            
            var counter = 0;
            var patterns = new List<string>();
            patterns.Add(@"[a-z]");
            patterns.Add(@"[A-Z]");
            patterns.Add(@"[0-9]");  
            patterns.Add(@"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\] ]");

            foreach (var p in patterns)
            {
                if (Regex.IsMatch(password, p))
                {
                    counter++;
                }
            }

            if (counter < 2)
            {
                return (false,
                    "Use characters from min 2 of these groups: lowercase, uppercase, digits, special symbols");
            }
            return (true, "");
        }
    }
}
