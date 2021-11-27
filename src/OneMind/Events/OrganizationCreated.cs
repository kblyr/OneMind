namespace OneMind.Events;

public record OrganizationCreated : INotification
{
    public int Id { get; init; }
}
