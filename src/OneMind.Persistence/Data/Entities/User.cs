namespace OneMind.Data.Entities;

record User
{
    public int Id { get; init; }
    public string Username { get; init; } = "";
    public string EmailAddress { get; init; } = "";
    public string HashedPassword { get; init; } = "";
    public bool IsEmailVerified { get; init; }
    public bool IsPasswordChangeRequired { get; init; }
}