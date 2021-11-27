namespace OneMind.Processes;

sealed class InsertTeam : IAsyncProcess<Team, int>
{
    public async Task<int> ExecuteAsync(IProcessContext processContext, Team team, CancellationToken cancellationToken = default)
    {
        if (processContext is not OneMindDbContext context)
            throw InvalidProcessContextException.Expects<OneMindDbContext>();

        context.Teams.Add(team, context.CurrentFootprint);
        await context.TrySaveChangesAsync(cancellationToken);
        return team.Id;
    }
}