namespace OneMind.Events;

public record OrganizationCreatedEvent : INotification
{
    public int Id { get; init; }
}
