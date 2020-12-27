using PunsApi.Models;

namespace PunsApi.Dtos.Games
{
    public class PasswordDto
    {
        public string Name { get; set; }

        public PasswordDto(Password password)
        {
            Name = password.Content;
        }
    }
}
