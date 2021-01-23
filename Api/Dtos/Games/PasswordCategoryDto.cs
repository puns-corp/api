using System;
using System.Collections.Generic;
using PunsApi.Models;

namespace PunsApi.Dtos.Games
{
    public class PasswordCategoryDto
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public List<PasswordDto> Passwords { get; set; }

        public PasswordCategoryDto(PasswordCategory passwordCategory)
        {
            Passwords = new List<PasswordDto>();
            Name = passwordCategory.CategoryName;
            Id = passwordCategory.Id;
            foreach (var password in passwordCategory.Passwords)
            {
                Passwords.Add(new PasswordDto(password));
            }
        }
    }
}
