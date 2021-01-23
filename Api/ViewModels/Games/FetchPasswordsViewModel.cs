using System.Collections.Generic;
using PunsApi.Dtos.Games;
using PunsApi.Models;

namespace PunsApi.ViewModels.Games
{
    public class FetchPasswordsViewModel
    {
        public List<PasswordCategoryDto> PasswordCategories { get; set; }

        public FetchPasswordsViewModel(List<PasswordCategory> passwordCategories)
        {
            PasswordCategories = new List<PasswordCategoryDto>();

            foreach (var passwordCategory in passwordCategories)
            {
                PasswordCategories.Add(new PasswordCategoryDto(passwordCategory));
            }
        }
    }
}
