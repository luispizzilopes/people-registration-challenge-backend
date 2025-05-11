using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Infrastructure.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Email)
          .HasMaxLength(200)
          .IsRequired();

        builder.Property(entity => entity.Password)
          .HasMaxLength(200)
          .IsRequired();
    }
}
