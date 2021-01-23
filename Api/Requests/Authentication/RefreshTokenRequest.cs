using System.ComponentModel.DataAnnotations;

namespace PunsApi.Requests.Authentication
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
