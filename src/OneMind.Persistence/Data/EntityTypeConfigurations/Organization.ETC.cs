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
    }
}
