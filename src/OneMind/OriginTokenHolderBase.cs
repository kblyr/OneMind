namespace OneMind;

public abstract record OriginTokenHolderBase : IOriginTokenHolder
{
    public OriginToken OriginToken { get; init; }
}
