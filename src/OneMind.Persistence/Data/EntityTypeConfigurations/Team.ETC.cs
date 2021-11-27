namespace OneMind.Data.EntityTypeConfigurations;

sealed class TeamETC : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team")
            .AsSoftDeletable()
            .HasInsertFootprint()
            .HasUpdateFootprint()
            .HasDeleteFootprint();

        builder.Property(team => team.Visibility)
            .HasConversion<short>();

        builder.HasOne(team => team.Leader)
            .WithMany()
            .HasForeignKey(team => team.LeaderId);

        builder.HasOne(team => team.Organization)
            .WithMany()
            .HasForeignKey(team => team.OrganizationId);

        builder.HasOne(team => team.CreaatedBy)
            .WithMany()
            .HasForeignKey(team => team.CreatedById);
    }
}
