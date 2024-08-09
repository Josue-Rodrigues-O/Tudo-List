namespace Tudo_List.Application.Dtos.User
{
    public record UserDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
