namespace CLOTHAPI.DTOs
{
    public class AuthDtos
    {
        public record RegisterDto(string Email, string Password, string FirstName, string LastName);
        public record LoginDto(string Email, string Password);
        public record AuthResponseDto(string Token, string Email, string FirstName, string Role);
    }
}
