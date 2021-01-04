using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Data.Config
{
    public class AutorConfiguration : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("PL.Autor");

            builder.HasKey(autor => autor.Id);

            builder.HasMany(b => b.Livros)
                .WithOne(a => a.Autor)
                .HasForeignKey(t => t.AutorId);

            builder.Property(autor => autor.Nome)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(autor => autor.Sobrenome)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(autor => autor.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(autor => autor.Bio)
                .IsRequired(false)
                .HasColumnType("TEXT");

            builder.Property(autor => autor.Avatar)
                .IsRequired(false)
                .HasMaxLength(100);
        }
    }
}
