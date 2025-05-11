using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Infrastructure.EntitiesConfiguration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Cpf)
            .HasMaxLength(11)
            .IsRequired(); 

        builder.Property(entity => entity.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(entity => entity.Email)
          .HasMaxLength(200); 

        builder.Property(entity => entity.BirthDate)
          .IsRequired();

        builder.Property(entity => entity.Address)
          .HasMaxLength(200);

        builder.Property(entity => entity.Naturality)
          .HasMaxLength(100);

        builder.Property(entity => entity.Nacionality)
          .HasMaxLength(100);
    }
}
