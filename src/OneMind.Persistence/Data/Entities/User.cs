namespace OneMind.Data.Entities
{
    public record User
    {
        public int Id { get; init; }
        public string Username { get; init; } = "";
        public string HashedPassword { get; init; } = "";
    }
}