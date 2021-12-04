namespace OneMind.EntityTypeConfigurations;

sealed class OrganizationInvitationETC : IEntityTypeConfiguration<OrganizationInvitation>
{
    public void Configure(EntityTypeBuilder<OrganizationInvitation> builder)
    {
        builder.ToTable("OrganizationInvitation")
            .AsSoftDeletable()
            .HasInsertFootprint()
            .HasDeleteFootprint();

        builder.Property(organizationInvitation => organizationInvitation.Status)
            .HasConversion<short>();

        builder.HasOne(organizationInvitation => organizationInvitation.Organization)
            .WithMany()
            .HasForeignKey(organizationInvitation => organizationInvitation.OrganizationId);

        builder.HasOne(organizationInvitation => organizationInvitation.Sender)
            .WithMany()
            .HasForeignKey(organizationInvitation => organizationInvitation.SenderId);

        builder.HasOne(organizationInvitation => organizationInvitation.Recipient)
            .WithMany()
            .HasForeignKey(organizationInvitation => organizationInvitation.RecipientId);
    }
}
