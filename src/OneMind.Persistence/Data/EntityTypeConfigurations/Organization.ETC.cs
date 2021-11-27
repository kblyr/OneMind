namespace OneMind.Data.EntityTypeConfigurations;

sealed class OrganizationETC : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organization")
            .AsSoftDeletable()
            .HasInsertFootprint()
            .HasUpdateFootprint()
            .HasDeleteFootprint();

        builder.Property(organization => organization.Visibility)
            .HasConversion<short>();

        builder.HasOne(organization => organization.Leader)
            .WithMany()
            .HasForeignKey(organization => organization.LeaderId);

        builder.HasOne(organization => organization.CreaatedBy)
            .WithMany()
            .HasForeignKey(organization => organization.CreatedById);
    }
}
