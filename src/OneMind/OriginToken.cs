namespace OneMind;

public record struct OriginToken
{
    public Guid Id { get; init; }
    public string? Owner { get; init; } = null;

    public OriginToken(Guid id)
    {
        Id = id;
    }
}
