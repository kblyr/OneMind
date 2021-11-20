namespace OneMind.Data.EntityTypeConfigurations;

sealed class UserETC : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User")
            .AsSoftDeletable()
            .HasInsertFootprint()
            .HasUpdateFootprint()
            .HasDeleteFootprint();
    }
}