namespace Tudo_List.Application.Models.Dtos
{
    public class AuthResultDto
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public List<string>? Errors { get; set; }
    }
}
