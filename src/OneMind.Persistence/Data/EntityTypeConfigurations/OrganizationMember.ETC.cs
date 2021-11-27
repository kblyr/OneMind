namespace OneMind.Data.EntityTypeConfigurations;

sealed class OrganizationMemberETC : IEntityTypeConfiguration<OrganizationMember>
{
    public void Configure(EntityTypeBuilder<OrganizationMember> builder)
    {
        builder.ToTable("OrganizationMember")
            .AsSoftDeletable()
            .HasInsertFootprint()
            .HasDeleteFootprint();

        builder.HasOne(organizationMember => organizationMember.Organization)
            .WithMany()
            .HasForeignKey(organizationMember => organizationMember.OrganizationId);

        builder.HasOne(organizationMember => organizationMember.Member)
            .WithMany()
            .HasForeignKey(organizationMember => organizationMember.MemberId);
    }
}
