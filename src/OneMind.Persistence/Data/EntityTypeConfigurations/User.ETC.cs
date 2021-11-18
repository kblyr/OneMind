using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneMind.Data.Entities;

namespace OneMind.Data.EntityTypeConfigurations
{
    sealed class UserETC : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
        }
    }
}