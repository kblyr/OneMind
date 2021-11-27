namespace OneMind.Data.EntityTypeConfigurations;

sealed class TeamMemberETC : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.ToTable("TeamMember")
            .AsSoftDeletable()
            .HasInsertFootprint()
            .HasDeleteFootprint();

        builder.HasOne(teamMember => teamMember.Team)
            .WithMany()
            .HasForeignKey(teamMember => teamMember.TeamId);

        builder.HasOne(teamMember => teamMember.Member)
            .WithMany()
            .HasForeignKey(teamMember => teamMember.MemberId);
    }
}
