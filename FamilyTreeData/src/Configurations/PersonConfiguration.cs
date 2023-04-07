using FamilyTreeData.SqliteTypes;
using FamilyTreeLib.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTreeData.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        _ = builder.HasKey(p => p.Id);

        _ = builder.Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType(ColumnTypes.INTEGER)
            .HasMaxLength(20)
            .IsRequired();

        _ = builder.Property(p => p.FirstName)
            .HasColumnName("first_name")
            .HasColumnType(ColumnTypes.TEXT)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder.Property(p => p.Dob)
            .HasColumnName("date_of_birth")
            .HasColumnType(ColumnTypes.DATETIME)
            .IsRequired();
        
        _ = builder.Property(p => p.WifeName)
            .HasColumnName("wife_name")
            .HasColumnType(ColumnTypes.TEXT)
            .HasMaxLength(200)
            .IsRequired(false);

        _ = builder.Property(p => p.FatherId)
            .HasColumnName("father_id")
            .HasColumnType(ColumnTypes.TEXT)
            .HasMaxLength(200)
            .IsRequired(false);
        
        _ = builder.Property(p => p.MotherId)
            .HasColumnName("mother_id")
            .HasColumnType(ColumnTypes.TEXT)
            .HasMaxLength(200)
            .IsRequired(false);

        _ = builder.HasOne(p => p.Father);
        _ = builder.HasOne(p => p.Mother);

        _ = builder.HasMany(p => p.Childrens);

        _ = builder.ToTable("persons");


    }
}