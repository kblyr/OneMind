namespace OneMind.Mailing;

public interface IMailSendQueue
{
    Task<Guid> EnqueueAsync(Mail mail, CancellationToken cancellationToken = default);
}
